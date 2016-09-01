using System;
using System.Text;

namespace lp_first
{
	public class DictionaryChecker : IDictionaryChecker
	{
		public void Initialize(string word, IBitStorage dictionary)
		{
			const int max = 256 * 64 * 1024;

			const ulong fnv64Offset = 14695981039346656037;
			const ulong fnv64Prime = 0x100000001b3;

			ulong hash11 = fnv64Offset ^ 0;

			for (var i = 0; i < word.Length; i++)
			{
				hash11 = hash11 ^ word[i];
				hash11 *= fnv64Prime;
			}

			ulong hash22 = fnv64Offset ^ hash11;
			for (var i = 0; i < word.Length; i++)
			{
				hash22 = hash22 ^ word[i];
				hash22 *= fnv64Prime;
			}

			dictionary.Set((int)((hash11 + (ulong)92569 * hash22) & (ulong)(max - 1)));
			dictionary.Set((int)((hash11 + (ulong)31 * hash22) & (ulong)(max - 1)));
			dictionary.Set((int)((hash11 + (ulong)9949 * hash22) & (ulong)(max - 1)));
		}

		public bool IsWordPresent(string word, IBitStorage dictionary)
		{
			const int max = 256 * 64 * 1024;

			const ulong fnv64Offset = 14695981039346656037;
			const ulong fnv64Prime = 0x100000001b3;

			ulong hash11 = fnv64Offset ^ 0;

			for (var i = 0; i < word.Length; i++)
			{
				hash11 = hash11 ^ word[i];
				hash11 *= fnv64Prime;
			}
			ulong hash22 = fnv64Offset ^ hash11;

			for (var i = 0; i < word.Length; i++)
			{
				hash22 = hash22 ^ word[i];
				hash22 *= fnv64Prime;
			}

			if (!dictionary.IsSet ((int)((hash11 + (ulong)92569 * hash22) & (ulong)(max - 1)))) {
				return false;
			}

			if (!dictionary.IsSet ((int)((hash11 + (ulong)31 * hash22) & (ulong)(max - 1)))) {
				return false;
			}

			if (!dictionary.IsSet ((int)((hash11 + (ulong)9949 * hash22) & (ulong)(max - 1)))) {
				return false;
			}

			// *Might* be in the dictionary
			return true;
		}
	}
}