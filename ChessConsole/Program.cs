using System;
using GameBoard;
using Chess;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch cm = new ChessMatch();

                while (!cm.Finished)
                {
                    Console.Clear();
                    Screen.PrintBoard(cm.Board);

                    Console.Write("\nFrom: ");
                    Position from = Screen.ReadChessPosition().ToPosition();

                    bool[,] possiblePos = cm.Board.GetPiece(from).GetPossibleMoves();
                    Console.Clear();
                    Screen.PrintBoard(cm.Board, possiblePos);

                    Console.Write("\nTo: ");
                    Position to = Screen.ReadChessPosition().ToPosition();
                    cm.Move(from, to);
                }
            }
            catch(GameBoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
