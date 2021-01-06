

namespace GameBoard
{
    class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] _pieces;

        // Constructor

        // @param int rows, int columns
        public Board(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
            this._pieces = new Piece[rows, columns];
        }

        // Methods 

        /* Returns a Piece from the private Piece matrix
         * @param int row, int column
         * @return return _pieces[row, column];
         */
        public Piece GetPiece(int row, int column)
        {
            return _pieces[row, column];
        }

        /* Returns a Piece from the private Piece matrix
         * @param Position pos
         * @return return _pieces[pos.Row, pos.Column];
         */
        public Piece GetPiece(Position pos)
        {
            return _pieces[pos.Row, pos.Column];
        }

        /* Checks if a piece exists in a position
         * @param Position pos
         * @return bool _pieces[pos.Row, pos.Column] != null
         */
        public bool HasPiece(Position pos)
        {
            ValidatePosition(pos);
            return _pieces[pos.Row, pos.Column] != null;
        }
        /*  Inserts a new Piece on the game board
         *  @param Piece p, Position pos
         */
        public void InsertPiece(Piece p, Position pos)
        {
            _pieces[pos.Row, pos.Column] = p;
            p.Position = pos;
        }

        /* Checks if a position is valid
         * @param Position pos
         * @return bool
         */
        public bool IsValid(Position pos)
        {
            if (pos.Row < 0 || pos.Row >= this.Rows || pos.Column < 0 
                || pos.Column >= this.Columns)
                return false;
            return true;
        }

        /* Throws a exception if the position is not valid
         */
        public void ValidatePosition(Position pos)
        {
            if (!IsValid(pos))
                throw new GameBoardException("Invalid position!");
        }
    }
}
