namespace Task;

class Program
{
    static void Main(string[] args)
    {
        Product product1 = new Product("Mac", 278947239, 2200.00, 1);
        //product1.ShowFullInfo();

        Book CrimeAndPunishment = new Book("Crime and Punishment", 8231834, 10.99, 1, "dram");
        Book It = new Book("It", 8231834, 15.99, 1, "horror");
        Book AnnaKarenina = new Book("Anna Karenina", 328374, 20.00, 1, "dram");
        Book Stand = new Book("Stand", 1238729, 13.99, 1, "Dark Fantasty");
        Book PetSametary = new Book("Pet Sametary", 74673846, 11.99, 1, "horror");
        Book DarkTower = new Book("Dark Tower", 28317723, 17.99, 1, "Dark Fantasty");

        //CrimeAndPunishment.ShowFullInfo(); 

        Library Axundov = new Library("Axundov");
        Axundov.AddBook(CrimeAndPunishment);
        Axundov.AddBook(It);
        Axundov.AddBook(AnnaKarenina);
        Axundov.AddBook(Stand);
        Axundov.AddBook(PetSametary);
        Axundov.AddBook(DarkTower);

        Axundov.GetFilteredBooks("horror");
        //Axundov.ShowAllBooks(); 

        Console.WriteLine(Axundov.GetFilteredBooks(10, 15));


    }
}

