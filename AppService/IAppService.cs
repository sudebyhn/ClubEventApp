using AppService.Dto;
using Data;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService
{
    public interface IAppService
    {
        Task<Guid> LoginForSksAdmin(string email, string password);
        Task<Guid> LoginForClubMenager(string email, string password);
        Task CreateClub(CreateClubRequestDto requestDto);
        Task DeleteClub(DeleteClubRequestDto requestDto);
        Task UpdateClub(UpdateClubRequestDto requestDto);
        Task CreateEvent(CreateEventRequestDto requestDto);
        Task<object> DeleteEvent(DeleteEventRequestDto requestDto);
        Task<object> UpdateEvent(UpdateEventRequestDto requestDto);
        Task CreateClubMenager(CreateClubMenagerRequestDto requestDto);
        Task DeleteClubMenager(DeleteClubMenagerRequestDto requestDto);
        Task UpdateClubMenager(UpdateClubMenagerRequestDto requestDto);
        Task<List<GetClubBySearchResponseDto>> SearchClubBySearchText(BaseGetRequestDto requestDto);
        Task<List<GetEventBySearchResponseDto>> SearchEventBySearchText(BaseGetRequestDto requestDto);
        Task<List<GetClubMenagerBySearchResponseDto>> SearchClubMenagerBySearchText(BaseGetRequestDto requestDto);
        Task<List<Club>> GetClubs();
        Task<List<Domain.Event>> GetEvents();
        Task<List<Domain.ClubMenager>> GetClubMenagers();
        Task<Club> GetClubById(BaseGetRequestIdDto requestDto);
        Task<Domain.Event> GetEventById(BaseGetRequestIdDto requestDto);
        Task<Domain.ClubMenager> GetClubMenagerById(BaseGetRequestIdDto requestDto);
        Task SaveImageForClub(SaveImageRequestDto requestDto);
        Task SaveImageForEvent(SaveImageRequestDto requestDto);
        Task<ImageResponseDto> GetImageForEvent(GetImageRequestDto image);
        Task<ImageResponseDto> GetImageForClub(GetImageRequestDto image);
        Task UpdateImageForEvent(UpdateImageRequestDto image);
        Task UpdateImageForClub(UpdateImageRequestDto image);
        Task DeleteImageForEvent(GetImageRequestDto image);
        Task DeleteImageForClub(GetImageRequestDto image);



    }
}
