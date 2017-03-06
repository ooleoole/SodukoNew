using System;
using System.Linq;
using Soduko.GameBoard;
using Soduko.GameHandlers;

namespace Soduko
{
    class Program
    {
        static void Main(string[] args)
        {

            int?[,] array2D = { { 1, 2, 3 }, { 3, 4, null }, { null, 9, null } };
            int x = 0;
            int y = 0;

            GameBoard2 gameBoard2 = new GameBoard2(9);
            var test2 = gameBoard2.OrderByDescending(r => r.Coordinates.Y).ThenBy(c=> c.Coordinates.X);
            var game = new GameHandler(gameBoard2, 3);
            
            game.GenerateGame();
            int counter = 0;
            Console.Clear();
            foreach (var item in test2)
            {
                counter++;
                Console.Write(item+" ");
                if (counter == 9)
                {
                    Console.WriteLine();
                    counter = 0;
                }
            }
            Console.ReadKey();
        }
    }

    public class SudokoBoard
    {
        int[,] array2D = { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };

        public void Print()
        {
            for (int i = 0; i < array2D.Length; i++)
            {

            }
        }
    }
}
