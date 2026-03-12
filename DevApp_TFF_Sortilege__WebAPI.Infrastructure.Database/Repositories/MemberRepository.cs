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
        #region DI
        private readonly AppDbContext _appDbContext;

        public MemberRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion


        #region Auth
        public Member Insert(Member newMember)
        {
            Member addedMember = _appDbContext.Add(newMember).Entity;

            _appDbContext.SaveChanges();

            return new Member(addedMember.Id, addedMember.Name, addedMember.Email);
        }

        /// <summary>
        /// Return all but the HashWord of a member or NullException
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Member? GetMemberByEmail(string email)
        {
            try
            {
                Member member = _appDbContext.Members
                    .FirstOrDefault(m => m.Email == email)!;

                return new Member(member.Id, member.Name, member.Email);
            }
            catch (Exception ex)
            {

                throw new NullReferenceException(ex.Message); // TODO : Custom a NotFoundException
            }
        }

        public string? GetHwdByEmail(string email)
        {
            string? hash = _appDbContext.Members
                .FirstOrDefault(m => m.Email == email)?
                .HashWord;

            return hash;
        } 
        #endregion

        public bool CheckNameExists(string name)
        {
            bool nameIsTaken = _appDbContext.Members
                .Any(m => m.Name.ToLower() == name.ToLower());

            return nameIsTaken;
        }

        public bool CheckEmailExists(string email)
        {
            bool emailIsTaken = _appDbContext.Members
                .Any(m => m.Email == email);

            return emailIsTaken;
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
