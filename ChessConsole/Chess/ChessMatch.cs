using System.Collections.Generic;
using GameBoard;

namespace Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set;}
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _captured;

        // Constructor
        public ChessMatch()
        {
            this.Board = new Board(8, 8);
            this.Turn = 1;
            this.CurrentPlayer = Color.White;
            this.Finished = false;

            this._pieces = new HashSet<Piece>();
            this._captured = new HashSet<Piece>();

            SetUpChessBoard();
        }

        // Methods

        /* private method to set up the chessboard
         */
        private void SetUpChessBoard()
        {
            InsertNewPiece('c', 1, new Rook(Color.White, Board));
            InsertNewPiece('c', 2, new Rook(Color.White, Board));
            InsertNewPiece('d', 2, new Rook(Color.White, Board));
            InsertNewPiece('e', 2, new Rook(Color.White, Board));
            InsertNewPiece('e', 1, new Rook(Color.White, Board));
            InsertNewPiece('d', 1, new King(Color.White, Board));

            InsertNewPiece('c', 7, new Rook(Color.Black, Board));
            InsertNewPiece('c', 8, new Rook(Color.Black, Board));
            InsertNewPiece('d', 7, new Rook(Color.Black, Board));
            InsertNewPiece('e', 7, new Rook(Color.Black, Board));
            InsertNewPiece('e', 8, new Rook(Color.Black, Board));
            InsertNewPiece('d', 8, new King(Color.Black, Board));
        }


        private void SwitchPlayers()
        {
            if (this.CurrentPlayer == Color.Black)
                this.CurrentPlayer = Color.White;
            else
                this.CurrentPlayer = Color.Black;
        }

        /* Return a HashSet that contains the captured pieces of a color
         */
        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece p in this._captured){
                if(p.Color == color)
                {
                    aux.Add(p);
                }
            }
            return aux;
        }

        /* Return a HashSet that contains pieces that are still in play
         */
        public HashSet<Piece> GamePieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece p in this._pieces)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }

            aux.ExceptWith(CapturedPieces(color));

            return aux;
        }

        /* Inserts new pieces on the game board
         * @param char column, int row, Piece 
         */
        public void InsertNewPiece(char column, int row, Piece p)
        {
            this.Board.InsertPiece(p, new ChessPosition(column, row).ToPosition());
            _pieces.Add(p);
        }

        /* Moves a chess Piece 
         */
        public void Move(Position from, Position to)
        {
            Piece p = this.Board.RemovePiece(from);
            p.IncreaseMoveCounter();
            Piece captured = this.Board.RemovePiece(to);
            this.Board.InsertPiece(p, to);

            if (captured != null)
                this._captured.Add(captured);
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
                throw new GameBoardException("The chosen piece is not yours!");

            if(!p.HasPossibleMoves())
                throw new GameBoardException("You cannot move the chosen piece!");
        }

        /* Checks if the position TO is valide
         * throws GameBoardException
         * @param Position from, Position to
         */
        public void ValidateToPosition(Position from, Position to)
        {
            if (!Board.GetPiece(from).CanMoveTo(to))
                throw new GameBoardException("Illegal move!");
        }

    }
}
