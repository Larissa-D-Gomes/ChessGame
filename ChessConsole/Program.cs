using System;
using GameBoard;
using Chess;

namespace ChessConsole
{
    class Program
    {
        // Test two
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
                        bool[,] possiblePos = null;
                        View.PrintMatch(cm, possiblePos);

                        Console.Write("\nFrom: ");
                        Position from = View.ReadChessPosition().ToPosition();
                        cm.ValidateFromPosition(from);

                        possiblePos = cm.Board.GetPiece(from).GetPossibleMoves();

                        Console.Clear();
                        View.PrintMatch(cm, possiblePos);

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
                View.PrintMatch(cm, null);
            }
            catch(GameBoardException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
