using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Services;
using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Services
{
    public class MemberService : IMemberService
    {
        public Member Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Member Register(Member newMember) // Attention Name et email UNIQUE et requis den DB ! => default name = "User" + rand()*10000000 p.e.
        {
            throw new NotImplementedException();
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
