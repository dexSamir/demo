using System;
namespace Core.Models
{
	public class Room
	{
		private static int _id = 0;
		public int Id{ get; set; }
		public string Name;
		public double Price;
		public int PersonCapacity;
		public bool IsAvialable { get; set; } = true;


		public Room(string name, double price, int personcapacity)
		{
			_id++;
			Id = _id;
			Name = name;
			Price = price;
			PersonCapacity = personcapacity;
			IsAvialable = true; 
		}

		public string ShowInfo()
		{
			return $"Id: {Id} \nName: {Name} \nPrice: {Price} \nPerson Capacity: {PersonCapacity}"; 
		}
        public override string ToString()
        {
            return ShowInfo();
        }
    }
}

