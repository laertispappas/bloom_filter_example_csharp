using System;

namespace lp_first
{
	/// <summary>
	/// This interface is used by implementations that provide 
	/// the functionality of a large bit array.
	/// </summary>
	public interface IBitStorage
	{
		void Set(int bitNumber);
		void Clear(int bitNumber);
		bool IsSet(int bitNumber);

		// If changing inerface is not applicable we need to keep track ourselves
		// in DictionaryChecker the size of bitArray elements.
		int Length();
	}

}


