using System;
namespace StudentsGroups
{
    public class Student
    {
        private static int _id = 0;
        public int Id { get; }
        public string Fullname { get; }
        public int Point { get; }

        public Student(string fullname, int point)
        {
            if (string.IsNullOrWhiteSpace(fullname))
            {
                Console.WriteLine("Fullname bos ola bilmez!");
                return;
            }

            if (point < 0 || point > 100)
            {
                Console.WriteLine("Point 0 ile 100 arasinda olmalidir!!!");
                return;
            }

            _id++;
            Id = _id;
            Fullname = fullname;
            Point = point;
        }

        public string StudentInfo()
        {
            return $"ID: {Id}, Fullname: {Fullname}, Point: {Point}";
        }
    }
}

