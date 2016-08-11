using System;

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
	// TODO Increase time: 

	public class DictionaryChecker : IDictionaryChecker
	{
		public int hashCount;
		private const int HashCount = 10;
		private MurmurHash3_x64_128 mmh3 = new MurmurHash3_x64_128();


		public void Initialize(string word, IBitStorage dictionary)
		{
			int hash = HashValue (word) % dictionary.Length();
			Console.Write ("Initializing word: {0} with hash: {1}", word, hash);
			dictionary.Set(hash);
		}

		public bool IsWordPresent(string word, IBitStorage dictionary)
		{
			int hash = HashValue (word) % dictionary.Length();
			//Console.Write ("Lookup word: {0} with hash: {1}", word, hash);
			return dictionary.IsSet(hash);
		}

		private int HashValue(string word)
		{
			Console.Write ("Inside hashedvalue. Word: {0}", word);

			int hashbits = 128;
			int hashbytes = hashbits / 8;

			byte[] key = new byte[256];
			byte[] hashes = new byte[hashbytes * 256];

			// Hash keys of the form {0}, {0,1}, {0,1,2}... up to N=255,using 256-N as
			// the seed
			for (int i = 0; i < 256; i++)
			{
				key[i] = (byte)i;
				mmh3.Seed = (uint)(256 - i);
				var ret = mmh3.ComputeHash(key, 0, i);
				Array.Copy(ret, 0, hashes, i * hashbytes, hashbytes);
			}

			// Then hash the result array
			mmh3.Seed = 0;
			var final = mmh3.ComputeHash(hashes, 0, hashbytes * 256);

			// The first four bytes of that hash, interpreted as a little-endian integer, is our
			// verification value
			UInt32 hashedWord = (UInt32)((final[0] << 0) | (final[1] << 8) | (final[2] << 16) | (final[3] << 24));
			Console.Write("hashWord: {0} --- final: {1}", hashedWord, final);
			Console.Write ("\n\n");
			//if (BitConverter.IsLittleEndian)
			//	Array.Reverse(final);
			// ToUInt64 // 
			//int intFinal = BitConverter.ToInt32(final, 0);

			return 10;
		}
	}
}

