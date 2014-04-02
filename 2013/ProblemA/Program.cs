using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2013.Qualification.ProblemA
{
	class Program
	{
		static void Main(string[] args)
		{
			string[] inputFileLines = File.ReadAllLines("input.txt");
			int testCases = int.Parse(inputFileLines[0]);

			List<GameBoard> gameBoards = new List<GameBoard>();
			int caseId = 1;

			for (int i = 0; i < testCases;  i++)
			{
				GameBoard board = new GameBoard();
				board.BoardId = i + 1;

				board.Board = new List<List<string>>();
				board.Board.Add(inputFileLines[(i * 5) + 1 + 0].ToCharArray().Select(x => x.ToString()).ToList());
				board.Board.Add(inputFileLines[(i * 5) + 1 + 1].ToCharArray().Select(x => x.ToString()).ToList());
				board.Board.Add(inputFileLines[(i * 5) + 1 + 2].ToCharArray().Select(x => x.ToString()).ToList());
				board.Board.Add(inputFileLines[(i * 5) + 1 + 3].ToCharArray().Select(x => x.ToString()).ToList());

				gameBoards.Add(board);
			}

			List<string> outputLines = new List<string>();
			foreach (GameBoard board in gameBoards)
			{
				string outcome = string.Empty;
				switch (board.GetWinner())
				{
					case "X": outcome = "X won"; break;
					case "O": outcome = "O won"; break;
					case "D": outcome = "Draw"; break;
					case "I": outcome = "Game has not completed"; break;
				}
				var outputLine = string.Format("Case #{0}: {1}", board.BoardId, outcome);
				Console.WriteLine(outputLine);
				outputLines.Add(outputLine);
			}
			File.WriteAllLines("output.txt", outputLines);
			Console.ReadLine();
		}
	}

	class GameBoard
	{
		public List<List<string>> Board { get; set; }
		public int BoardId { get; set; }

		//X = X Won
		//O = O Won
		//I = Incomplete
		//D = Draw
		public string GetWinner()
		{

			if (OutrightWinner("X", "T")) return "X";
			if (OutrightWinner("O", "T")) return "O";

			//No winner. Check if the entire board is covered. 
			var draw = true;
			for (int y = 0; y < 4; y++)
			{
				for (int x = 0; x < 4; x++)
				{
					if (Board[y][x] == ".")
					{
						draw = false;
					}
				}
			}
			if (draw)
				return "D";

			return "I";
		}

		public bool OutrightWinner(string key, string wildcard)
		{
			//Check for horizontal winners. 
			for (int y = 0; y < 4; y++)
			{
				var winner = true;

				for (int x = 0; x < 4; x++)
				{
					if (Board[y][x] == wildcard) continue;

					//If the position doesn't match it in the first row. Then we know we haven't got a winner. 
					//Same if there is an empty space. 
					if (Board[y][x] != key || Board[y][x] == ".")
					{
						winner = false;
					}
				}
				if (winner)
					return true;
			}

			//Check for reversal for vertical winners. 
			for (int x = 0; x < 4; x++)
			{
				var winner = true;

				for (int y = 0; y < 4; y++)
				{
					if (Board[y][x] == wildcard) continue;
					//If the position doesn't match it in the first row. Then we know we haven't got a winner. 
					//Same if there is an empty space. 
					if (Board[y][x] != key || Board[y][x] == ".")
					{
						winner = false;
					}
				}
				if (winner)
					return true;
			}

			var diagonalWinner = true;

			for (int i = 0; i < 4; i++)
			{
				if (Board[i][i] == wildcard) continue;

				if (Board[i][i] != key || Board[i][i] == ".")
				{
					diagonalWinner = false;
					break;
				}
			}
			if (diagonalWinner)
				return true;

			//Reverse diagonal. 
			diagonalWinner = true;

			for (int i = 0; i < 4; i++)
			{
				if (Board[i][3 - i] == wildcard) continue;

				if (Board[i][3 - i] != key || Board[i][3 - i] == ".")
				{
					diagonalWinner = false;
					break;
				}
			}
			if (diagonalWinner)
				return true;

			return false;
		}
	}
}
