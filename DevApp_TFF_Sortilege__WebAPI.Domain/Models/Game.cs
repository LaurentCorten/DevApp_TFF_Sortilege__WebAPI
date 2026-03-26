namespace DevApp_TFF_Sortilege__WebAPI.Domain.Models
{
    public class Game
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;
        public DateTime CreationDate { get; private set; }
        public Member PlayerA {  get; private set; } = default!; //? Réunir les 2 player en 1 liste de player ?
        public Member? PlayerB { get; private set; } = default!;
        public Member? Winner { get; private set; } = default!; //? Enum avec A ou B pour alléger ? Mais alors plus requête plus complexe pour compter le nombre de vistoire d'un joueur donné par contre 🤔

        // ctor EF Core
        private Game() { }


        // ctor front -> db
        public Game (string name, DateTime date, Member joueurA, Member? joueurB, Member? winner)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Trim().Length < 3 || name.Trim().Length > 50)
                throw new ArgumentException("Le nom de la partie doit faire entre 3 et 50 caractères");
            if (date > DateTime.Now)
                throw new ArgumentOutOfRangeException("la partie ne peut pas avoir été créée dans le future ...");

            Name = name;
            CreationDate = date;
            PlayerA = joueurA;
            PlayerB = joueurB;
            Winner = winner;
        }

        // ctor db -> front
        public Game (Guid id, string name, DateTime date, Member joueurA, Member? joueurB, Member? winner) : this()
        {
            Id = id;
        }



    }
}
