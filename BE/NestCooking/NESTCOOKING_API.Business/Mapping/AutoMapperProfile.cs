﻿using AutoMapper;
using NESTCOOKING_API.Business.DTOs.AuthDTOs;
using NESTCOOKING_API.Business.DTOs.UserDTOs;
using NESTCOOKING_API.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NESTCOOKING_API.Business.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User
            CreateMap<User, RegistrationRequestDTO>().ReverseMap();
            CreateMap<User, LoginWithThirdPartyRequestDTO>().ReverseMap();
            CreateMap<User, UserDetailInfoDTO>().ReverseMap();
            CreateMap<User, UserDetailInfoDTO>().ReverseMap();
            CreateMap<User, UpdateUserDTO>().ReverseMap();
            CreateMap<User, UserShortInfoDTO>().ReverseMap();
            //CreateMap<User, ChefDetailDTO>().ReverseMap();
            //CreateMap<User, AdminUserDTO>().ReverseMap();

            //// Request to become chef
            //CreateMap<RequestToBecomeChef, CreatedRequestToBecomeChefDTO>().ReverseMap();
            //CreateMap<RequestToBecomeChef, RequestToBecomeChefDTO>().ReverseMap();
            //CreateMap<RequestToBecomeChef, ApprovalRequestDTO>().ReverseMap();
            //// Report
            //CreateMap<Report, ReportResponseDTO>().ReverseMap().ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
            //CreateMap<Report, CreateReportDTO>().ReverseMap();
            //CreateMap<Report, UpdateReportDTO>().ReverseMap();
            //CreateMap<Response, AdminResponseDTO>().ReverseMap().ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            //// Recipe
            //CreateMap<Category, CategoryDTO>().ReverseMap();
            //CreateMap<IngredientTipContent, IngredientTipContentDTO>().ReverseMap();
            //CreateMap<IngredientTip, IngredientTipDTO>().ReverseMap();
            //CreateMap<IngredientTip, IngredientTipShortInfoDTO>().ReverseMap();
            //CreateMap<Ingredient, CreateIngredientDTO>().ReverseMap();
            //CreateMap<Ingredient, IngredientDTO>().ReverseMap();
            //CreateMap<Recipe, RecipeDTO>().ReverseMap();
            //CreateMap<Instructor, InstructorDTO>().ReverseMap();
            //CreateMap<Instructor, InstructorDTO>().ReverseMap();
            //CreateMap<Instructor, CreateInstructorDTO>().ReverseMap();
            //CreateMap<Recipe, RecipeDetailDTO>().ReverseMap();
            //CreateMap<Recipe, CreateRecipeDTO>().ReverseMap();
            //CreateMap<Recipe, UpdateRecipeDTO>().ReverseMap();
            //CreateMap<Recipe, RecipeForBookingDTO>().ReverseMap();

            //// Comment
            //CreateMap<Comment, CreatedCommentDTO>().ReverseMap();
            //CreateMap<Comment, RequestCommentDTO>().ReverseMap();
            //CreateMap<Comment, UpdateCommentDTO>().ReverseMap();

            //// Transaction
            //CreateMap<Transaction, TransactionDTO>().ReverseMap();

            //// Notification
            //CreateMap<Notification, NotificationCreateDTO>().ReverseMap();
            //CreateMap<Notification, NotificationReadDTO>().ReverseMap();

            //// Booking
            //CreateMap<Booking, CreateBookingDTO>().ReverseMap();
            //CreateMap<Booking, BookingShortInfoDTO>().ReverseMap();
            //CreateMap<Booking, BookingDetailDTO>().ReverseMap();
            //CreateMap<Booking, UpdateUserDTO>().ReverseMap();
            //CreateMap<BookingTransactionDTO, Transaction>().ReverseMap();
        }
    }
}
