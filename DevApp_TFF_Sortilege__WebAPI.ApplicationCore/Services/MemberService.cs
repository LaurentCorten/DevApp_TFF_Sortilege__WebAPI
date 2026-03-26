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
        public Member Register(Member newMember) // Attention Name et email UNIQUE
        {
            // Check Unicity Rules
            if (_memberRepository.CheckEmailExists(newMember.Email))
                throw new ArgumentException("Cet email a déjà un compte associé !"); // TODO : Custom Error !
            if (_memberRepository.CheckNameExists(newMember.Name))
                throw new ArgumentException("Ce Pseudo est déjà pris !");

            // Hash the Password
            string hashWord = Argon2HashingUtil.Hash(newMember.HashWord!).Result; // TODO : Question : Pq dans la doc ça dit : string hash = await Argon2HashingUtil.Hash(password) mais qu'ici il ne veut pas ??? 

            // Immutable since DDD => new instance
            Member MemberToAdd = new Member(newMember.Name, newMember.Email, hashWord);

            // Send to Repo
            Member addedMember = _memberRepository.Insert(MemberToAdd);

            return addedMember;
        }

        public Member Login(string email, string password)
        {
            // Check that we recieved actual data
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) // TODO : Question : À priori impossible vu que le constructeur ne le permet pas, du coup est-cee que ça vaut la peine ? Voire pire, est ce que ce n'est pas contre productif ??
                throw new ArgumentNullException("Un ou plusieurs champs manquant(s) !");

            // Try to get the HashWord from DB
            string? hash = _memberRepository.GetHwdByEmail(email)!;

            // If it's null the email is unknown => Exception
            if (hash is null)
                throw new ArgumentException("Association Login / Mot de passe erronée !"); // TODO : Custom BadCredentialsException

            // If it isn't null we check if it's the good one
            if (!Argon2HashingUtil.Verify(password, hash).Result) 
                throw new ArgumentException("Association Login / Mot de passe erronée !"); // TODO : Custom BadCredentialsException

            // Since all went well let's send what's expected
            return _memberRepository.GetMemberByEmail(email)!;
        } 
        #endregion

        public Member Update(Member modifiedMember)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
