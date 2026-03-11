using DevApp_TFF_Sortilege__WebAPI.Domain.Models;

namespace DevApp_TFF_Sortilege__WebAPI.ApplicationCore.Interfaces.Services
{
    // Interface de communication entre l'appCore et le Domain
    public interface IMemberService
    {
        // C
        public Member Register(Member newMember);

        // R
        public Member Login(string email, string password);

        //U
        public Member Update(Member modifiedMember);

        //D
        public bool Delete(string email, string password);
    }
}
