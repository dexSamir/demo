using System;
using System.Text.RegularExpressions;

namespace StudentsGroups
{
    public class Group
    {
        public string GroupNo { get; }
        public int StudentLimit { get; }

        private Student[] students;

        public Group(string groupNo, int studentLimit)
        {
            if (!CheckGroupNo(groupNo))
            {
                Console.WriteLine("GroupNo formati yanlisdir! 2 boyuk herf və 3 reqemden ibarət olmalidir");
                return;
            }

            if (studentLimit < 5 || studentLimit > 18)
            {
                Console.WriteLine("Student limit 5 ile 18 arasinda olmalidir!");
                return;
            }

            GroupNo = groupNo;
            StudentLimit = studentLimit;
            students = new Student[0];
        }

        public bool CheckGroupNo(string groupNo)
        {
            Regex pattern = new Regex("^[A-Z]{2}[0-9]{3}$");
            return pattern.IsMatch(groupNo);
        }

        public void AddStudent(Student student)
        {
            if (students.Length >= StudentLimit)
            {
                Console.WriteLine("Qrup doludur, yeni telebe elave edile bilmez");
                return;
            }

            Array.Resize(ref students, students.Length + 1);
            students[students.Length - 1] = student;
            Console.WriteLine($"{student.Fullname} adli telebe qrupa elave olundu");
        }

        public Student GetStudent(int? id)
        {
            if (id == null)
            {
                Console.WriteLine("ID bos ola bilmez");
                return null;
            }

            foreach (var student in students)
            {
                if (student.Id == id)
                {
                    return student;
                }
            }

            Console.WriteLine($"ID {id} olan telebe tapilmadi");
            return null;
        }

        public Student[] GetAllStudents()
        {
            return students;
        }
    }

}

