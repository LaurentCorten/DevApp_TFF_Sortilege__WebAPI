using System.Net.Mail;

namespace DevApp_TFF_Sortilege__WebAPI.Domain.Models
{
    public class Member
    {
        // TODO : Question : Pq au début on nous a appris à passer par variable privée _nomVarPrivée qui sera remplie par le ctor (_nomVarPrivée = nomVarCtor) puis utilisée par un setter manuel set {NomParam = _nomVarPrivée} ???
        
        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!; // TODO : Question : Au final il vaut mieux mettre le Default ou forcer l'ignore de l'acert ou juste s'en foutre ???
        public string Email { get; private set; } = default!;
        public string? HashWord { get; private set; }


        // ctor EF Core
        private Member() { }

        // ctor 'aller' pour contenir les params membres qui doivent aller du front à la db 
        public Member(string name, string email, string hashWord) // TODO : Question : Pq autoriser l'utilisation du constructeur sans mdp ou avec un mdp null ?
        {
            // Test de garde pour s'assurer d'avoir reçu un pseudo qui pourra rentrer dans l'emplacement mémoir prévu en db
            if (name is not null && (name.Trim().Length < 3 || name.Trim().Length > 50 ))
                throw new ArgumentException("Le pseudo dois faire entre 3 et 50 caractères !", nameof(name));

            // Test de garde pour s'assurer d'avoir reçu un email au format valide
            if (string.IsNullOrWhiteSpace(email) || !MailAddress.TryCreate(email, out _))
                throw new ArgumentException("Email invalide !", nameof(email));

            // Test de garde pour vérifier qu'on a bien reçu un mdp
            if (string.IsNullOrWhiteSpace(hashWord))
                throw new ArgumentException("mot de passe vide ! O__o", nameof(hashWord));
            
            Name = name.Trim();
            Email = email.ToLower();
            HashWord = hashWord;
        }

        // ctor 'retour' pour contenir les params membres qui doivent aller de la db vers le front
        public Member(Guid id, string name, string email) 
        {
            Id = id;
            Name = name; 
            Email = email; 
        }

    }
}
