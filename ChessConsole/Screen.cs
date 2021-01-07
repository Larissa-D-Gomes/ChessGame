using System;
using GameBoard;
using Chess;

namespace ChessConsole
{
    class Screen
    {
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
                    if (board.GetPiece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        PrintPiece(board.GetPiece(i, j));
                    }   
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        /* Prints a chess game piece 
         * @param Piece piece
         */
        public static void PrintPiece(Piece piece)
        {
            if(piece.Color == Color.White)
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
