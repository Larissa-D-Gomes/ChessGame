using System;
using System.Collections.Generic;
using GameBoard;
using Chess;

namespace ChessConsole
{
    class View
    {

        /* Prints a chess match
         * @param ChessMatch cm
         */
        public static void PrintMatch(ChessMatch cm, bool[,] m)
        {
            if(m == null)
                PrintBoard(cm.Board);
            else
                PrintBoard(cm.Board, m);

            PrintCapturedPieces(cm);
            Console.WriteLine("\nTurn: " + cm.Turn);

            if (!cm.Finished)
            {
                Console.Write("Next Move: ");

                if(cm.CurrentPlayer == Color.Black)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(cm.CurrentPlayer);
                    Console.ForegroundColor = aux;
                }
                else
                {
                    Console.WriteLine(cm.CurrentPlayer);
                }


                if (cm.Check)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("CHECK!");
                    Console.ForegroundColor = aux;
                }
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("CHECKMATE!");
                Console.ForegroundColor = aux;

                Console.WriteLine("Winner: "+ cm.CurrentPlayer);
            }
        }

        /* Prints captured pieces 
         * @param ChessMatch cm
         */
        public static void PrintCapturedPieces(ChessMatch cm)
        {
            Console.WriteLine("\nCaptured Pieces:");

            Console.Write("White: ");
            PrintCollection(cm.CapturedPieces(Color.White));

            Console.Write("\nBlack: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            PrintCollection(cm.CapturedPieces(Color.Black));
            Console.WriteLine();
            Console.ForegroundColor = aux;
        }

        /* Prints a piece HashSet
         * @param HashSet<Piece> p
         */
        public static void PrintCollection(HashSet<Piece> p)
        {
            Console.Write("[ ");
            foreach (Piece piece in p)
                Console.Write(piece + " ");

            Console.Write("]");
        }

        /* Prints the chess game board 
         * @param Board board
         */
        public static void PrintBoard(Board board)
        {
            ConsoleColor backgroundOriginal = Console.BackgroundColor;
            ConsoleColor background = ConsoleColor.DarkGray;
            for ( int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                background = SwitchBackgroung(background);
                for (int j = 0; j < board.Columns; j++)
                {
                    Console.BackgroundColor = SwitchBackgroung(background);
                    background = Console.BackgroundColor;
                    PrintPiece(board.GetPiece(i, j));
                       
                }
                Console.BackgroundColor = backgroundOriginal;
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static ConsoleColor SwitchBackgroung(ConsoleColor c)
        {
            if (c == ConsoleColor.DarkRed)
                return ConsoleColor.DarkGray;
            else
                return ConsoleColor.DarkRed;
        } 

        /* Prints the chess game board 
         * @param Board board, bool[,] possiblePos
         */
        public static void PrintBoard(Board board, bool[,] possiblePos)
        {
            ConsoleColor backgroundOriginal = Console.BackgroundColor;
            ConsoleColor background = ConsoleColor.DarkGray;
            ConsoleColor newBackground = ConsoleColor.Green;

            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                background = SwitchBackgroung(background);
                for (int j = 0; j < board.Columns; j++)
                {
                    Console.BackgroundColor = SwitchBackgroung(background);
                    background = Console.BackgroundColor;
                    if (possiblePos[i, j])
                        Console.BackgroundColor = newBackground;
                    else
                        Console.BackgroundColor = background;

                    PrintPiece(board.GetPiece(i, j));

                }
                Console.BackgroundColor = backgroundOriginal;
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        /* Prints a chess game piece 
         * @param Piece piece
         */
        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("  ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece + " ");
                }
                else
                {
                    //print 
                    ConsoleColor c = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(piece + " ");
                    Console.ForegroundColor = c;
                }
            }
        }

        /* Reads a chess position from console
         * @return ChessPosition
         */
        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse("" + s[1]);
            return new ChessPosition(column, row);
        }
    }
}
