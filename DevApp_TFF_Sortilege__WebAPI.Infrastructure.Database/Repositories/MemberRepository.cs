using DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Repositories;
using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevApp_TFF_Sortilege__WebAPI.Infrastructure.Database.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        public Member Insert(Member newMember)
        {
            throw new NotImplementedException();
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
