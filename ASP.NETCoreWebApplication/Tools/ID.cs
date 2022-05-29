using System;

namespace ASP.NETCoreWebApplication.Tools
{
	public class ID
	{
		public static Int32 NewId()
		{
			Random rnd = new Random();

			return rnd.Next(100000, 100000000) - rnd.Next(2, 100) + rnd.Next(1, 1000) - rnd.Next(2, 5) +
			       rnd.Next(1, 13213);
		}
	}
}