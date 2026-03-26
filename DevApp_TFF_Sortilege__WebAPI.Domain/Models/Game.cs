namespace DevApp_TFF_Sortilege__WebAPI.Domain.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public Member PlayerA {  get; set; } = default!;
        public Member? PlayerB { get; set; } = default!;

        // ctor EF Core
        private Game() { }


        // ctor front -> db
        public Game (string name, DateTime date, Member joueurA, Member? joueurB)
        {
            Name = name;
            CreatedDate = date;
            PlayerA = joueurA;
            PlayerB = joueurB;
        }

        // ctor db -> front
        public Game (Guid id, string name, DateTime date, Member joueurA, Member? joueurB) : this()
        {
            Id = id;
        }



    }
}
