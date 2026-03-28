namespace DevApp_TFF_Sortilege__WebAPI.Domain.Models
{
    public class Game
    {
        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }
        public Member InvokerA {  get; private set; } = default!; //? Réunir les 2 player en 1 liste de player ?
        public Member InvokerB { get; private set; } = default!;
        public Member? Winner { get; private set; } = default!; //? Enum avec A ou B pour alléger ? Mais alors plus requête plus complexe pour compter le nombre de vistoire d'un joueur donné par contre 🤔

        // ctor EF Core
        private Game() { }


        // ctor front -> db
        public Game (DateTime date, Member joueurA, Member joueurB, Member? winner)
        {

            if (date > DateTime.Now)
                throw new ArgumentOutOfRangeException("la partie ne peut pas avoir été créée dans le future ...");

            CreationDate = date;
            InvokerA = joueurA;
            InvokerB = joueurB;
            Winner = winner;
        }

        // ctor db -> front
        public Game (Guid id, DateTime date, Member joueurA, Member joueurB, Member? winner) : this(date, joueurA, joueurB, winner)
        {
            Id = id;
        }



    }
}
