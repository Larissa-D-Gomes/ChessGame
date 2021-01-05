using System;
using GameBoard;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Board b = new Board(4, 5);
            Screen.PrintBoard(b);
        }
    }
}
