using DevApp_TFF_Sortilege__WebAPI.Domain.Models;

namespace DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Services
{
    // Interface de communication entre l'appCore et le Domain
    public interface IMemberService
    {
        // C
        public Task<Member> RegisterAsync(Member newMember);

        // R
        public Task<Member> LoginAsync(string email, string password);

        //U
        public Task<Member> UpdateAsync(Member modifiedMember);

        //D
        public Task<bool> DeleteAsync(string email, string password);
    }
}
