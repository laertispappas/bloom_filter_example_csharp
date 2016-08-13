using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace lp_first
{
	class MainClass
	{

		// corpus: https://github.com/laertispappas/mapreduce_python/tree/master/corpus/
		// TODO add 100k unique words
		private const string WordlistCirpusFileName = "../../l33t_words.txt";

		public static void Main (string[] args)
		{
			var bitArray = new BitStorage ();
			var dictionary = new DictionaryChecker ();

			var corpusWordList = GetWordsFromCorpus (WordlistCirpusFileName);
			// Add all words to dictionary (200k+)
			Console.WriteLine ("Going to puts 200k words in dictionary");

			var watch = System.Diagnostics.Stopwatch.StartNew();
			AddToDictionary (corpusWordList, bitArray, dictionary);
			// Find all words. This raises an exception if no word is found
			// Our probability is 0.001 
			Console.WriteLine ("Going to iterate over all words and check if they exist in dictionary");
			Console.WriteLine ("If a word is not found an exception will be thrown");
			Console.WriteLine ("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			CheckWords (corpusWordList, bitArray, dictionary);

			watch.Stop();
			var elapsedMs = watch.ElapsedMilliseconds;
			Console.WriteLine ("**** Time Elapsed: {0} ********", elapsedMs);

			Console.WriteLine ("All words were found!!!");

			var nonExistingWord = "l33tn0n3)(1St1nGWord";
			Console.WriteLine ("Going to check if this word is in dictionary: #{0}#", nonExistingWord);
			var isPresent = dictionary.IsWordPresent (nonExistingWord, bitArray);
			Console.WriteLine ("Is word #l33tn0n3)(1St1nGWord# present?: ");
			Console.WriteLine (isPresent);


			Console.WriteLine ("Exiting. ");
		}

		private static List<string> GetWordsFromCorpus(string filename){
			var list = new List<String>();

			char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
			foreach (string line in File.ReadAllLines(filename))
			{
				string[] parts = line.Split(delimiterChars);
				foreach (string part in parts)
				{
					list.Add(part);

					//Console.WriteLine("{0}", part);
				}
			}

			return list;
		}

		private static void AddToDictionary(List<String> corpusWordList, BitStorage bitArray,DictionaryChecker dictionary){
			foreach (var word in corpusWordList) {
				//Console.Write ("Adding: #{0} to BitStorage\n", word);
				dictionary.Initialize (word, bitArray);
			}
		}

		private static void CheckWords(List<String> wordlist, BitStorage bitArray, DictionaryChecker dictionary) {
			foreach (var word in wordlist) {
				var isPresent = dictionary.IsWordPresent (word, bitArray);
				//Console.Write ("Is: #{0} present? {1}\n", word, isPresent);
				if (!isPresent) {
					throw new Exception ("Word not found in dictionary");
				}
			}
		}
	}
}
