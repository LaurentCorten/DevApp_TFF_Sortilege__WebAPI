using DevApp_TFF_Sortilege__WebAPI.Domain.Models;

namespace DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Repositories
{
    public interface IMemberRepository
    {
        // C
        Task<Member> InsertAsync(Member newMember);

        // R
        Task<Member> GetMemberByIdAsync(Guid memberId);
        Task<Member> GetMemberByEmailAsync(string email);
        Task<string?> GetHwdByEmailAsync(string email);
        Task<bool> CheckNameExistsAsync(string name);
        Task<bool> CheckEmailExistsAsync(string email);

        //U
        Task<Member> UpdateAsync(Guid id, Member modifiedMember);

        //D
        Task<bool> DeleteByIdAsync(Guid id);
    }
}
