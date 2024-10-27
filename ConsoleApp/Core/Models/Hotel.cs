
namespace Core.Models
{
	public class Hotel
	{
		private static int _id = 0;
		public int Id { get; set; }
		public string Name;

		public Hotel(string name)
		{
			_id++;
			Id = _id;
			Name = name; 
		}

		public string showInfo()
		{
			return $"Id: {Id}   Name: {Name}"; 
		}
        public override string ToString()
        {
            return showInfo();
        }

    }
}

