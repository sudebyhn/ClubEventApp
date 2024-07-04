using Data.DbContextLib;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;

//using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Data
{
    public class Data : IData
    {
        private SQLDbContext _dbContext;
        public Data(SQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddClub(Club club)
        {
            await _dbContext.Clubs.AddAsync(club);
        }
        public async Task AddEvent(Domain.Event _event)
        {
            await _dbContext.Events.AddAsync(_event);
        }

        public async Task<Club> GetClubById(Guid clubId)
        {
            return await _dbContext.Clubs.Where(x => x.ClubId == clubId).FirstOrDefaultAsync();
        }

        public async Task<ClubMenager> GetClubMenagerById(Guid clubMenagerId)
        {
            return await _dbContext.ClubManagers.Where(x => x.ClubManagerId == clubMenagerId).FirstOrDefaultAsync();
        }

        public async Task<List<ClubMenager>> GetClubMenagerList()
        {
            return await _dbContext.ClubManagers.ToListAsync();
        }

        public async Task<List<Club>> GetClubsList()
        {
            return await _dbContext.Clubs.ToListAsync();
        }
        public async Task<List<Domain.Club>> SearchClubBySearchText(BaseGetRequestDto requestDto)
        {
            var querry = _dbContext.Clubs.ToList();
            return querry.Where(x => x.ClubName.Contains(requestDto.Keyword)).Skip(requestDto.Skip).Take(requestDto.Take).OrderBy(g => g.ClubName).ToList();
        }
        public async Task<List<Domain.Event>> SearchEventBySearchText(BaseGetRequestDto requestDto)
        {
            var querry = _dbContext.Events.ToList();
            return querry.Where(x => x.Title.Contains(requestDto.Keyword)).Skip(requestDto.Skip).Take(requestDto.Take).OrderBy(g => g.Title).ToList();
        }
        public async Task<List<Domain.ClubMenager>> SearchClubMenagerBySearchText(BaseGetRequestDto requestDto)
        {
            var querry = _dbContext.ClubManagers.ToList();
            return querry.Where(x => x.Name.Contains(requestDto.Keyword)).Skip(requestDto.Skip).Take(requestDto.Take).OrderBy(g => g.Name).ToList();
        }
        public async Task<Event> GetEventById(Guid eventId)
        {
            return await _dbContext.Events.Where(x => x.EventId == eventId).FirstAsync();
        }

        public async Task<List<Event>> GetEventList()
        {
            return await _dbContext.Events.ToListAsync();
        }

        public async Task<SksAdmin> GetSksAdminById(Guid sksAdminId)
        {
            return await _dbContext.SksAdmin.Where(x => x.SksAdminId == sksAdminId).FirstOrDefaultAsync();
        }

        public void UpdateClub(Club club)
        {
            _dbContext.Clubs.Update(club);
        }
        public void UpdateEvent(Domain.Event _event)
        {
            _dbContext.Events.Update(_event);
        }
        public void UpdateSksAdmin(SksAdmin sksAdmin)

        {
            _dbContext.SksAdmin.Update(sksAdmin);
        }

        public void UpdateClubMenager(ClubMenager clubMenager)
        {
            _dbContext.ClubManagers.Update(clubMenager);
        }

        public async Task DeleteClubById(Guid clubId)
        {
            var clubForDeleting = await _dbContext.Clubs.Where(x => x.ClubId == clubId).FirstAsync();
            _dbContext.Clubs.Remove(clubForDeleting);
        }

        public async Task DeleteEventById(Guid eventId)
        {
            var eventForDeleting = await _dbContext.Events.Where(x => x.EventId == eventId).FirstAsync();
            _dbContext.Events.Remove(eventForDeleting);
        }

        public async Task DeleteClubMenager(Guid clubMenagerId)
        {
            var clubMenagerForDeleting = await _dbContext.ClubManagers.Where(x => x.ClubManagerId == clubMenagerId).FirstAsync();
            _dbContext.Remove(clubMenagerForDeleting);
        }
        public async Task PersistAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateClubMenager(ClubMenager clubMenager)
        {
            await _dbContext.AddAsync(clubMenager);
        }

        public async Task<List<SksAdmin>> GetSksAdmin()
        {
            return await _dbContext.SksAdmin.ToListAsync();
        }

        public async Task SaveImageForEvent(Event image)
        {
            var relatedEvent = await _dbContext.Events.Where(x => x.EventId == image.EventId).FirstAsync();
            relatedEvent.ImageId = Guid.NewGuid();
            relatedEvent.ImageName = image.ImageName;
            relatedEvent.Data = image.Data;
            _dbContext.Update(relatedEvent);
        }

        public async Task SaveImageForClub(Club image)
        {
            var relatedClub = await _dbContext.Clubs.Where(x => x.ClubId == image.ClubId).FirstAsync();
            relatedClub.ImageName = image.ImageName;
            relatedClub.ImageId = Guid.NewGuid();
            relatedClub.Data = image.Data;
            _dbContext.Clubs.Update(relatedClub);
        }

        public async Task<Event> GetImageForEvent(Event image)
        {
            Event relatedEvent = await _dbContext.Events.Where(x => x.ImageId == image.ImageId).FirstAsync();
            return relatedEvent;

        }

        public async Task<Club> GetImageForClub(Club image)
        {
            Club relatedClub = await _dbContext.Clubs.Where(x => x.ImageId == image.ImageId).FirstAsync();
            return relatedClub;

        }

        public async Task UpdateImageForEvent(Event image)
        {
            _dbContext.Events.Update(image);
        }

        public async Task UpdateImageForClub(Club image)
        {
            _dbContext.Clubs.Update(image);
        }

        public async Task DeleteImageForEvent(Event imageId)
        {
            var imageToBeDeleted = await _dbContext.Events.Where(x => x.ImageId == imageId.ImageId).FirstAsync();
            imageId.ImageId = Guid.Empty;
            imageId.Data = null;
            imageId.ImageName = string.Empty;
        }

        public async Task DeleteImageForClub(Club imageId)
        {
            var imageToBeDeleted = await _dbContext.Clubs.Where(x => x.ImageId == imageId.ImageId).FirstAsync();
            imageId.ImageId = Guid.Empty;
            imageId.Data = null;
            imageId.ImageName = string.Empty;
        }
    }
}
