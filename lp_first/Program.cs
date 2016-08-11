using System;
using System.Collections;

namespace lp_first
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//int hashCount = 10;	 // k
			//for  = 100000 words;  // n
			// and error = 0.001		 // p


			var bitArray = new BitStorage ();
			var dictionary = new DictionaryChecker ();
			dictionary.Initialize ("pappas", bitArray);
			var res = dictionary.IsWordPresent ("pappas", bitArray);
			Console.Write (res);

			dictionary.Initialize ("pappas", bitArray);
			res = dictionary.IsWordPresent ("pappas", bitArray);
			Console.Write (res);

			dictionary.Initialize ("laertis1", bitArray);
			res = dictionary.IsWordPresent ("laertis2", bitArray);
			Console.Write (res);
		}
	}
}
