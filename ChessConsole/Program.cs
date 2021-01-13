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
                    try
                    {
                        Console.Clear();

                        View.PrintMatch(cm);

                        Console.Write("\nFrom: ");
                        Position from = View.ReadChessPosition().ToPosition();
                        cm.ValidateFromPosition(from);

                        bool[,] possiblePos = cm.Board.GetPiece(from).GetPossibleMoves();
                        Console.Clear();
                        View.PrintBoard(cm.Board, possiblePos);
                        View.PrintCapturedPieces(cm);
                        Console.WriteLine("\nTurn: " + cm.Turn);
                        Console.WriteLine("Next Move: " + cm.CurrentPlayer);

                        Console.Write("\nTo: ");
                        Position to = View.ReadChessPosition().ToPosition();
                        cm.ValidateToPosition(from, to);
                        cm.ExecuteMove(from, to);
                    }
                    catch(GameBoardException e)
                    {
                        Console.WriteLine("\n" + e.Message + "\nPress enter to continue...");
                        Console.ReadLine();
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Console.WriteLine("\n" + "Invalid Position!" + "\nPress enter to continue...");
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                View.PrintMatch(cm);
            }
            catch(GameBoardException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
