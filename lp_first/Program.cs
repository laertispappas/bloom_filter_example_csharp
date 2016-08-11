using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace lp_first
{
	// corpus: https://github.com/laertispappas/mapreduce_python/tree/master/corpus/
	class MainClass
	{
		private const string CorpusFileName = "../../KING_HENRY_THE_EIGHTH";
		private const string TestCorpusFileName = "../../THE_TRAGEDY_OF_ROMEO_AND_JULIET";

		public static void Main (string[] args)
		{
			var bitArray = new BitStorage ();
			var dictionary = new DictionaryChecker ();

			var corpusWordList = GetWordsFromCorpus (CorpusFileName);
			AddToDictionary (corpusWordList, bitArray, dictionary);
			CheckWords (corpusWordList, bitArray, dictionary);

			// Test
			var testCorpusWordList = GetWordsFromCorpus (CorpusFileName);
			CheckWords (testCorpusWordList, bitArray, dictionary);

			var isPresent = dictionary.IsWordPresent ("laertis2", bitArray);
			Console.Write ("Is word ###{0}### present? {1}\n", "laertis2", isPresent);	
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
				Console.Write ("Adding: #{0} to BitStorage\n", word);
				dictionary.Initialize (word, bitArray);
			}
		}

		private static void CheckWords(List<String> wordlist, BitStorage bitArray, DictionaryChecker dictionary) {
			foreach (var word in wordlist) {
				var isPresent = dictionary.IsWordPresent (word, bitArray);
				Console.Write ("Is: #{0} present? {1}\n", word, isPresent);				
			}
		}
	}
}
