using System;
namespace Task
{
	public class Product
	{
		private double _price; 
		public double Price
		{
			get
			{
				return _price; 
			}
			set
			{
				if(value > 0)
				{
					_price = value; 
				}
			}
		}
        private int _count;
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                if (value > 0)
                {
                    _count = value;
                }
            }
        }

		public int No;
		public string name;

		public Product(string name, int no, double Price, int Count)
		{
			this.name = name;
			No = no;
			this.Price = Price;
			this.Count = Count;
		}

		public void ShowFullInfo()
		{
			Console.WriteLine($"Ad: {name} \nId: {No} \nPrice: {Price} \nCount:{Count}");
		}
    }
}

