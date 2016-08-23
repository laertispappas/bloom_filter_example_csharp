using System;

namespace lp_first
{	/// *********************************************************************************************
	/// <summary>																					*
	/// Implementation of Bloom Filter algorithm. Bloom filter requires K hashes					*
	/// to hash a word k times. For example given word 'foo' and k = 3 we hash foo					*
	/// 3 times with 3 hashing function: 															*
	/// h1 = hasher1.hash('foo')																	*
	/// h2 = hasher2.hash('foo')																	*
	/// h3 = hasher3.hash('foo')																	*
	/// for each hash result (h1, h2, h3) we set 1 to bit array meaning this word is present.		*
	/// In order to see if a word is in out dictionary wee need to do the same procedure again: 	*
	/// h1 = hasher1.hash('foo')																	*
	/// h2 = hasher2.hash('foo')																	*
	/// h3 = hasher3.hash('foo')																	*
	/// And check our bitArray if all the values on corresponding hash indexes are 1				*
	/// If true we return true (May be in dictionaey). If at least 1 hash index returns				*
	/// 0 (false) from our bit array we are sure 100% that the word does not exist in ou dictionary	*
	/// *********************************************************************************************
	/// *********************************************************************************************
	/// *** For this implementation we take a more efficient approach as discribed in this paper:   *
	/// http://www.eecs.harvard.edu/~kirsch/pubs/bbbf/esa06.pdf										*
	// No need to have k hash functions and hash the word k times.									*
	/// We only hash it 2 times and then we take each hash index by 								*
	/// the following fucntion:																		*
	/// 																							*
	/// hash[i] = (hash1_result + i * hash2_result) % bitArraySize									*
	/// 																							*
	/// </summary>																					*
	/// *********************************************************************************************
	public class BloomFilter
	{
		// We use MurmurHash v2 here ported ported from a JAVA implementation 
		// (Same code with 2 changes for unsigned right shift in JAVA (>>> operator))
		// TODO: Think of changing this to MurmurHashv3
		private static MurmurHash hasher = new MurmurHash();
		// we know our data 100k words and 2mb limit in space
		// so this is statically typed here (pre calculated)
		// for 0.001 error If we eant to decrease error rate we need to increament hashCount as well. 
		private const int HashCount = 10;

		// Gets the hash bucket indexes for bitArray
		//
		// @param [String] key the word we want to put in our dictionary
		// @param [int] max Length of out botArray storage
		// 
		// @return [Array] K (hashCount) bucjetIndexes for the given word
		public static int[] GetHashBuckets(string key, int max) {
			byte[] byteKey = ToByteArray (key);
			int[] result = new int[HashCount];

			int hash1 = hasher.hash (byteKey, byteKey.Length, 0);
			int hash2 = hasher.hash (byteKey, byteKey.Length, hash1);

			for (int i = 0; i < HashCount; i++) {
				result [i] = (int)((uint)(hash1 + i * hash2) % (uint)max);
			}

			return result;
		}

		// Convert a string to bytes
		// Rethink this
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

