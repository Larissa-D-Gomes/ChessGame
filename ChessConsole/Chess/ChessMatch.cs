using GameBoard;

namespace Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        private int _turn;
        private Color _currentPlayer;
        public bool Finished { get; private set;}

        // Constructor
        public ChessMatch()
        {
            this.Board = new Board(8, 8);
            this._turn = 1;
            this._currentPlayer = Color.White;
            this.Finished = false;
            SetUpChessBoard();
        }

        // Methods

        /* private method to set up the chessboard
         */
        private void SetUpChessBoard()
        {
            Board.InsertPiece(new Rook(Color.White, Board), new ChessPosition('c', 1).ToPosition());
            Board.InsertPiece(new Rook(Color.White, Board), new ChessPosition('c', 2).ToPosition());
            Board.InsertPiece(new Rook(Color.White, Board), new ChessPosition('d', 2).ToPosition());
            Board.InsertPiece(new Rook(Color.White, Board), new ChessPosition('e', 2).ToPosition());
            Board.InsertPiece(new Rook(Color.White, Board), new ChessPosition('e', 1).ToPosition());
            Board.InsertPiece(new King(Color.White, Board), new ChessPosition('d', 1).ToPosition());

            Board.InsertPiece(new Rook(Color.Black, Board), new ChessPosition('c', 7).ToPosition());
            Board.InsertPiece(new Rook(Color.Black, Board), new ChessPosition('c', 8).ToPosition());
            Board.InsertPiece(new Rook(Color.Black, Board), new ChessPosition('d', 7).ToPosition());
            Board.InsertPiece(new Rook(Color.Black, Board), new ChessPosition('e', 7).ToPosition());
            Board.InsertPiece(new Rook(Color.Black, Board), new ChessPosition('e', 8).ToPosition());
            Board.InsertPiece(new King(Color.Black, Board), new ChessPosition('d', 8).ToPosition());
        }

        /* Moves a chess Piece 
         */
        public void Move(Position from, Position to)
        {
            Piece p = this.Board.RemovePiece(from);
            p.IncreaseMoveCounter();
            Piece removed = this.Board.RemovePiece(to);
            this.Board.InsertPiece(p, to);
        }
    }
}
