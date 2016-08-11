using System;
using System.Text;

namespace lp_first
{
	// Bloom Filter:
	// Given k hash functions find the hash values for word by hashing it k times
	// modulus each hash value with the size of the bits in dictionary (dictionary.Length)
	// to get the array index
	// Set the bit to 1 to dictionary.Set[index]
	// 
	// Optimization: Use the same hash function k times as decribed in this paper
	// No need to implement k different hash functions
	// see http://www.eecs.harvard.edu/~kirsch/pubs/bbbf/esa06.pdf
	// TODO Increase time: ...


	public class DictionaryChecker : IDictionaryChecker
	{
		private const int HashCount = 10;
		private MurmurHash3_x86_32 mmh3 = new MurmurHash3_x86_32();


		public void Initialize(string word, IBitStorage dictionary)
		{
			//Console.Write ("Got hash value ##{0}## for word ##{1}##\n", hash, word);
			//Console.Write ("Modulus with bit array length #{0}\n", dictionary.Length ());
			//Console.Write ("Index (after mod) #{0}\n\n\n", index);
			for (int seed = 0; seed < HashCount; seed++) {
				int hash = (HashValue (word, seed));
				// TODO modulus with bits instead
				int index = mod (hash, dictionary.Length ());
				dictionary.Set(index);
			}
		}

		public bool IsWordPresent(string word, IBitStorage dictionary)
		{
			// Loop and hash the word HashCOunt times
			// If a zero is found we are definetly sure that
			// the world does not exist in our dictionary
			for (int seed = 0; seed < HashCount; seed++) {
				int hash = (HashValue (word, seed));
				// TODO modulus with bits instead
				int index = mod (hash, dictionary.Length ());
				if (dictionary.IsSet (index) == false) {
					return false;
				}
			}

			// Else World might exist in our dictionary
			return true;
		}

		private int HashValue(string word, int seed)
		{
			byte[] key = ToByteArray (word);
			mmh3.Seed = (uint)(0);

			var hashedKey = mmh3.ComputeHash(key, 0, key.Length);
			return BitConverter.ToInt32 (hashedKey, 0);
		}

		private byte[] ToByteArray(string word)
		{
			char[] letters = word.ToCharArray();
			byte[] wordToBytes = new byte[word.Length];

			for(int i = 0; i < word.Length; i++)
			{
				wordToBytes[i] = (byte)letters[i];
			}

			return wordToBytes;
		}

		// C# modulus % returns negative results
		// custom modulus function
		private int mod(int x, int m) {
			int r = x%m;
			return r<0 ? r+m : r;
		}
	}
}