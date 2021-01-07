using GameBoard;

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
            this.Row = linha;
        }

        // Methods

        /* Return the ChessPosition as Position
         */
        public Position ToPosition()
        {
            return new Position(8 - this.Row, this.Column - 'a');
        }

        // ToString
        public override string ToString()
        {
            return "" + this.Column + this.Row;
        }



    }
}
