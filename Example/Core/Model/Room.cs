using System;
namespace Core.Model
{
    public class Room
    {
        private static int _idCounter = 1;
        public int Id { get; private set; }
        public string No { get; private set; }
        public int PersonCapacity { get; private set; }

        public Room(string no, int personCapacity)
        {
            Id = _idCounter++;
            No = no;
            PersonCapacity = personCapacity;
        }

        public override string ToString()
        {
            return $"Room ID: {Id}, Room No: {No}, Person Capacity: {PersonCapacity}";
        }
    }
}

