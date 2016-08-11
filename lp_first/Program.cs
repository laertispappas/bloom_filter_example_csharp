using System;
using System.Collections;

namespace lp_first
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var bitArray = new BitStorage ();
			var dictionary = new DictionaryChecker ();
			dictionary.Initialize ("pappas", bitArray);
			dictionary.IsWordPresent ("pappas", bitArray);

			dictionary.Initialize ("pappas", bitArray);
			dictionary.IsWordPresent ("pappas", bitArray);

			dictionary.Initialize ("laertis1", bitArray);
			dictionary.IsWordPresent ("laertis2", bitArray);
		}
	}
}
