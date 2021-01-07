

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
         * GameBoardException
         * @param Position pos
         * @return bool _pieces[pos.Row, pos.Column] != null
         */
        public bool HasPiece(Position pos)
        {
            ValidatePosition(pos);
            return GetPiece(pos) != null;
        }

        /* Inserts a new Piece on the game board
         * Throws GameBoardException
         * @param Piece p, Position pos
         */
        public void InsertPiece(Piece p, Position pos)
        {
            if (HasPiece(pos))
                throw new GameBoardException("There is already a piece in this position");
            _pieces[pos.Row, pos.Column] = p;
            p.Position = pos;
        }

        /* Removes a piece from the game board
         * @param Position pos
         * @return Piece Removed
         */
        public Piece RemovePiece(Position pos)
        {
            Piece p = GetPiece(pos);
            if (p == null)
                return null;

            p.Position = null;
            _pieces[pos.Row, pos.Column] = null;
            return p;
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
