using System;
namespace Core.Helper
{
	public class NotAvailableException : Exception
	{
		public NotAvailableException(string massage):base(massage)
		{
		}
	}
}

