using System;
using System.Collections.Generic;
using ChessConsole;
using ChessConsole.Helpers;
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
        public Piece EnPassant { get; private set; }

        private HashSet<Piece> _pieces;
        private HashSet<Piece> _captured;

        private char[] files;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameType">What chess game mode the user wants to play</param>
        public ChessMatch(int gameType)
        {
            if (gameType == 0) { Finished = true; return; } // 0 is exit

            init();
            switch (gameType)
            {
                case 1:
                    SetUpChessBoardTraditional();
                    break;
                case 2:
                    SetUpChess960();
                    break;
               //case 3:
               //    SomeOtherGameMode()...
               //    break;

                default:
                    IO.SetError("Oh no! Something went wrong, this shouldn't have happened.", "Press any key to try again.");
                    break;
            }
            
        }

        /// <summary>
        /// Initializing necessary things for chess match
        /// </summary>
        private void init()
        {
            this.Board = new Board(8, 8);
            this.Turn = 1;
            this.CurrentPlayer = Color.White;
            this.Finished = false;
            this.Check = false;
            this.EnPassant = null;

            this._pieces = new HashSet<Piece>();
            this._captured = new HashSet<Piece>();

            files = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        }

        /// <summary>
        /// Most chess variants don't alter rows 2 or 7 so can just call this method instead of repeating code
        /// </summary>
        private void SetUpRows2And7()
        {
            // Row 2
            InsertNewPiece('a', 2, new Pawn(Color.White, Board, this));
            InsertNewPiece('b', 2, new Pawn(Color.White, Board, this));
            InsertNewPiece('c', 2, new Pawn(Color.White, Board, this));
            InsertNewPiece('d', 2, new Pawn(Color.White, Board, this));
            InsertNewPiece('e', 2, new Pawn(Color.White, Board, this));
            InsertNewPiece('f', 2, new Pawn(Color.White, Board, this));
            InsertNewPiece('g', 2, new Pawn(Color.White, Board, this));
            InsertNewPiece('h', 2, new Pawn(Color.White, Board, this));

            // Row 7
            InsertNewPiece('a', 7, new Pawn(Color.Black, Board, this));
            InsertNewPiece('b', 7, new Pawn(Color.Black, Board, this));
            InsertNewPiece('c', 7, new Pawn(Color.Black, Board, this));
            InsertNewPiece('d', 7, new Pawn(Color.Black, Board, this));
            InsertNewPiece('e', 7, new Pawn(Color.Black, Board, this));
            InsertNewPiece('f', 7, new Pawn(Color.Black, Board, this));
            InsertNewPiece('g', 7, new Pawn(Color.Black, Board, this));
            InsertNewPiece('h', 7, new Pawn(Color.Black, Board, this));
        }

        // Methods

        /* private method to set up the chessboard
         */
        private void SetUpChessBoardTraditional()
        {
            SetUpRows2And7();

            // Row 1
            InsertNewPiece('a', 1, new Rook(Color.White, Board));
            InsertNewPiece('b', 1, new Knight(Color.White, Board));
            InsertNewPiece('c', 1, new Bishop(Color.White, Board));
            InsertNewPiece('d', 1, new Queen(Color.White, Board));
            InsertNewPiece('e', 1, new King(Color.White, Board, this));
            InsertNewPiece('f', 1, new Bishop(Color.White, Board));
            InsertNewPiece('g', 1, new Knight(Color.White, Board));
            InsertNewPiece('h', 1, new Rook(Color.White, Board));
            
            // Row 2
            InsertNewPiece('a', 8, new Rook(Color.Black, Board));
            InsertNewPiece('b', 8, new Knight(Color.Black, Board));
            InsertNewPiece('c', 8, new Bishop(Color.Black, Board));
            InsertNewPiece('d', 8, new Queen(Color.Black, Board));
            InsertNewPiece('e', 8, new King(Color.Black, Board, this));
            InsertNewPiece('f', 8, new Bishop(Color.Black, Board));
            InsertNewPiece('g', 8, new Knight(Color.Black, Board));
            InsertNewPiece('h', 8, new Rook(Color.Black, Board));

        }

        private void SetUpChess960()
        {
            SetUpRows2And7();


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

            /*** King-side castling ***/
            if (p is King && to.Column == from.Column + 2)
            {
                Position fromR = new Position(from.Row, from.Column + 3);
                Position toR = new Position(from.Row, from.Column + 1);
                Piece R = this.Board.RemovePiece(fromR);
                R.IncreaseMoveCounter();
                this.Board.InsertPiece(R, toR);
            }

            /*** Queen-side castling ***/
            if (p is King && to.Column == from.Column - 2)
            {
                Position fromR = new Position(from.Row, from.Column - 4);
                Position toR = new Position(from.Row, from.Column - 1);
                Piece R = this.Board.RemovePiece(fromR);
                R.IncreaseMoveCounter();
                this.Board.InsertPiece(R, toR);
            }

            /*** En Passant ***/
            if(p is Pawn)
            {
                if(from.Column != to.Column && captured == null)
                {
                    Position posP;

                    if(p.Color == Color.White)
                    {
                        posP = new Position(to.Row + 1, to.Column);
                    }
                    else
                    {
                        posP = new Position(to.Row - 1, to.Column);
                    }

                    captured = this.Board.RemovePiece(posP);
                    _captured.Add(captured);
                }

            }

            return captured;
        }

        /* Executes a chess move
         * @param Position from, Position to
         */
        public void ExecuteMove(Position from, Position to)
        {
            Piece captured = Move(from, to);
            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(from, to, captured);
                throw new GameBoardException("You cannot put yourself in check.");
            }

            Piece p = this.Board.GetPiece(to);

            /*** Promotion ***/
            if(p is Pawn)
            {
                if ((p.Color == Color.White && to.Row == 0) ||
                   (p.Color == Color.Black && to.Row == 7))
                {
                    p = this.Board.RemovePiece(to);
                    this._pieces.Remove(p);
                    bool wrong = true;
                    Piece np = null;

                    do
                    {
                        Console.Clear();
                        View.PrintMatch(this, null);
                        Console.WriteLine("PROMOTION\n" +
                                          "Knight [N/n]\n" +
                                          "Rook   [R/r]\n" +
                                          "Bishop [B/b]\n" +
                                          "Queen  [Q/q]\n" +
                                          "Choose a piece: ");
                        string c = Console.ReadLine();

                        switch (c.ToUpper())
                        {
                            case "N":
                                np = new Knight(p.Color, this.Board);
                                wrong = false;
                                break;

                            case "R":
                                np = new Rook(p.Color, this.Board);
                                wrong = false;
                                break;

                            case "B":
                                np = new Bishop(p.Color, this.Board);
                                wrong = false;
                                break;

                            case "Q": 
                                np = new Queen(p.Color, this.Board);
                                wrong = false;
                                break;
                            
                            default:
                                Console.WriteLine("Invalid Piece!" + "\nPress enter to continue...");
                                Console.ReadLine();
                                wrong = true;
                                break;
                        }
                    } while (wrong);

                    this.Board.InsertPiece(np, to);
                    this._pieces.Add(np);
                                        
                }
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

            /*** En Passant ***/
            if (p is Pawn && (to.Row == from.Row - 2 || to.Row == from.Row + 2))
                this.EnPassant = p;
            else
                this.EnPassant = null;
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

            /*** King-side castling ***/
            if (p is King && to.Column == from.Column + 2)
            {
                Position fromR = new Position(from.Row, from.Column + 3);
                Position toR = new Position(from.Row, from.Column + 1);
                Piece R = this.Board.RemovePiece(toR);
                R.DecreaseMoveCounter();
                this.Board.InsertPiece(R, fromR);
            }

            /*** Queen-side castling ***/
            if (p is King && to.Column == from.Column - 2)
            {
                Position fromR = new Position(from.Row, from.Column - 4);
                Position toR = new Position(from.Row, from.Column - 1);
                Piece R = this.Board.RemovePiece(toR);
                R.DecreaseMoveCounter();
                this.Board.InsertPiece(R, fromR);
            }

            /*** En Passant ***/
            if(p is Pawn)
            {
                if(from.Column != to.Column && captured == this.EnPassant)
                {
                    Piece pawn = this.Board.GetPiece(to);
                    Position posP;

                    if(p.Color == Color.White)
                        posP = new Position(3, to.Column);
                    else
                        posP = new Position(3, to.Column);

                    this.Board.InsertPiece(pawn, posP);
                }
            }

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
