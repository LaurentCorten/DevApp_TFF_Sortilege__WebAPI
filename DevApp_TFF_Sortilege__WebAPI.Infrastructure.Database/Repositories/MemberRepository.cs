using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Repositories;
using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Xml.Linq;

namespace DevApp_TFF_Sortilege__WebAPI.Infrastructure.Database.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        // DI
        private readonly AppDbContext _appDbContext;

        public MemberRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public Member Insert(Member newMember)
        {
            Member addedMember = _appDbContext.Add(newMember).Entity;

            _appDbContext.SaveChanges();

            return new Member(addedMember.Id, addedMember.Name, addedMember.Email);
        }

        public bool CheckNameExists(string name)
        {
            bool nameIsTaken = _appDbContext.Members
                .AsNoTracking()
                .Any(m => m.Name.ToLower() == name.ToLower());

            return nameIsTaken;
        }

        public bool CheckEmailExists(string email)
        {
            bool emailIsTaken = _appDbContext.Members
                .AsNoTracking()
                .Any(m => m.Email == email);

            return emailIsTaken;
        }

        public Member? GetMemberByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public string? GetHwdByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Member Update(Guid id, Member modifiedMember)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
