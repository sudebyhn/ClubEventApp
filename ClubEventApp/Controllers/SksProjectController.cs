using AppService;
using AppService.Dto;
using Data;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace ClubEventApp.Controllers
{
    [Route("api/SksProject/[action]")]
    [ApiController]
    public class SksProjectController : ControllerBase
    {
        private readonly IAppService _appService;
        public SksProjectController(IAppService appService)
        {
            _appService = appService;
        }
        [HttpGet]
        public async Task<Guid> LoginForSksAdmin(string email, string password)
        {
            return await _appService.LoginForSksAdmin(email, password);
        }
        [HttpGet]
        public async Task<Guid> LoginForClubMenager(string email, string password)
        {
            return await _appService.LoginForClubMenager(email, password);
        }
        [HttpPost]
        public async Task CreateClub(CreateClubRequestDto requestDto)
        {
            await _appService.CreateClub(requestDto);
        }
        [HttpDelete]
        public async Task DeleteClub(DeleteClubRequestDto requestDto)
        {
            await _appService.DeleteClub(requestDto);
        }
        [HttpPatch]
        public async Task UpdateClub(UpdateClubRequestDto requestDto)
        {
            await _appService.UpdateClub(requestDto);
        }
        [HttpPost]
        public async Task CreateEvent(CreateEventRequestDto requestDto)
        {
            await _appService.CreateEvent(requestDto);
        }
        [HttpDelete]
        public async Task<object> DeleteEvent(DeleteEventRequestDto requestDto)
        {
            return await _appService.DeleteEvent(requestDto);
        }
        [HttpPatch]
        public async Task<object> UpdateEvent(UpdateEventRequestDto requestDto)
        {
            return await _appService.UpdateEvent(requestDto);
        }
        [HttpPost]
        public async Task CreateClubMenager(CreateClubMenagerRequestDto requestDto)
        {
            await _appService.CreateClubMenager(requestDto);
        }
        [HttpDelete]
        public async Task DeleteClubMenager(DeleteClubMenagerRequestDto requestDto)
        {
            await _appService.DeleteClubMenager(requestDto);
        }
        [HttpPatch]
        public async Task UpdateClubMenager(UpdateClubMenagerRequestDto requestDto)
        {
            await _appService.UpdateClubMenager(requestDto);
        }
        [HttpPost]
        public async Task<List<GetClubBySearchResponseDto>> SearchClubBySearchText(BaseGetRequestDto requestDto)
        {
            return await _appService.SearchClubBySearchText(requestDto);
        }
        [HttpPost]
        public async Task<List<GetEventBySearchResponseDto>> SearchEventBySearchText(BaseGetRequestDto requestDto)
        {
            return await _appService.SearchEventBySearchText(requestDto);
        }
        [HttpPost]
        public async Task<List<GetClubMenagerBySearchResponseDto>> SearchClubMenagerBySearchText(BaseGetRequestDto requestDto)
        {
            return await _appService.SearchClubMenagerBySearchText(requestDto);
        }
        [HttpGet]
        public async Task<List<Club>> GetClubs()
        {
            return await _appService.GetClubs();
        }
        [HttpGet]
        public async Task<List<Domain.Event>> GetEvents()
        {
            return await _appService.GetEvents();
        }
        [HttpGet]
        public async Task<List<Domain.ClubMenager>> GetClubMenagers()
        {
            return await _appService.GetClubMenagers();
        }
        [HttpPost]
        public async Task<Club> GetClubById(BaseGetRequestIdDto requestDto)
        {
            return await _appService.GetClubById(requestDto);
        }
        [HttpPost]
        public async Task<Domain.Event> GetEventById(BaseGetRequestIdDto requestDto)
        {
            return await _appService.GetEventById(requestDto);
        }
        [HttpPost]
        public async Task<Domain.ClubMenager> GetClubMenagerById(BaseGetRequestIdDto requestDto)
        {
            return await _appService.GetClubMenagerById(requestDto);
        }
        [HttpPost]
        public async Task SaveImageForClub(SaveImageRequestDto requestDto)
        {
            await _appService.SaveImageForClub(requestDto);
        }
        [HttpPost]
        public async Task SaveImageForEvent(SaveImageRequestDto requestDto)
        {
            await _appService.SaveImageForEvent(requestDto);
        }
        [HttpPost]
        public async Task<ImageResponseDto> GetImageForEvent(GetImageRequestDto image)
        {
            return await _appService.GetImageForEvent(image);
        }
        [HttpPost]
        public async Task<ImageResponseDto> GetImageForClub(GetImageRequestDto image)
        {
            return await _appService.GetImageForClub(image);
        }
        [HttpPatch]
        public async Task UpdateImageForEvent(UpdateImageRequestDto image)
        {
            await _appService.UpdateImageForEvent(image);
        }
        [HttpPatch]
        public async Task UpdateImageForClub(UpdateImageRequestDto image)
        {
            await UpdateImageForClub(image);
        }
        [HttpDelete]
        public async Task DeleteImageForEvent(GetImageRequestDto image)
        {
            await _appService.DeleteImageForEvent(image);
        }
        [HttpDelete]
        public async Task DeleteImageForClub(GetImageRequestDto image)
        {
            await _appService.DeleteImageForClub(image);
        }
    }
}
