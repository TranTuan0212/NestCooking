﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NESTCOOKING_API.Business.DTOs;
using NESTCOOKING_API.Business.Services.IServices;
using NESTCOOKING_API.Presentation.Helpers;
using NESTCOOKING_API.Utility;
using System.Net;
using System.Security.Claims;

namespace NESTCOOKING_API.Presentation.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;
        private readonly IPurchasedRecipesService _purchasedRecipesService;

        public TransactionController(IPaymentService paymentService, ITransactionService transactionService, IUserService userService, IPurchasedRecipesService purchasedRecipesService)
        {
            _paymentService = paymentService;
            _transactionService = transactionService;
            _userService = userService;
            _purchasedRecipesService = purchasedRecipesService;
        }
        [HttpPost("create")]
        [Authorize]
        [ApiValidatorFilter]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] TransactionInfor transactionInfor)
        {
            try
            {
                if (!String.Equals(transactionInfor.OrderType, PaymentType_DEPOSIT, StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest(ResponseDTO.BadRequest(message: "Type is not valid"));
                }
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var transactionId = await _transactionService.CreateTransaction(transactionInfor, userId, StaticDetails.Payment_VnPay);
                var paymentUrl = _paymentService.CreatePaymentUrl(transactionInfor, HttpContext, transactionId);
                return Ok(ResponseDTO.Accept(result: paymentUrl));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseDTO.BadRequest(ex.Message));
            }
        }

        [HttpGet("callback")]
        public async Task<IActionResult> ProcessPaymentCallback()
        {
            try
            {
                if (Request.Query.Count <= 0)
                {
                    return BadRequest(ResponseDTO.BadRequest("Input data required"));
                }
                else
                {
                    var paymentResponse = _paymentService.ProcessPaymentCallback(Request.Query);
                    var typeTransaction = await _transactionService.GetTransactionTypeByIdAsync(paymentResponse.OrderId);
                    if (paymentResponse.Success)
                    {
                        var initialAmountTransaction = (paymentResponse.Amount / 100) / StaticDetails.VNDToUSD;
                        await _transactionService.TransactionSuccessById(paymentResponse.OrderId, true);
                        if (String.Equals(typeTransaction, StaticDetails.PaymentType_DEPOSIT, StringComparison.OrdinalIgnoreCase))
                        {
                            await _userService.UpdateUserBalanceWithDeposit(paymentResponse.OrderId, initialAmountTransaction);
                        }
                        else
                        {
                            var recipeId = await _purchasedRecipesService.FindIdRecipeByTransactionId(paymentResponse.OrderId);
                            await _userService.UpdateUserBalanceWithPurchaseRecipe(initialAmountTransaction, recipeId);
                        }
                    }
                    else
                    {
                        if (String.Equals(typeTransaction, StaticDetails.PaymentType_PURCHASEDRECIPE, StringComparison.OrdinalIgnoreCase))
                        {
                            await _purchasedRecipesService.DeletePurchaseByTransactionId(paymentResponse.OrderId);
                        }
                        return BadRequest(ResponseDTO.Create(HttpStatusCode.BadRequest, message: "Error", result: paymentResponse));
                    }
                    return Ok(ResponseDTO.Accept(result: paymentResponse));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseDTO.BadRequest(ex.Message));
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTransactionsByUserId()
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await _transactionService.GetTransactionsByUserId(userId);
                if (result == null)
                {
                    return BadRequest(ResponseDTO.BadRequest(message: "Not found transaction by user"));
                }
                return Ok(ResponseDTO.Accept(result: result));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseDTO.BadRequest(message: ex.Message));
            }
        }
        [HttpPost("withdraw")]
        [Authorize]
        public async Task<IActionResult> WithDrawByUser(string description, double amount)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var transactionInfor = new TransactionInfor
                {
                    OrderType = PaymentType_WITHDRAW,
                    Amount = amount,
                    OrderDescription = description,
                };
                var transactionId = await _transactionService.CreateTransaction(transactionInfor, userId, Payment_Wallet);
                if (transactionId == null)
                {
                    return BadRequest(ResponseDTO.BadRequest(message: "Withdraw Fail"));
                }
                if (await _userService.ChangeUserBalanceByWithdraw(userId, amount))
                {
                    await _transactionService.TransactionSuccessById(transactionId, true);
                }
                else
                {
                    return BadRequest(ResponseDTO.BadRequest(message: "Withdraw Fail"));
                }

                return Ok(ResponseDTO.Accept(result: "Withdraw Success"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseDTO.BadRequest(ex.Message));
            }
        }
    }
}
