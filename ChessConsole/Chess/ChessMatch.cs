using GameBoard;

namespace Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set;}

        // Constructor
        public ChessMatch()
        {
            this.Board = new Board(8, 8);
            this.Turn = 1;
            this.CurrentPlayer = Color.White;
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

        private void SwitchPlayers()
        {
            if (this.CurrentPlayer == Color.Black)
                this.CurrentPlayer = Color.White;
            else
                this.CurrentPlayer = Color.Black;
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

        /* Executes a chess move*/
        public void ExecuteMove(Position from, Position to)
        {
            Move(from, to);
            this.Turn++;
            SwitchPlayers();
        }

        /* Checks if the position FROM is valide\
         * throws GameBoardException
         * @param Position pos
         */
        public void ValidateFromPosition(Position pos)
        {
            Piece p = this.Board.GetPiece(pos);
            if (p == null)
                throw new GameBoardException("There is no piece in the chosen position!");

            if(p.Color != this.CurrentPlayer)
                throw new GameBoardException("The piece chosen is not yours!");

            if(!p.HasPossibleMoves())
                throw new GameBoardException("You cannot move the piece chosen!");
        }

        /* Checks if the position TO is valide
         * throws GameBoardException
         * @param Position from, Position to
         */
        public void ValidateToPosition(Position from, Position to)
        {
            if (!Board.GetPiece(from).CanMoveTo(to))
                throw new GameBoardException("Invalid position!");
        }

    }
}
