using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Repositories;
using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Services;
using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using Soenneker.Hashing.Argon2;

namespace DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public Member Login(string email, string password)
        {
            throw new NotImplementedException();
        }

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
