namespace lp_first
{
	/// <summary>
	/// Implementations of this interface provide the functionality
	/// of the dictionary.
	/// </summary>
	public interface IDictionaryChecker
	{
		/// <summary>
		/// Called once for each word that will be stored in the dictionary.
		/// </summary>
		/// <param name="word"></param>
		/// <param name="dictionary"></param>
		void Initialize(string word, IBitStorage dictionary);

		/// <summary>
		/// Called to check if the passed word is present in the dictionary
		/// or not.
		/// </summary>
		/// <param name="word"></param>
		/// <param name="dictionary"></param>
		/// <returns></returns>
		bool IsWordPresent(string word, IBitStorage dictionary);
	}
}