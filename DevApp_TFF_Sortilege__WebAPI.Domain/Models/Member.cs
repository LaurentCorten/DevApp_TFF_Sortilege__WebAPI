using System.Net.Mail;

namespace DevApp_TFF_Sortilege__WebAPI.Domain.Models
{
    public class Member
    {                
        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string? HashWord { get; private set; }


        // ctor EF Core
        private Member() { }

        // ctor dto front -> db 
        public Member(string name, string email, string hashWord)
        {
            // Test de garde pour s'assurer d'avoir reçu un pseudo qui pourra rentrer dans l'emplacement mémoir prévu en db
            if ( string.IsNullOrWhiteSpace(name) || name.Trim().Length < 3 || name.Trim().Length > 50 )
                throw new ArgumentException("Le pseudo dois faire entre 3 et 50 caractères !", nameof(name));

            // Test de garde pour s'assurer d'avoir reçu un email au format valide
            if (string.IsNullOrWhiteSpace(email) || !MailAddress.TryCreate(email.Trim(), out _) || email.Trim().Length > 320)
                throw new ArgumentException("Email invalide !", nameof(email));

            // Test de garde pour vérifier qu'on a bien reçu un mdp
            if (string.IsNullOrWhiteSpace(hashWord))
                throw new ArgumentException("mot de passe vide ! O__o", nameof(hashWord));

            Name = name.Trim();
            Email = email.Trim().ToLower();
            HashWord = hashWord;
        }

        // ctor dto db -> front
        public Member(Guid id, string name, string email) 
        {
            Id = id;
            Name = name; 
            Email = email; 
        }

    }
}
