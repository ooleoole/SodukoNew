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
            
            var gameHandler = new GameCreator(gameBoard2, 3);
            
            gameHandler.GenerateGame();
            var gameKey = gameHandler.GameBoardGameKeysPair.Keys.ElementAt(0);
            var game = gameHandler.GameBoardGameKeysPair.Values.ElementAt(0);
            Console.Clear();
            Console.WriteLine(game);
            Console.WriteLine(gameKey);
            Console.ReadKey();
        }
    }

   
}
