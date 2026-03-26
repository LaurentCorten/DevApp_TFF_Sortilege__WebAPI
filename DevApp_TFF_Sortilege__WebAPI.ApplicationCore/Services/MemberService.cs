using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Repositories;
using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Services;
using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using Soenneker.Hashing.Argon2;

namespace DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Services
{
    public class MemberService : IMemberService
    {
        #region DI
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        #endregion

        #region Auth
        public async Task<Member> RegisterAsync(Member newMember) // Attention Name & email UNIQUE
        {
            // Check Unicity Rules
            if (await _memberRepository.CheckEmailExistsAsync(newMember.Email))
                throw new ArgumentException("Cet email a déjà un compte associé !"); // TODO : Custom Error !
            if (await _memberRepository.CheckNameExistsAsync(newMember.Name))
                throw new ArgumentException("Ce Pseudo est déjà pris !");           // TODO : Custom Error !          

            // Hash the Password
            string hashWord = await Argon2HashingUtil.Hash(newMember.HashWord!);      

            // Immutable since DDD => new instance
            Member MemberToAdd = new Member(newMember.Name, newMember.Email, hashWord);

            // Send to Repo
            Member addedMember = await _memberRepository.InsertAsync(MemberToAdd);

            return addedMember;
        }

        public async Task<Member> LoginAsync(string email, string password)
        {
            // Check that we recieved actual data
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Un ou plusieurs champs manquant(s) !");

            // Try to get the HashWord from DB
            string? hash = await _memberRepository.GetHwdByEmailAsync(email)!;

            // If it's null the email is unknown => Exception
            if (hash is null)
                throw new ArgumentException("Association Login / Mot de passe erronée !"); // TODO : Custom BadCredentialsException

            // If it isn't null we check if it's the good one
            if (!Argon2HashingUtil.Verify(password, hash).Result) 
                throw new ArgumentException("Association Login / Mot de passe erronée !"); // TODO : Custom BadCredentialsException

            // Since all went well let's send what's expected
            return await _memberRepository.GetMemberByEmailAsync(email);
        } 
        #endregion

        public async Task<Member> UpdateAsync(Member modifiedMember)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
