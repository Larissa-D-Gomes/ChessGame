using GameBoard;
using System;

namespace Chess
{
    class ChessPosition
    {
        public char Column { get; set; }
        public int Row { get; set; }

        // Constructor

        /* @param char column, int linha
         */
        public ChessPosition(char column, int row)
        {
            this.Column = column;
            this.Row = row;
        }

        // Methods

        /* Return the ChessPosition as Position
         */
        public Position ToPosition()
        {
            return new Position(8 - this.Row, Char.ToLower(this.Column) - 'a');
        }

        // ToString
        public override string ToString()
        {
            return "" + this.Column + this.Row;
        }



    }
}
