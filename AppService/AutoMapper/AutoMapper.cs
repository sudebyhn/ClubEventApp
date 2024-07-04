using AppService.Dto;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.AutoMapper
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<CreateClubMenagerRequestDto,ClubMenager>();
            CreateMap<CreateClubRequestDto, Club>();
            CreateMap<CreateEventRequestDto,Event>();
            CreateMap<Club, GetClubBySearchResponseDto>();
            CreateMap<Event, GetEventBySearchResponseDto>();
            CreateMap<ClubMenager, GetClubMenagerBySearchResponseDto>();
            CreateMap<SaveImageRequestDto, Club>()
                .ForMember(destinationMember => destinationMember.ClubId, operation => operation.MapFrom(sourceMember => sourceMember.Id));
            CreateMap<SaveImageRequestDto, Event>()
                .ForMember(destinationMember => destinationMember.EventId, operation => operation.MapFrom(sourceMember => sourceMember.Id));
            CreateMap<Club,ImageResponseDto>();
            CreateMap<Event, ImageResponseDto>();
            CreateMap<UpdateClubRequestDto, Club>();
            CreateMap<UpdateClubMenagerRequestDto, ClubMenager>();

        }
    }
}
