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

		public void Initialize(string word, IBitStorage dictionary)
		{
			int hash = HashValue (word) % dictionary.Length();
			dictionary.Set(hash);
		}

		public bool IsWordPresent(string word, IBitStorage dictionary)
		{
			int hash = HashValue (word) % dictionary.Length();
			return dictionary.IsSet(hash);
		}

		// TODO Dummy hash function. Add a real one here
		private int HashValue(string word) 
		{
			return 123;
		}
	}
}

