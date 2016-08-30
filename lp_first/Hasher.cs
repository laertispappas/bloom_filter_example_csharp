using System;

namespace lp_first
{
	public class Hasher
	{
		public static ulong fnv1a(string bytes, ulong seed)
		{
			const ulong fnv64Offset = 14695981039346656037;
			const ulong fnv64Prime = 0x100000001b3;
			ulong hash = fnv64Offset ^ seed;

			for (var i = 0; i < bytes.Length; i++)
			{
				hash = hash ^ bytes[i];
				hash *= fnv64Prime;
				// hash += (hash << 10);

			}

			//hash += (hash << 3);
			// hash ^= (hash >> 11);
			//hash += (hash << 15);

			return hash;
		}

		public static int jenkins(string key, int len, int seed) {
			int hash, i;
			hash = seed ^ len;

			for (i = 0; i < len; ++i) {
				hash += key[i];
				hash += (hash << 10);
				hash ^= (hash >> 6);
			}
			hash += (hash << 3);
			hash ^= (hash >> 11);
			hash += (hash << 15);
			return hash;
		}

		public static int murmur(string data, int length, int seed){
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

			// avoid calculating modulo
			int len_m = len_4 << 2;
			int left = length - len_m;

			if (left != 0) {
				if (left >= 3) {
					h ^= (int) data[length - 3] << 16;
				}
				if (left >= 2) {
					h ^= (int) data[length - 2] << 8;
				}
				if (left >= 1) {
					h ^= (int) data[length - 1];
				}

				h *= m;
			}

			h ^= h >> 13; //h ^= h >>> 13;
			h *= m;
			h ^= h >> 15; //h ^= h >>> 15;

			return h;
		}

		public static ulong murmur64(string key, int offset, int len, ulong seed)
		{
			ulong length = (ulong) len;
			ulong m64 = 0xc6a4a7935bd1e995L;
			int r64 = 47;

			ulong h64 = (seed & 0xffffffffL) ^ (m64 * length);

			int lenLongs = len >> 3;

			for (int i = 0; i < lenLongs; ++i)
			{
				int i_8 = i << 3;

				ulong k64 =  ((ulong)  key[offset+i_8+0] & 0xff)      + (((ulong) key[offset+i_8+1] & 0xff)<<8)  +
					(((ulong) key[offset+i_8+2] & 0xff)<<16) + (((ulong) key[offset+i_8+3] & 0xff)<<24) +
						(((ulong) key[offset+i_8+4] & 0xff)<<32) + (((ulong) key[offset+i_8+5] & 0xff)<<40) +
						(((ulong) key[offset+i_8+6] & 0xff)<<48) + (((ulong) key[offset+i_8+7] & 0xff)<<56);

				k64 *= m64;
				k64 ^= k64 >> r64; // k64 ^= k64 >>> r64;
				k64 *= m64;

				h64 ^= k64;
				h64 *= m64;
			}

			int rem = len & 0x7;

			switch (rem)
			{
				case 0:
				break;
			case 7:
				h64 ^= (ulong)key [offset + len - rem + 6] << 48;
				break;
			case 6:
				h64 ^= (ulong)key [offset + len - rem + 5] << 40;
				break;
			case 5:
				h64 ^= (ulong)key [offset + len - rem + 4] << 32;
				break;
			case 4:
				h64 ^= (ulong)key [offset + len - rem + 3] << 24;
				break;
			case 3:
				h64 ^= (ulong)key [offset + len - rem + 2] << 16;
				break;
			case 2:
				h64 ^= (ulong)key [offset + len - rem + 1] << 8;
				break;
			case 1:
				h64 ^= (ulong)key [offset + len - rem];
				h64 *= m64;
				break;
			}

			h64 ^= h64 >> r64; // h64 >>> r64;
			h64 *= m64;
			h64 ^= h64 >> r64; // h64 >>> r64;

			return h64;
		}
	}
}

