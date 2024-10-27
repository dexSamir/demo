using System.Diagnostics;
using Core.Data;
using Core.Helper;
using Core.Models;

namespace ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        Hotel hotel = null;
        Room room = null; 
        string HotelName; 
        bool isFalse = false;
        bool isMainLoop = false;
        string RoomName;
        double RoomPrice;
        int RoomCapacity;
        int RoomId;
        int CustomerCount; 

        do
        {
            Console.WriteLine("1. Sisteme giris edin \n0. Cixis\n");
            string choise = Console.ReadLine();

            switch (choise)
            {
                case "0":
                    Console.WriteLine("Sistemden cixis edilir\n");
                    isMainLoop = true; 
                    break;
                case "1":
                    Console.WriteLine("Sisteme giris edilir\n");
                    bool isHotelLoop = false;

                    do
                    {

                        Console.WriteLine("1. Hotel elave et \n2. Butun hotelleri gor \n3. Hotel sec \n0. Exit\n");
                        string choise2 = Console.ReadLine();
                        switch (choise2)
                        {
                            case "1":
                                Console.WriteLine("Hotelin Adini daxil edin: ");
                                HotelName = Console.ReadLine();
                                hotel = new Hotel(HotelName);

                                if (!AppDbContext.checkHotel(HotelName))
                                {
                                    AppDbContext.AddHotel(hotel);
                                    Console.WriteLine($"{HotelName} otel-i yaradildi\n");
                                }
                                else
                                {
                                    bool condition1; 
                                    do
                                    {
                                        Console.WriteLine("Bele adda hotel movcuddur\n");
                                        Console.WriteLine("Hotelin Adini daxil edin: ");
                                        HotelName = Console.ReadLine();
                                        hotel = new Hotel(HotelName);
                                        condition1 = AppDbContext.checkHotel(HotelName);


                                    } while (condition1);

                                    AppDbContext.AddHotel(hotel);
                                    Console.WriteLine($"{HotelName} otel-i yaradildi\n");
                                }

                                break;

                            case "2":
                                AppDbContext.ShowAllHotels(); 
                                break;

                            case "3":
                                Console.WriteLine("Hotelin adini daxil edin: ");
                                string searchHotelName = Console.ReadLine();
                                isFalse = AppDbContext.checkHotel(searchHotelName);
                                while (!AppDbContext.checkHotel(searchHotelName))
                                {
                                    Console.WriteLine("Sistemde daxil etdiyiniz ada uygun hotel movcud deyil! \nXahis olnur duz-emelli ad daxil edesiniz: ");
                                    searchHotelName = Console.ReadLine();
                                    isFalse = AppDbContext.checkHotel(searchHotelName);

                                }

                                Hotel foundedHotelByName = AppDbContext.FindAllHotel(searchHotelName);
                                Console.WriteLine(foundedHotelByName.ToString());

                                bool isRoomLoop = false;

                                do
                                {

                                
                                    Console.WriteLine("\n1. Otag yarat \n2. Otaglari gor \n3. Rezervasiya Et \n4. Evvelki menu-ya qayit. \n0. EXIT\n");

                                    string choise3 = Console.ReadLine();

                                    switch (choise3)
                                    {
                                        case "1":
                                            Console.WriteLine("Otagin adini daxil edin: ");
                                            RoomName = Console.ReadLine();


                                            Console.WriteLine("Otagin bir gunluk qiymetini daxil edin: ");
                                            while (!double.TryParse(Console.ReadLine(), out RoomPrice))
                                            {
                                                Console.WriteLine("Otagin qiymetini duzgun daxil edin!");
                                            }


                                            Console.WriteLine("Otagin maximum insan tutumunu daxil edin: ");
                                            while (!int.TryParse(Console.ReadLine(), out RoomCapacity))
                                            {
                                                Console.WriteLine("Otagin tutumunu duzgun daxil edin!");
                                            }

                                            room = new Room(RoomName, RoomPrice, RoomCapacity);
                                            AppDbContext.AddRoom(room); 
                                            isFalse = false; 
                                            break;

                                        case "2":
                                            AppDbContext.ShowAllRooms(); 
                                            break;

                                        case "3":
                                            Console.WriteLine("Rezervasiya etmek ucun Otagin id-ni ve Musteri sayini daxil etmek lazimdin\n");
                                            Console.WriteLine("Otagin id-ni daxil edin: ");
                                            while (!int.TryParse(Console.ReadLine(), out RoomId))
                                            {
                                                Console.WriteLine("Otagin id-ni duzgun daxil edin: ");
                                            }


                                            try
                                            {
                                                Console.WriteLine("Musteri sayini daxil edin: ");
                                                while (!int.TryParse(Console.ReadLine(), out CustomerCount))
                                                {
                                                    Console.WriteLine("Musteri sayini duzgun daxil edin: ");
                                                }
                                                AppDbContext.MakeReservation(RoomId, CustomerCount);
                                            }
                                            catch (NotAvailableException ex)
                                            {
                                                Console.WriteLine(ex.Message) ; 
                                            }
                                            break;

                                        case "4":
                                            Console.WriteLine("Evvelki menuya qayidilir");
                                            isRoomLoop = true;
                                            break;
                                        case "0":
                                            isRoomLoop = true;
                                            isHotelLoop = true; 
                                            isMainLoop = true;
                                            break;
                                        default:
                                            Console.WriteLine("Bele emeliyyat movcud deyil!\n");
                                            break;
                                    }
                                } while (!isRoomLoop);

                                break;
                            case "0":
                                isHotelLoop = true;
                                break;
                            default:
                                Console.WriteLine("Bele emeliyyat movcud deyil!\n");
                                break;
                        }

                    } while (!isHotelLoop);

                    break;
                default:
                    Console.WriteLine("Bele emeliyyat movcud deyil!\n");
                    break;
            }
        } while (!isMainLoop);
    }
}

