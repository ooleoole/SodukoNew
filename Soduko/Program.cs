﻿using System;
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

            var gameBoard2 = new GameBoard.GameBoard(9);
            var test2 = gameBoard2.OrderByDescending(r => r.Coordinate.Y).ThenBy(c=> c.Coordinate.X);
            var game = new GameHandler(gameBoard2, 3);
            
            game.GenerateGame();
            int counter = 0;
            Console.Clear();
            Console.WriteLine(gameBoard2);
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
