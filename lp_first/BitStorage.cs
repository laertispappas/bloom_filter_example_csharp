using System;
using System.Collections;

namespace lp_first
{
	public class BitStorage : IBitStorage
	{
		private const int MaxBits = 256 * 64 * 1024;

		private readonly BitArray _ba = new BitArray(MaxBits);

		public void Clear(int bitNumber)
		{
			Value(bitNumber, false);
		}

		public bool IsSet(int bitNumber)
		{
			return _ba[bitNumber];
		}

		public void Set(int bitNumber)
		{
			Value(bitNumber, true);
		}

		// I need this in my calculations.
		// We need to know the bit array size
		// in order to mod the hash value to fit in
		// array. 
		public int Length()
		{
			return _ba.Length;
		}

		private void Value(int bitNumber, bool value)
		{
			_ba[bitNumber] = value;
		}
	}
}

