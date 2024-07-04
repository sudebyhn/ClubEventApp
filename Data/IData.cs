using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IData
    {
        Task<Club> GetClubById(Guid clubId);
        Task<List<Club>> GetClubsList();
        Task DeleteClubById(Guid clubId);
        Task DeleteEventById(Guid eventId);
        Task DeleteClubMenager(Guid clubMenagerId);
        Task CreateClubMenager(ClubMenager clubMenager);
        void UpdateClub(Club club);
        Task AddClub(Club club);
        Task<ClubMenager> GetClubMenagerById(Guid clubMenagerId);
        Task<List<ClubMenager>> GetClubMenagerList();
        Task<Event> GetEventById(Guid eventId);
        Task<List<Event>> GetEventList();
        void UpdateEvent(Domain.Event _event);
        void UpdateSksAdmin(SksAdmin sksAdmin);
        void UpdateClubMenager(ClubMenager clubMenager);
        Task<SksAdmin> GetSksAdminById(Guid sksAdminId);
        Task<List<SksAdmin>> GetSksAdmin();
        Task PersistAsync();
        Task AddEvent(Domain.Event _event);
        Task<List<Club>> SearchClubBySearchText(BaseGetRequestDto requestDto);
        Task<List<Domain.Event>> SearchEventBySearchText(BaseGetRequestDto requestDto);
        Task<List<Domain.ClubMenager>> SearchClubMenagerBySearchText(BaseGetRequestDto requestDto);
        Task SaveImageForEvent(Domain.Event image);
        Task SaveImageForClub(Domain.Club image);
        Task<Event> GetImageForEvent(Domain.Event image);
        Task<Club> GetImageForClub(Club club);
        Task UpdateImageForEvent(Domain.Event image);
        Task UpdateImageForClub(Domain.Club club);
        Task DeleteImageForEvent(Domain.Event image);
        Task DeleteImageForClub(Domain.Club image);


    }
}
