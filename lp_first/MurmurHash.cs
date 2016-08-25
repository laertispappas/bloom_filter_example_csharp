using System;

namespace lp_first
{
	public class MurmurHash
	{
		public MurmurHash ()
		{
		}
		// MurmurHash 2.0 Implementation
		// See http://murmurhash.googlepages.com/
		public int hash(string data, int length, int seed){
			int m = 0x5bd1e995;
			int r = 24;

			int h = seed ^ length;

			int len_4 = length >> 2;

			for (int i = 0; i < len_4; i++) {
				int i_4 = i << 2;
				int k = data[i_4 + 3];
				k = k << 8;
				k = k | (data[i_4 + 2] & 0xff);
				k = k << 8;
				k = k | (data[i_4 + 1] & 0xff);
				k = k << 8;
				k = k | (data[i_4 + 0] & 0xff);
				k *= m;
				k ^= k >> r; // (int)((uint)x >> r);  ----> //k ^= k >>> r;
				k *= m;
				h *= m;
				h ^= k;
			}

			return h;
		}
	}
}

