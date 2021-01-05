﻿

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

        /* Return a Piece from the private Piece matrix
         * @param int row, int column
         * @return return _pieces[row, column];
         */
        public Piece GetPiece(int row, int column)
        {
            return _pieces[row, column];
        }
    }
}