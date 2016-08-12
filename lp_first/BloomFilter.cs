using System;

namespace lp_first
{
	public class BloomFilter
	{
		private static MurmurHash hasher = new MurmurHash();
		// we know our data 100k words and 2mb limit in space
		// so this is statically typed here (pre calculated)
		private const int HashCount = 10;

		public BloomFilter ()
		{
		}

		// Use the same hash function k times as decribed in this paper
		// No need to have k hash functions and hash the word k times.
		// http://www.eecs.harvard.edu/~kirsch/pubs/bbbf/esa06.pdf
		public static int[] GetHashBuckets(string key, int max) {
			byte[] byteKey = ToByteArray (key);
			int[] result = new int[HashCount];

			int hash1 = hasher.hash (byteKey, byteKey.Length, 0);
			int hash2 = hasher.hash (byteKey, byteKey.Length, hash1);

			for (int i = 0; i < HashCount; i++) {
				result [i] = mod ((hash1 + i * hash2), max);
			}

			return result;
		}

		
		// C# modulus % returns negative results
		// custom mod function to return positive result
		private static int mod(int x, int m) {
			int r = x%m;
			return r<0 ? r+m : r;
		}

		// Convert a string to bytes
		// TODO I think this can be done more efficient
		private static byte[] ToByteArray(string word)
		{
			char[] letters = word.ToCharArray();
			byte[] wordToBytes = new byte[word.Length];

			for(int i = 0; i < word.Length; i++)
			{
				wordToBytes[i] = (byte)letters[i];
			}

			return wordToBytes;
		}
	}
}

