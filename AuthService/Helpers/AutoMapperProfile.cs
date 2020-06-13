using Authentication.Models;
using AutoMapper;
using Domain.Entities;

public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserRegisterModel, User>();
            CreateMap<UpdateUserModel, User>();
        }
    }