using System;
using System.Linq;
using Soduko.GameHandlers;

namespace Soduko
{
    class Program
    {
        static void Main(string[] args)
        {


            var gameBoard2 = new GameBoards.GameBoard(9);
            
            var gameCreator = new GameCreator(gameBoard2, 3);

            gameCreator.GenerateGame();
            gameCreator.GenerateGame();

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

            Console.ReadKey();

        }
    }

   
}
