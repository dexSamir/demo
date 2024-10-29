using System;
namespace Core.Model
{
    public class Hotel
    {
        private static int _idCounter = 1;
        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<Room> Rooms { get; private set; }

        public Hotel(string name)
        {
            Id = _idCounter++;
            Name = name;
            Rooms = new List<Room>();
        }

        public void AddRoom(Room room)
        {
            Rooms.Add(room);
            Console.WriteLine($"{room.No} otagi elave olundu");
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Hotel ID: {Id}, Name: {Name}");
            foreach (var room in Rooms)
            {
                Console.WriteLine(room.ToString());
            }
        }
    }
}

