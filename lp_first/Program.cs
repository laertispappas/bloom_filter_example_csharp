using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace lp_first
{
	class MainClass
	{
		private static int noItems = 250 * 1000;

		public static void Main (string[] args)
		{
			CalculateProbability ();
			CalculateExecutionTime ();
		}

		private static void CalculateExecutionTime(){
			var bitArray = new BitStorage ();
			var dictionary = new DictionaryChecker ();

			// Store some random words in an array
			string[] wordList = new string[noItems];
			for (int itemNo = 0; itemNo < noItems; itemNo++) {
				String s = Utils.GetRandomString(10);
				wordList[itemNo] = s;
			}

			// Calculate toral execution time for initialize and IsWordPresent
			var watch = System.Diagnostics.Stopwatch.StartNew();
			for (int itemNo = 0; itemNo < noItems; itemNo++) {
				dictionary.Initialize (wordList[itemNo], bitArray);
				dictionary.IsWordPresent (wordList[itemNo], bitArray);
			}
			watch.Stop();
			var elapsedMs = watch.ElapsedMilliseconds;
			Console.WriteLine ("**** #DictionaryChecker#Initialize Time Elapsed: {0} ********", elapsedMs);
		}

		private static void CalculateProbability(){
			Console.WriteLine ("****************************************");
			Console.WriteLine ("Calculating False Positive Probability");
			Console.WriteLine ("****************************************");

			int falsePositiveTest = 100 * 1000;
			double noFalsePositives = 0;
			int noNotIn = 0;

			var bitArray = new BitStorage ();
			var dictionary = new DictionaryChecker ();
			HashSet<string> already = new HashSet<string>();

			// Add items to dictionary
			for (int itemNo = 0; itemNo < noItems; itemNo++) {
				String s = Utils.GetRandomString(10);
				already.Add(s);
				dictionary.Initialize (s, bitArray);
			}
			// test for false positives
			for (int n = 0; n < falsePositiveTest; n++) {
				String s = Utils.GetRandomString(10);
				if (!already.Contains(s)) {
					noNotIn++;
					if (dictionary.IsWordPresent (s, bitArray)) 
						noFalsePositives++;
				}
			}
			double falsePositiveRate = noNotIn == 0 ? 0d : noFalsePositives / noNotIn;
			Console.WriteLine ("falsePositiveRate: #{0}", falsePositiveRate);
		}
	}
}
