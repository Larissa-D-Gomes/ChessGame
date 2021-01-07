using System;
using GameBoard;
using Chess;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Board b = new Board(8, 8);
            b.InsertPiece(new Rook(Color.Black, b), new Position(0, 0));
            b.InsertPiece(new Rook(Color.Black, b), new Position(1, 3));
            b.InsertPiece(new King(Color.Black, b), new Position(2, 4));
            b.InsertPiece(new King(Color.White, b), new Position(3, 5));
            Screen.PrintBoard(b);
        }
    }
}
