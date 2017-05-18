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
	/// https://www.eecs.harvard.edu/~michaelm/postscripts/tr-02-05.pdf										*
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
		private const int HashCount = 3;

		// Gets the hash bucket indexes for bitArray
		//
		// @param [String] key the word we want to put in our dictionary
		// @param [int] max Length of bitArray storage
		// 
		// @return [Array] K (hashCount) bucjetIndexes for the given word
		public static int[] GetHashBuckets(string key, int max) {
			int[] result = new int[HashCount];

			//ulong hash11 = Hasher.murmur64 (key, 0, key.Length, 0);
			//ulong hash22 = Hasher.murmur64 (key, 0, key.Length, hash11);

			ulong hash11 = Hasher.fnv1a (key, 0);
			ulong hash22 = Hasher.fnv1a (key, hash11);

			for (int i = 0; i < HashCount; i++) {
				result [i] = (int)((hash11 + (ulong)i * hash22) & (ulong)(max - 1));
			}

			return result;
		}
	}
}

