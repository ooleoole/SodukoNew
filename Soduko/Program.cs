using System;
using System.Linq;
using System.Net.NetworkInformation;
using Soduko.GameHandlers;
using Soduko.Utilitys;

namespace Soduko
{
    class Program
    {
        static void Main(string[] args)
        {


            var gameBoard2 = new GameBoards.GameBoard(9);

            var gameCreator = new GameHolder(gameBoard2, 3);

            gameCreator.LoadGame();
            gameCreator.LoadGame();

            var testtag1 = new GameBoardTag(new Coordinate(1, 1), 1);
            var testtag2 = new GameBoardTag(new Coordinate(1, 1), 1);
            Console.WriteLine(testtag2==testtag1);
            Console.ReadKey();
            Console.WriteLine(testtag2.Coordinate==testtag1.Coordinate);
            Console.ReadKey();

            var gameKey = gameCreator.GameBoardGameKeysPair.Keys.ElementAt(0);
            var game = gameCreator.GameBoardGameKeysPair.Values.ElementAt(0);
            var gameKey2 = gameCreator.GameBoardGameKeysPair.Keys.ElementAt(1);
            var game2 = gameCreator.GameBoardGameKeysPair.Values.ElementAt(1);
            Console.Clear();
            Console.WriteLine(game);
            Console.WriteLine(gameKey);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(game2);
            Console.WriteLine(gameKey2);
            
            var gameSolver = new GameSolver(game2);
            gameSolver.SolveBoard();

            var target = gameSolver.TargetSolutionGameBoards.Keys.ElementAt(0);
            var solutuion = gameSolver.TargetSolutionGameBoards.Values.ElementAt(0).FirstOrDefault();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(target);
            Console.WriteLine(solutuion);

            Console.ReadKey();

        }
    }


}
