using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Services;
using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Services
{
    internal class MemberService : IMemberService
    {
        public Member Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Member Register(Member newMember)
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
