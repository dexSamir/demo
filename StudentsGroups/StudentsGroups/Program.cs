using System;
using StudentsGroups;

namespace StudentsClass
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Qrup yaradin:");
            Console.Write("Qrup nomresini daxil edin (meselen, AB123): ");
            string groupNo = Console.ReadLine();

            Console.Write("Telebe limitini daxil edin (5-18 arasinda): ");
            int studentLimit;
            while (!int.TryParse(Console.ReadLine(), out studentLimit) || studentLimit < 5 || studentLimit > 18)
            {
                Console.WriteLine("Telebe limitini 5 ilə 18 arasinda daxil edin");
                Console.Write("Telebe limitini daxil edin (5-18 arasinda): ");
            }

            Group group = new Group(groupNo, studentLimit);

            while (true)
            {
                Console.WriteLine("\nSeçim edin:");
                Console.WriteLine("1. Telebe elave et");
                Console.WriteLine("2. Telebeleri goster");
                Console.WriteLine("3. Telebeni ID-ye gore tap");
                Console.WriteLine("4. Cix");
                Console.Write("Seciminizi daxil edin: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Telebe elave etme ucun melumat daxil edin:");
                        Console.Write("Tam adini daxil edin: ");
                        string fullname = Console.ReadLine();

                        Console.Write("Ballarini daxil edin (0-100 arasinda): ");
                        int point;
                        while (!int.TryParse(Console.ReadLine(), out point) || point < 0 || point > 100)
                        {
                            Console.WriteLine("Ballarini 0 ilə 100 arasinda daxil edin.");
                            Console.Write("Ballarini daxil edin (0-100 arasinda): ");
                        }

                        Student student = new Student(fullname, point);
                        group.AddStudent(student);
                        break;

                    case "2":
                        Console.WriteLine("\nQrupdaki butun telebeler:");
                        foreach (var stud in group.GetAllStudents())
                        {
                            Console.WriteLine(stud.StudentInfo());
                        }
                        break;

                    case "3":
                        Console.Write("Telebenin ID-sini daxil edin: ");
                        int id;
                        while (!int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("ID-nu daxil edin.");
                            Console.Write("Telebenin ID-sini daxil edin: ");
                        }

                        Student foundStudent = group.GetStudent(id);
                        if (foundStudent != null)
                        {
                            Console.WriteLine($"Tapilan Telebe: {foundStudent.StudentInfo()}");
                        }
                        else
                        {
                            Console.WriteLine($"ID {id} olan telebe tapilmadi.");
                        }
                        break;

                    case "4":
                        Console.WriteLine("Proqramdan cixilir...");
                        return;

                    default:
                        Console.WriteLine("Zehmet olmasa duzgun secim ele.");
                        break;
                }
            }
        }
    }
}
