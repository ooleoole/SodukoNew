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
            var gameBordOpt = new GameBoards.GameBoard(9)
            {
                {new GameBoardTag(new Coordinate(9, 8), 6)},
                { new GameBoardTag(new Coordinate(9, 5), 3) },
                { new GameBoardTag(new Coordinate(8, 4), 2) },
                { new GameBoardTag(new Coordinate(8, 5), 7) },
               
                { new GameBoardTag(new Coordinate(8, 7), 3) },
                { new GameBoardTag(new Coordinate(7, 2), 7) },
                { new GameBoardTag(new Coordinate(7, 4), 9) },
                { new GameBoardTag(new Coordinate(3,5), 4) },
                { new GameBoardTag(new Coordinate(5,5), 1) },
                { new GameBoardTag(new Coordinate(5,7), 6) },
                { new GameBoardTag(new Coordinate(5,8), 7) },
                { new GameBoardTag(new Coordinate(5,9), 4) },
                { new GameBoardTag(new Coordinate(9,1), 4) }

            };
            var boardRules = new NormalSodukoRules();
            var gameCreator = new GameHolder(gameBoard2, 3, boardRules);

            gameCreator.LoadGame();
            gameCreator.LoadGame();




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

            var gameSolver = new GameSolver(gameBordOpt, boardRules);
            for (int i = 0; i < 1; i++)
            {
                gameSolver.SolveBoard();

                var target = gameSolver.TargetSolutionGameBoards.Keys.ElementAt(i);
                var solutuion = gameSolver.TargetSolutionGameBoards.Values.ElementAt(i).FirstOrDefault();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("---------------------------------------");
                Console.WriteLine(target);
                Console.WriteLine(solutuion);
            }

            Console.ReadKey();

        }
    }


}
