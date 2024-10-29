using System;
using Core.Model;

namespace Core.Data
{
    public static class AppDbContext
    {
        private static List<Hotel> hotels = new List<Hotel>();
        public static void AddHotel(Hotel hotel)
        {
            hotels.Add(hotel);
        }

        public static void ShowAllHotels()
        {
            foreach (var hotel in hotels)
            {
                hotel.ShowInfo();
                Console.WriteLine();
            }
        }
        public static Hotel findHotel(string name)
        {
            name.Trim().ToLower(); 
            foreach (var hotel in hotels)
            {
                if(hotel.Name == name)
                {
                    return hotel; 
                }
            }
            return null; 
        }
    }
}

