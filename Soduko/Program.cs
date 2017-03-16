using System;
using System.Linq;
using Soduko.GameHandlers;
using Soduko.Utilitys;

namespace Soduko
{
    public class Program
    {
        static void Main(string[] args)
        {


            
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
            
            for (int j = 0; j < 80; j++)
            {
                var gameBoard2 = new GameBoards.GameBoard(9);
                var boardRules = new NormalSodukoRules();
                var gameCreator = new GameHolder(gameBoard2, 3, boardRules);
                Console.WriteLine("///////////////////////////////");
                gameCreator.LoadGame();
               



                var gameKey2 = gameCreator.GameBoardGameKeysPair.Keys.ElementAt(0);
                var game2 = gameCreator.GameBoardGameKeysPair.Values.ElementAt(0);
         
                //Console.Clear();
               
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(game2);
                Console.WriteLine(gameKey2);
                Console.ResetColor();
                var solve = game2.Clone();
                var gameSolver = new GameSolver(game2, boardRules);
                for (int i = 0; i < 1; i++)
                {

                    gameSolver.SolveBoard();

                    var target = gameSolver.TargetSolutionGameBoards.Keys.ElementAt(i);
                    var solutuion = gameSolver.TargetSolutionGameBoards.Values.ElementAt(i).FirstOrDefault();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("---------------------------------------");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(target);
                    Console.WriteLine(solutuion);
                    Console.ResetColor();

                }

            }
            Console.WriteLine("Done!!");
            Console.ReadKey();

        }
    }


}
