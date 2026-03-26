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
        public async Task<Member> InsertAsync(Member newMember)
        {
            EntityEntry<Member> result = await _appDbContext.AddAsync(newMember);

            await _appDbContext.SaveChangesAsync();

            return new Member(result.Entity.Id, result.Entity.Name, result.Entity.Email);
        }

        /// <summary>
        /// Return all but the HashWord of a member or NullException
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Member</returns>
        public async Task<Member> GetMemberByEmailAsync(string email)
        {
            try
            {
                Member member = (await _appDbContext.Members
                    .FirstOrDefaultAsync(m => m.Email == email))!; //TODO : Question : pq il ne prends pas le '!' en Async ??

                return new Member(member.Id, member.Name, member.Email);
            }
            catch (Exception ex)
            {

                throw new NullReferenceException(ex.Message); // TODO : Custom a NotFoundException
            }
        }

        public async Task<string?> GetHwdByEmailAsync(string email)
        {
            string? hash = (await _appDbContext.Members
                .FirstOrDefaultAsync(m => m.Email == email))?
                .HashWord;

            return hash;
        } 
        #endregion

        public async Task<bool> CheckNameExistsAsync(string name)
        {
            bool nameIsTaken = await _appDbContext.Members
                .AnyAsync(m => m.Name.ToLower() == name.ToLower());

            return nameIsTaken;
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            bool emailIsTaken = await _appDbContext.Members
                .AnyAsync(m => m.Email == email);

            return emailIsTaken;
        }

        public async Task<Member> UpdateAsync(Guid id, Member modifiedMember)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
