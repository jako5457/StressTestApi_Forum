using System.Runtime.CompilerServices;
using StressTestApi_Forum.Models;
using StressTestApi_Forum.Services.Efcore.Entities;
using AutoMapper;

namespace StressTestApi_Forum.Services
{
    public static class MapperExtensions
    {

        public static IServiceCollection AddAutoMapperMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.CreateMap<User, UserDto>().ReverseMap();
                config.CreateMap<Post, PostDto>();
                config.CreateMap<PostToBeCreatedDto, Post>();
                config.CreateMap<UserToBeCreatedDto, User>();
            });

            return services;
        }

    }
}
