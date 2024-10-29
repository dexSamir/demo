using System;
using Core.Data;
using Core.Model;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isMainLoop = true;
            do
            {
                Console.WriteLine("1. Hotel yarat \n2. Hotelleri gor \n3. Hotel sec \n0. Exit\n");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Hotelin adini daxil edin: ");
                        string hotelName = Console.ReadLine();
                        Hotel newHotel = new Hotel(hotelName); 
                        AppDbContext.AddHotel(newHotel);
                        Console.WriteLine($"{hotelName} hotel yaradildi.\n");
                        break;

                    case "2":
                        AppDbContext.ShowAllHotels();
                        break;

                    case "3":
                        Console.WriteLine("Hotelin adini daxil edin: ");
                        hotelName = Console.ReadLine();
                        Hotel foundedHotel = AppDbContext.findHotel(hotelName);
                        bool isRoomLoop = false;
                        do
                        {

                            Console.WriteLine("1. Otag yarat \n2. Butun otaglari goster \n3. Evvelki Menuya qayit \n0. EXIT");
                            string choise2 = Console.ReadLine();
                            switch (choise2)
                            {
                                case "1":
                                    Console.WriteLine("Otagin Nomresini daxil edin: ");
                                    string RoomNo= Console.ReadLine();
                                    int personCapacity; 
                                    Console.WriteLine("Otagin insan tutumunu daxil edin: ");
                                    bool isFalse = int.TryParse(Console.ReadLine(), out personCapacity);
                                    while (!isFalse)
                                    {
                                        Console.WriteLine("Otagin insan tutumunu duzgun daxil edin!!");
                                        isFalse = int.TryParse(Console.ReadLine(),out  personCapacity); 
                                    }
                                    Room room = new Room(RoomNo, personCapacity);
                                    foundedHotel.AddRoom(room);

                                    break;

                                case "2":
                                    foundedHotel.ShowInfo();
                                    break;
                                case "3":
                                    Console.WriteLine("Esas menyuya qayidilir...");
                                    isRoomLoop = true;
                                    break;
                                case "4":
                                    Console.WriteLine("Proqramdan cixilir...");
                                    isRoomLoop = true;
                                    isMainLoop = true;
                                    break; 
                                default:
                                    Console.WriteLine("Bele bir emeliyyat yoxdur!");
                                    break;
                            }
                        } while (!isRoomLoop);
                        
                        break; 
                    case "0":
                        Console.WriteLine("Cixis edildi.");
                        isMainLoop = false;
                        break;

                    default:
                        Console.WriteLine("Bele emeliyyat yoxdur!!");
                        break;
                }
            } while (isMainLoop);
        }
    }
}
