namespace AccountUser;

class Program
{
    static void Main(string[] args)
    {
        bool isfalse = false;
        string fullname;
        string email;
        string password;

        do
        {
            Console.WriteLine("Adinizi ve Soyadinizi(optional) daxil edin:");
            fullname = Console.ReadLine().Trim();

            Console.WriteLine("Emailinizi daxil edin:");
            email = Console.ReadLine().Trim();

            Console.WriteLine("Parolunuzu daxil edin:");
            password = Console.ReadLine().Trim();
            User user = new User(fullname, email, password);

            if(!user.PasswordChecker(password))
            {
                do
                {
                    Console.WriteLine("Parol 1 boyuk herf, 1 kicik herf, 1 eded ve minimum 8 simvol uzunlugunda olmalidir! \nParolunuzu yeniden daxil edin:");
                    password = Console.ReadLine();
                } while (!user.PasswordChecker(password));
            }

            user.ShowInfo(); 
            isfalse = true; 

        } while (!isfalse);
    }
}

