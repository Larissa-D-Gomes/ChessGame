using Chess;
using GameBoard;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessConsole.Helpers
{
    public class Controller
    {
        private static string[] MAIN_MENU = { "Traditional", "Chess 960"};
        private ChessMatch cm;


        // Starting point of the application
        public void Run()
        {
            var selection = 0;
            do
            {
                Console.Clear();
                selection = IO.GetMenuItem(MAIN_MENU, true);
                cm = new ChessMatch(selection);
                play();
            } while (selection != 0);
        }


        // Playing a match
        private void play()
        {
            try
            {

                while (!cm.Finished)
                {
                    try
                    {
                        bool[,] possiblePos = null;
                        View.PrintMatch(cm, possiblePos);

                        Position from = IO.GetMove().ToPosition();
                        cm.ValidateFromPosition(from);

                        possiblePos = cm.Board.GetPiece(from).GetPossibleMoves();

                        View.PrintMatch(cm, possiblePos);

                        Position to = IO.GetMove(false).ToPosition();
                        cm.ValidateToPosition(from, to);

                        cm.ExecuteMove(from, to);
                    }
                    catch (GameBoardException e)
                    {
                        IO.SetError(e.Message, "Press any key to continue...");
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        IO.SetError("Invalid Position!", "Press any key to continue...");
                    }
                }
                View.PrintMatch(cm, null);
            }
            catch (GameBoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
