using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using GameReview.Dto;
using GameReview.Models;
using GameReview.ViewModels;

namespace GameReview.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Review, ReviewDto>();
            Mapper.CreateMap<ReviewDto, Review>();

            Mapper.CreateMap<Game, GameItemViewModel>();
            Mapper.CreateMap<GameItemViewModel, Game>();
        }


    }
}