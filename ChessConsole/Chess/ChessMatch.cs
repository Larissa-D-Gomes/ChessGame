using System;
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
        public bool Check { get; private set; }
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _captured;

        // Constructor
        public ChessMatch()
        {
            this.Board = new Board(8, 8);
            this.Turn = 1;
            this.CurrentPlayer = Color.White;
            this.Finished = false;
            this.Check = false;

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

        /* Returns the opponent's color
         * @param Color color
         * @return Color
         */
        private Color Opponent(Color color)
        {
            if (color == Color.White)
                return Color.Black;
            else
                return Color.White;
        }
        /* Returns a king 
         * Piece king
         */
        private Piece GetKing(Color color)
        {
            foreach( Piece c in GamePieces(color))
            {
                if(c is King)
                {
                    return c;
                }
            }
            return null;
        }

        /* Switches the current player */
        private void SwitchPlayers()
        {
            if (this.CurrentPlayer == Color.Black)
                this.CurrentPlayer = Color.White;
            else
                this.CurrentPlayer = Color.Black;
        }


        /* Checks if a king is in check
         * @param Color color
         * @return bool
         */
        public bool IsInCheck(Color color)
        {
            Piece king = GetKing(color);

            if (king == null)
                throw new GameBoardException("There is not a " + color + " king on the game board." );

            foreach(Piece x in GamePieces(Opponent(color)))
            {
                bool[,] m = x.GetPossibleMoves();
                if (m[king.Position.Row, king.Position.Column])
                    return true;
            }
            return false;
        }

        /* Tests if a king is in checkmate*/
        public bool IsInCheckmate(Color color)
        {
            if (!IsInCheck(color))
                return false;

            foreach(Piece x in GamePieces(color))
            {
                bool[,] m = x.GetPossibleMoves();

                for(int i = 0; i < this.Board.Rows; i++)
                {
                    for(int j = 0; j < this.Board.Columns; j++)
                    {
                        if(m[i, j])
                        {
                            Position from = x.Position;
                            Position to = new Position(i, j);
                            Piece captured = Move(from, to);
                            bool check = IsInCheck(color);
                            UndoMove(from, to, captured);
                            
                            if (!check)
                                return false;
                        }
                    }
                }
            }
            return true;
        }

        /* Returns a HashSet that contains the captured pieces of a color
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
         * @param Position from, Position to
         * @return Piece p
         */
        public Piece Move(Position from, Position to)
        {
            Piece p = this.Board.RemovePiece(from);
            p.IncreaseMoveCounter();
            Piece captured = this.Board.RemovePiece(to);
            this.Board.InsertPiece(p, to);

            if (captured != null)
                this._captured.Add(captured);

            return captured;
        }

        /* Executes a chess move*/
        public void ExecuteMove(Position from, Position to)
        {
            Piece captured = Move(from, to);
            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(from, to, captured);
                throw new GameBoardException("You cannot put yourself in check.");
            }

            if (IsInCheck(Opponent(CurrentPlayer)))
            {
                this.Check = true;
            }
            else
            {
                this.Check = false;
            }

            if (IsInCheckmate(Opponent(CurrentPlayer)))
            {
                this.Finished = true;
            }
            else
            {
                this.Turn++;
                SwitchPlayers();
            }
        }

        public void UndoMove(Position from, Position to, Piece captured)
        {
            
            Piece p = this.Board.RemovePiece(to);
            p.DecreaseMoveCounter();

            if (captured != null)
            {
                this.Board.InsertPiece(captured, to);
                this._captured.Remove(captured);            
            }
            this.Board.InsertPiece(p, from);

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
