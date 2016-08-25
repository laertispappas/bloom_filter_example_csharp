using System;
using System.Text;

namespace lp_first
{
	public class DictionaryChecker : IDictionaryChecker
	{
		public void Initialize(string word, IBitStorage dictionary)
		{
			foreach (int bucketIndex in BloomFilter.GetHashBuckets(word, 256 * 64 * 1024)) {
				dictionary.Set(bucketIndex);
			}
		}

		public bool IsWordPresent(string word, IBitStorage dictionary)
		{
			foreach (int bucketIndex in BloomFilter.GetHashBuckets(word, 256 * 64 * 1024)) {
				if (!dictionary.IsSet (bucketIndex)) {
					return false;
				}
			}

			// *Might* be in the dictionary
			return true;
		}
	}
}