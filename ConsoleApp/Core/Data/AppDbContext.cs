using System;
using Core.Helper;
using Core.Models;

namespace Core.Data
{
	public static class AppDbContext
	{
		static List<Room> rooms = new List<Room>();
		static List<Hotel> hotels = new List<Hotel>();


		public static void AddRoom (Room room)
		{
			rooms.Add(room); 
		}

		public static void AddHotel(Hotel hotel)
		{
			hotels.Add(hotel); 
		}

        public static Hotel FindAllHotel(string name)
        {
            foreach (var hotel in hotels)
            {
                if (hotel.Name == name)
                {
                    return hotel;
                }
            }
            return null;
        }

        public static Hotel FindAllHotel(int id)
        {
            foreach (var hotel in hotels)
            {
                if (hotel.Id == id)
                {
                    return hotel;
                }
            }
            return null;
        }

        public static void ShowAllHotels()
        {
            foreach (var hotel in hotels)
            {
                Console.WriteLine(hotel.ToString());
            }
        }

        public static void ShowAllRooms()
        {
            foreach (var room in rooms)
            {
                Console.WriteLine(room.ToString());
            }
        }

        #region burada Roomlar-i filter kimi bisey eledim 
        public static List<Room> FindAllRoom(double maxPrice, double minPrice)
		{
            List<Room> matchedRooms = new List<Room>();
            foreach (var room in rooms)
			{
				if (room.Price <= maxPrice && room.Price >= minPrice)
				{
					matchedRooms.Add(room); 
				}
			}
			return matchedRooms; 
		}
        public static List<Room> FindAllRoom(bool isAviable)
        {
            List<Room> matchedRooms = new List<Room>();
            foreach (var room in rooms)
            {
                if (room.IsAvialable == isAviable)
                {
                    matchedRooms.Add(room);
                }
            }
            return matchedRooms;
        }
        public static Room FindAllRoom(string name)
        {
            foreach (var room in rooms)
            {
                if (room.Name == name)
                {
                    return room; 
                }
            }
            return null;
        }
        public static Room FindAllRoom(int id)
        {
            foreach (var room in rooms)
            {
                if (room.Id == id)
                {
                    return room; 
                }
            }
            return null; 
        }
        #endregion

        public static void MakeReservation(int? roomId, int customerCOunt)
        {

            Room room = rooms.Find(room => room.Id == roomId);

            if (!room.IsAvialable)
            {
                throw new NotAvailableException("Otag bos deyil");
            }

            if(customerCOunt > room.PersonCapacity)
            {
                throw new NotAvailableException("Otag-in tutumu musteri sayina uygun deyil!");
            }

            room.IsAvialable = false;
            Console.WriteLine($"{room.Id} nomreli otag-a musteri yerlestirildi");
        }

        public static bool checkHotel(string name)
        {
            return hotels.Any(hotel => hotel.Name == name);
        }
    }
}

