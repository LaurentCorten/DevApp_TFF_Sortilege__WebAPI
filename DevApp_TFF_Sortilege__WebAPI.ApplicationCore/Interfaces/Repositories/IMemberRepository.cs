using DevApp_TFF_Sortilege__WebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Repositories
{
    public interface IMemberRepository
    {
        // C
        Member Insert(Member newMember);

        // R
        Member? GetMemberByEmail(string email);
        string? GetHwdByEmail(string email);

        //U
        Member Update(Guid id, Member modifiedMember);

        //D
        bool DeleteById(Guid id);
    }
}
