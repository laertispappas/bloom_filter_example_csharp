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

		public static void Main (string[] args)
		{
			var bitArray = new BitStorage ();
			var dictionary = new DictionaryChecker ();

			var corpusWordList = GetWordsFromCorpus (CorpusFileName);
			AddToDictionary (corpusWordList, bitArray, dictionary);
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
				var isPresent = dictionary.IsWordPresent (word, bitArray);
				Console.Write ("Is: #pappas present? {0}\n", isPresent);
			}
		}
	}
}
