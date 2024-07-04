using AppService.Dto;
using AppService.Validators;
using AutoMapper;
using Data;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppService
{
    public class AppService : IAppService
    {
        private readonly IData _data;
        private readonly IMapper _mapper;
        public AppService(IData data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }
        public async Task CreateClubMenager(CreateClubMenagerRequestDto requestDto)
        {
            //yetki kontrolü lazım diğerleri gibi
            var checkIfUserIsSksAdmin = await _data.GetSksAdminById(requestDto.SksAdminId);
            ValidationContext<CreateClubMenagerRequestDto> validationContext = new(requestDto);
            var validator = new CreateClubMenagerValidation();
            var validationResult = validator.Validate(validationContext);
            if (validationResult.IsValid is true && checkIfUserIsSksAdmin is not null)
            {
                var clubMenager = _mapper.Map<ClubMenager>(requestDto);
                await _data.CreateClubMenager(clubMenager);
                await _data.PersistAsync();
            }
            else if (validationResult.IsValid is false)
            {
                throw new Exception(validationResult.Errors.ToString());

            }
            else if (checkIfUserIsSksAdmin is null)
            {
                throw new Exception("You are not allowed to do this action");
            }
        }

        public async Task<Guid> LoginForClubMenager(string email, string password)
        {
            var clubMenagertList = _data.GetClubMenagerList().Result;
            var checkUser = clubMenagertList.Exists(x => x.Email == email && x.Password == password);
            if (!checkUser)
            {
                throw new Exception("Wrong email or password.Please try again");
            }
            else
            {
                var retval = _data.GetClubMenagerList().Result.Where(x => x.Email == email && x.Password == password).Select(x => x.ClubManagerId).First();
                return retval;
            }
        }

        public async Task<Guid> LoginForSksAdmin(string email, string password)
        {
            var sksAdmin = _data.GetSksAdmin().Result;
            var checkUser = sksAdmin.Exists(x => x.Email == email && x.Password == password);
            if (!checkUser)
            {
                throw new Exception("Wrong email or password.Please try again");
            }
            else
            {
                var retval = _data.GetSksAdmin().Result.Where(x => x.Email == email && x.Password == password).Select(x => x.SksAdminId).First();
                return retval;
            }
        }
        public async Task CreateClub(CreateClubRequestDto requestDto)
        {
            ValidationContext<CreateClubRequestDto> validationContext = new(requestDto);
            var validator = new CreateClubValidation();
            var validationResult = validator.Validate(validationContext);
            var checkIfUserIsSksAdmin = _data.GetSksAdminById(requestDto.SksAdminId).Result;
            var clubList = _data.GetClubsList().Result;
            var checkIfNameIsNotUnique = clubList.Exists(x => x.ClubName == requestDto.ClubName);

            if (checkIfUserIsSksAdmin is not null && validationResult.IsValid is true && checkIfNameIsNotUnique is false)
            {
                var club = _mapper.Map<Club>(requestDto);
                await _data.AddClub(club);
                await _data.PersistAsync();
            }
            else if (validationResult.IsValid is false)
            {
                throw new Exception(validationResult.Errors.ToString());
            }
            else if (checkIfUserIsSksAdmin is null)
            {
                throw new Exception("You are not allowed to create a club");
            }
            else if (checkIfNameIsNotUnique is true)
            {
                throw new Exception("Please enter a unique name for club");
            }
        }
        public async Task DeleteClub(DeleteClubRequestDto requestDto)
        {
            var checkIfUserIsSksAdmin = _data.GetSksAdminById(requestDto.SksAdminId).Result;
            var clubToBeDeleted = _data.GetClubsList().Result.Where(x => x.ClubId == requestDto.ClubId).FirstOrDefault();
            if (checkIfUserIsSksAdmin is not null && clubToBeDeleted is not null)
            {
                await _data.DeleteClubById(clubToBeDeleted.ClubId);
                await _data.PersistAsync();

            }
            else if (checkIfUserIsSksAdmin is null)
            {
                throw new Exception("You are not allowed to delete club");
            }
            else if (clubToBeDeleted is null)
            {
                throw new Exception("Club couldn't found to be deleted");
            }
        }

        public async Task UpdateClub(UpdateClubRequestDto requestDto)
        {
            /*
             yapsak iyi olur:club bazlı request geldiğinde ekrandan aynı zamanda club menager name'ini talep ederiz o talep sayesinde club menager Id'yi 
            alırız. sonrasında aldığımız club objesi ve elde ettiğimiz club menager Id uyuşuyor ise işlemi yaptırırız değilse yetkin yok deriz
            sks admin her türlü yapar
             */
            ValidationContext<UpdateClubRequestDto> validationContext = new(requestDto);
            var validator = new UpdateClubValidation();
            var validationResult = validator.Validate(validationContext);
            var clubToBeUpdated = _data.GetClubsList().Result.Where(x => x.ClubId == requestDto.Id).FirstOrDefault();
            if (clubToBeUpdated is not null && validationResult.IsValid is true)
            {
                var updatingClub = _mapper.Map<Club>(requestDto);
                clubToBeUpdated.ClubName = updatingClub.ClubName;
                clubToBeUpdated.FacultyName= updatingClub.FacultyName;
                _data.UpdateClub(clubToBeUpdated);
                await _data.PersistAsync();
            }
            else if (validationResult.IsValid is false)
            {
                throw new Exception(validationResult.Errors.ToString());
            }
            else
            {
                throw new Exception("The club that you are trying to update couldn't be found");
            }

        }

        public async Task CreateEvent(CreateEventRequestDto requestDto)
        {
            /*
             yaratılan event default olarak waiting Id'de oluşsun eğer sks adminse onaylayıp processId'yi değiştirebilsin
             */
            ValidationContext<CreateEventRequestDto> validationContext = new(requestDto);
            var validator = new CreateEventValidation();
            var validationResult = validator.Validate(validationContext);
            var eventToBeCreated = _mapper.Map<Event>(requestDto);
            var checkIfUserIsSksAdmin = _data.GetSksAdminById(requestDto.SksAdminId).Result;
            if (checkIfUserIsSksAdmin is not null && validationResult.IsValid is true)
            {
                eventToBeCreated.EventSituationId = 1;
                await _data.AddEvent(eventToBeCreated);
                await _data.PersistAsync();
            }
            else if (validationResult.IsValid is true && checkIfUserIsSksAdmin is null)
            {
                eventToBeCreated.EventSituationId = 3;
                await _data.AddEvent(eventToBeCreated);
                await _data.PersistAsync();
            }
            else if (validationResult.IsValid is false)
            {
                throw new Exception(validationResult.Errors.ToString());
            }


        }

        public async Task<object> DeleteEvent(DeleteEventRequestDto requestDto)
        {
            /*
             Sks admin istek attıysa direkt silsin değilse update event mekanizması çalışsın
             */
            var checkIfUserIsSksAdmin = _data.GetSksAdminById(requestDto.SksAdminId).Result;
            var eventToBeDeleted = _data.GetEventList().Result.Where(x => x.EventId == requestDto.EventId).FirstOrDefault();

            if (checkIfUserIsSksAdmin is not null && eventToBeDeleted is not null)
            {
                await _data.DeleteEventById(eventToBeDeleted.EventId);
                await _data.PersistAsync();
                return 0;
            }
            else if (eventToBeDeleted is null)
            {
                throw new Exception("The event that you are trying to delete couldn't be found");
            }
            else
            {
                var eventToBeDeletedByMenager = await _data.GetEventById(requestDto.EventId);
                return eventToBeDeletedByMenager;
            }
        }

        public async Task<object> UpdateEvent(UpdateEventRequestDto requestDto)
        {
            /*
             İsteğin hangi taraftan geldiğini önceki metotlar gibi kontrol edeceğiz sonrasında eğer bu istek menagerdansa response'ta requestten gelen
            değişiklik isteklerini dönceğiz eğer sks admin idli bir istek varsa ortada o zaman else koşulunda değişiklikleri yapıp save changes diyerek 
            db'ye kayıt atacağız.Else koşulunda process situationId'yi güncelleyeceğiz.
             */
            ValidationContext<UpdateEventRequestDto> validationContext = new(requestDto);
            var validator = new UpdateEventValidation();
            var validationResult = validator.Validate(validationContext);
            var checkIfUserIsSksAdmin = _data.GetSksAdminById(requestDto.SksAdminId).Result;
            var eventToBeUpdated = _data.GetEventList().Result.Where(x => x.EventId == requestDto.EventId).FirstOrDefault();
            if (checkIfUserIsSksAdmin is not null && eventToBeUpdated is not null && validationResult.IsValid is true)
            {
                var _event = _data.GetEventById(eventToBeUpdated.EventId).Result;
                _event.Title = requestDto.Title;
                _event.Text = requestDto.Text;
                _event.EventDate = requestDto.EventDate;
                _event.EventSituationId = requestDto.EventSituationId;
                await _data.PersistAsync();
                return 0;

            }
            else if (eventToBeUpdated is null)
            {
                throw new Exception("Event to be updated couldn't be found");
            }
            else if (validationResult.IsValid is false)
            {
                throw new Exception(validationResult.Errors.ToString());
            }
            else
            {
                return requestDto;
            }
        }

        public async Task DeleteClubMenager(DeleteClubMenagerRequestDto requestDto)
        {
            /*
             yapsak iyi olur:club bazlı request geldiğinde ekrandan aynı zamanda club menager name'ini talep ederiz o talep sayesinde club menager Id'yi 
            alırız. sonrasında aldığımız club objesi ve elde ettiğimiz club menager Id uyuşuyor ise işlemi yaptırırız değilse yetkin yok deriz
            sks admin her türlü yapar
             */
            var clubMenagerToBeUpdated = _data.GetClubMenagerList().Result.Where(x => x.ClubManagerId == requestDto.ClubMenagerId).FirstOrDefault();
            if (clubMenagerToBeUpdated is not null)
            {
                await _data.DeleteClubMenager(clubMenagerToBeUpdated.ClubManagerId);
                await _data.PersistAsync();

            }
            else
            {
                throw new Exception("The club menager that you are trying to update couldn't be found");
            }
        }

        public async Task UpdateClubMenager(UpdateClubMenagerRequestDto requestDto)
        {
            /*
              yapsak iyi olur:club bazlı request geldiğinde ekrandan aynı zamanda club menager name'ini talep ederiz o talep sayesinde club menager Id'yi 
              alırız. sonrasında aldığımız club objesi ve elde ettiğimiz club menager Id uyuşuyor ise işlemi yaptırırız değilse yetkin yok deriz
              sks admin her türlü yapar
            */
            ValidationContext<UpdateClubMenagerRequestDto> validationContext = new(requestDto);
            var validator = new UpdateClubMenagerValidation();
            var validationResult = validator.Validate(validationContext);
            var clubMenagerToBeUpdated = _data.GetClubMenagerList().Result.Where(x => x.ClubManagerId == requestDto.ClubMenagerId).FirstOrDefault();
            if (clubMenagerToBeUpdated is not null && validationResult.IsValid is true)
            {
                var updatingCLubMenager = _mapper.Map<ClubMenager>(requestDto);
                clubMenagerToBeUpdated.Name = updatingCLubMenager.Name;
                clubMenagerToBeUpdated.Surname = updatingCLubMenager.Surname;
                clubMenagerToBeUpdated.Password = updatingCLubMenager.Password;
                clubMenagerToBeUpdated.Email = updatingCLubMenager.Email;

                _data.UpdateClubMenager(clubMenagerToBeUpdated);
                await _data.PersistAsync();

            }
            else if (validationResult.IsValid is false)
            {
                throw new Exception(validationResult.Errors.ToString());
            }
            else
            {
                throw new Exception("The club that you are trying to update couldn't be found");
            }
        }

        public async Task<List<GetClubBySearchResponseDto>> SearchClubBySearchText(BaseGetRequestDto requestDto)
        {
            var clubList = await _data.SearchClubBySearchText(requestDto);
            var returnList = _mapper.Map<List<GetClubBySearchResponseDto>>(clubList);
            return returnList;
        }

        public async Task<List<GetEventBySearchResponseDto>> SearchEventBySearchText(BaseGetRequestDto requestDto)
        {
            var eventList = await _data.SearchEventBySearchText(requestDto);
            var returnList = _mapper.Map<List<GetEventBySearchResponseDto>>(eventList);
            return returnList;
        }

        public async Task<List<GetClubMenagerBySearchResponseDto>> SearchClubMenagerBySearchText(BaseGetRequestDto requestDto)
        {
            var clubMenagerList = await _data.SearchClubMenagerBySearchText(requestDto);
            var returnList = _mapper.Map<List<GetClubMenagerBySearchResponseDto>>(clubMenagerList);
            return returnList;
        }

        public Task<List<Club>> GetClubs()
        {
            return _data.GetClubsList();
        }

        public Task<List<Event>> GetEvents()
        {
            return _data.GetEventList();
        }

        public Task<List<ClubMenager>> GetClubMenagers()
        {
            return _data.GetClubMenagerList();
        }

        public async Task<Club> GetClubById(BaseGetRequestIdDto requestDto)
        {
            return await _data.GetClubById(requestDto.Id);
        }

        public async Task<Event> GetEventById(BaseGetRequestIdDto requestDto)
        {
            return await _data.GetEventById(requestDto.Id);
        }

        public async Task<ClubMenager> GetClubMenagerById(BaseGetRequestIdDto requestDto)
        {
            return await _data.GetClubMenagerById(requestDto.Id);
        }
        public async Task SaveImageForClub(SaveImageRequestDto requestDto)
        {
            var _image = _mapper.Map<Club>(requestDto);
            await _data.SaveImageForClub(_image);
            await _data.PersistAsync();
        }
        public async Task SaveImageForEvent(SaveImageRequestDto requestDto)
        {
            var _image = _mapper.Map<Event>(requestDto);
            await _data.SaveImageForEvent(_image);
            await _data.PersistAsync();
        }

        public async Task<ImageResponseDto> GetImageForEvent(GetImageRequestDto image)
        {
            var eventForImageEntity=_data.GetEventList().Result.Where(x=>x.ImageId==image.ImageId).First();
            var imageEntity = await _data.GetImageForEvent(eventForImageEntity);
            var retval=_mapper.Map<ImageResponseDto>(imageEntity);
            return retval;
           
        }

        public async Task<ImageResponseDto> GetImageForClub(GetImageRequestDto image)
        {
            var clubForImageEntity = _data.GetClubsList().Result.Where(x => x.ImageId == image.ImageId).First();
            var imageEntity = await _data.GetImageForClub(clubForImageEntity);
            var retval = _mapper.Map<ImageResponseDto>(imageEntity);
            return retval;
        }

        public async Task UpdateImageForEvent(UpdateImageRequestDto image)
        {
            var eventForImageEntity = await _data.GetEventList();
            var imageEntityToUpdate= eventForImageEntity.Where(x=>x.ImageId == image.ImageId).First();
            var imageEntity = await _data.GetImageForEvent(imageEntityToUpdate);
            if(imageEntity is not null)
            {
                imageEntity.Data = image.Data;
                imageEntity.ImageName = image.ImageName;
                await _data.UpdateImageForEvent(imageEntity);
                await _data.PersistAsync();
            }
        }

        public async Task UpdateImageForClub(UpdateImageRequestDto image)
        {
            var eventForImageEntity = await _data.GetClubsList();
            var imageEntityToUpdate = eventForImageEntity.Where(x => x.ImageId == image.ImageId).First();
            var imageEntity = await _data.GetImageForClub(imageEntityToUpdate);
            if (imageEntity is not null)
            {
                imageEntity.Data = image.Data;
                imageEntity.ImageName = image.ImageName;
                await _data.UpdateImageForClub(imageEntity);
                await _data.PersistAsync();
            }
        }

        public async Task DeleteImageForEvent(GetImageRequestDto image)
        {
            var eventForImageEntity = _data.GetEventList().Result.Where(x => x.ImageId == image.ImageId).First();
            var imageEntity = await _data.GetImageForEvent(eventForImageEntity);
            await _data.DeleteImageForEvent(imageEntity);
            await _data.PersistAsync();
        }

        public async Task DeleteImageForClub(GetImageRequestDto image)
        {
            var clubForImageEntity = _data.GetClubsList().Result.Where(x => x.ImageId == image.ImageId).First();
            var imageEntity = await _data.GetImageForClub(clubForImageEntity);
            await _data.DeleteImageForClub(imageEntity);
            await _data.PersistAsync();
        }
    }
}
