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
        public static void PrintMatch(ChessMatch cm)
        {
            PrintBoard(cm.Board);
            PrintCapturedPieces(cm);
            Console.WriteLine("\nTurn: " + cm.Turn);
            Console.WriteLine("Next Move: " + cm.CurrentPlayer);

            if (cm.Check)
            {
                Console.WriteLine("CHECK");
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
            Console.ForegroundColor = ConsoleColor.Red;
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
            for( int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.GetPiece(i, j));
                       
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        /* Prints the chess game board 
         * @param Board board, bool[,] possiblePos
         */
        public static void PrintBoard(Board board, bool[,] possiblePos)
        {
            ConsoleColor background = Console.BackgroundColor;
            ConsoleColor newBackground = ConsoleColor.DarkRed;

            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possiblePos[i, j])
                        Console.BackgroundColor = newBackground;
                    else
                        Console.BackgroundColor = background;

                    PrintPiece(board.GetPiece(i, j));

                }
                Console.BackgroundColor = background;
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
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece + " ");
                }
                else
                {
                    // New print color
                    ConsoleColor c = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
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
