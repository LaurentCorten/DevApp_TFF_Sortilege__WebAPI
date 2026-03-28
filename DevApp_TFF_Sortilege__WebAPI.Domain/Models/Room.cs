using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DevApp_TFF_Sortilege__WebAPI.Domain.Models
{
    public class Room
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;
        public Member CreatorMember { get; private set; } = default!;
        public Member? GuestMember { get; private set; } = default!;
        //public List<Member>? SpectatorMembers { get; private set; } = [];
        public DateTime InitTimeStamp { get; private set; }


        // ctor init
        public Room(string name, Member creatorMember, DateTime initTimeStamp)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Trim().Length < 3 || name.Trim().Length > 50)
                throw new ArgumentException("Le nom de la partie doit faire entre 3 et 50 caractères");
            if (initTimeStamp > DateTime.Now)
                throw new ArgumentOutOfRangeException("la partie ne peut pas avoir été créée dans le future ...");
            Name = name;
            CreatorMember = creatorMember;
            InitTimeStamp = initTimeStamp;            
        }

        // ctor store
        public Room(Guid id, string name, Member creatorMember, DateTime initTimeStamp, Member? guestMember = null) : this(name, creatorMember, initTimeStamp)
        {
            Id = id;
            GuestMember = guestMember;
        }
    }

    public class RoomManager
    {
        public List<Room> Rooms { get; } = []; //? Dictionary ou Enum mieux ? Est-ce que ça pourrait être dans le service RoomService directement plutôt que de faire un injectione n plus ?
      
    }
}
