using DevApp_TFF_Sortilege__WebAPI.Domain.Models;

namespace DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Repositories
{
    public interface IMemberRepository
    {
        // C
        Member Insert(Member newMember);

        // R
        Member? GetMemberByEmail(string email);
        string? GetHwdByEmail(string email);
        bool CheckNameExists(string name);
        bool CheckEmailExists(string email);

        //U
        Member Update(Guid id, Member modifiedMember);

        //D
        bool DeleteById(Guid id);
    }
}
