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
        public Guid CreatorId { get; private set; } = default!;
        public Guid? GuestId { get; private set; } = default!;
        //public List<Member>? SpectatorMembers { get; private set; } = [];
        public DateTime TimeStamp { get; private set; }


        // ctor init
        public Room(string roomName, Guid creatorId, DateTime timeStamp)
        {
            if (string.IsNullOrWhiteSpace(roomName) || roomName.Trim().Length < 3 || roomName.Trim().Length > 50)
                throw new ArgumentException("Le nom de la partie doit faire entre 3 et 50 caractères");
            if (TimeStamp > DateTime.Now)
                throw new ArgumentOutOfRangeException("la partie ne peut pas avoir été créée dans le future ...");
            Name = roomName;
            CreatorId = creatorId;
            TimeStamp = timeStamp;


        }

        // ctor store
        public Room(Guid id, string roomName, Guid creatorId, DateTime timeStamp, Guid? guestId = null) : this(roomName, creatorId, timeStamp)
        {
            Id = id;
            GuestId = guestId;
        }
    }
}
