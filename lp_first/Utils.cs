using System;
using System.Text;

namespace lp_first
{
	public class Utils
	{
		private static Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden
		public static string GetRandomString(int size)
		{
			StringBuilder builder = new StringBuilder();
			char ch;
			for (int i = 0; i < size; i++)
			{
				ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));                 
				builder.Append(ch);
			}

			return builder.ToString();
		}

		private static readonly Random getrandom = new Random();
		private static readonly object syncLock = new object();
		public static int GetRandomNumber(int min, int max)
		{
			lock(syncLock) { // synchronize
				return getrandom .Next(min, max);
			}
		}
	}
}

