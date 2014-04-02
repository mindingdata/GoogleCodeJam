using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2009.Qualification.ProblemA
{
	class Program
	{
		static void Main(string[] args)
		{
			string[] inputFileLines = File.ReadAllLines("input.txt");
			int knownWordCount = int.Parse(inputFileLines[0].Split(' ')[1]);
			//int testCaseCount = int.Parse(inputFileLines[0].Split(' ')[2]);

			List<string> knownWords = new List<string>();
			for (int i = 1; i < knownWordCount + 1; i++)
			{
				knownWords.Add(inputFileLines[i]);
			}

			List<string> testCases = new List<string>();
			for (int i = knownWordCount + 1; i < inputFileLines.Count(); i++)
			{
				testCases.Add(inputFileLines[i]);
			}

			List<string> outputList = new List<string>();
			int caseNumber = 1;
			foreach (string testCase in testCases)
			{
				List<string> possibleAnswers = knownWords;
				List<string> explodedStrings = explodeString(testCase);

				int position = 0;
				foreach (string explodedString in explodedStrings)
				{
					
					List<string> matchingAnswers = new List<string>();

					char[] possibleLetters = explodedString.ToCharArray();
					foreach (var possibleLetter in possibleLetters)
					{
						foreach (var answer in possibleAnswers)
						{
							if (answer[position] == possibleLetter)
								matchingAnswers.Add(answer);
						}
					}

					possibleAnswers = matchingAnswers;
					position++;
				}

				var output = string.Format("Case #{0}: {1}", caseNumber, possibleAnswers.Count);
				outputList.Add(output);
				Console.WriteLine(output);
				caseNumber++;
			}
			File.WriteAllLines("output.txt", outputList);
			Console.ReadLine();
		}

		static List<string> explodeString(string testCase)
		{
			bool inBrackets = false;
			string buffer = string.Empty;

			List<string> resultSet = new List<string>();

			for (int i = 0; i < testCase.Length; i++)
			{
				if (testCase[i] == '(')
				{
					inBrackets = true;
					continue;
				}

				if (testCase[i] == ')')
				{
					resultSet.Add(buffer);
					buffer = string.Empty;
					inBrackets = false;
					continue;
				}

				if (inBrackets)
				{
					buffer += testCase[i];
				}
				else
				{
					resultSet.Add(testCase[i].ToString());
				}
			}

			return resultSet;
		}
	}
}
