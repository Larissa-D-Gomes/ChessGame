using GameBoard;

namespace Chess
{
    class King : Piece
    {
        private ChessMatch _match;
        //Constructor

        // @param Color color, Board board
        public King(Color color, Board board, ChessMatch match) : base(color, board)
        {
            this._match = match;
        }

        //Methods

        /* Checks if it is posible to move to a position */
        private bool PossibleMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p == null || p.Color != this.Color;
        }

        /* Checks if the tower is eligible for Castling
         * @param Position pos
         * @return bool
         */
        private bool TestRookCastling(Position pos)
        {
            Piece p = this.Board.GetPiece(pos);
            return p != null && p is Rook && p.Color == this.Color && p.MoveCounter == 0;
        }

        public override string ToString()
        {
            return "K";
        }

        /* possible moves = true
         * @return bool[,]
         */
        public override bool[,] GetPossibleMoves()
        {
            bool[,] m = new bool[this.Board.Rows, this.Board.Columns];
            Position pos;
            
            //n
            pos = new Position(this.Position.Row - 1, this.Position.Column);

            if (Board.IsValid(pos) && PossibleMove(pos))
                m[pos.Row, pos.Column] = true;

            //ne 
            pos.NewValues(this.Position.Row - 1, this.Position.Column + 1);

            if (Board.IsValid(pos) && PossibleMove(pos))
                m[pos.Row, pos.Column] = true;

            //e 
            pos.NewValues(this.Position.Row, this.Position.Column + 1);

            if (Board.IsValid(pos) && PossibleMove(pos))
                m[pos.Row, pos.Column] = true;

            //se 
            pos.NewValues(this.Position.Row + 1, this.Position.Column + 1);

            if (Board.IsValid(pos) && PossibleMove(pos))
                m[pos.Row, pos.Column] = true;

            //s 
            pos.NewValues(this.Position.Row + 1, this.Position.Column);

            if (Board.IsValid(pos) && PossibleMove(pos))
                m[pos.Row, pos.Column] = true;

            //sw 
            pos.NewValues(this.Position.Row + 1, this.Position.Column - 1);

            if (Board.IsValid(pos) && PossibleMove(pos))
                m[pos.Row, pos.Column] = true;

            //w 
            pos.NewValues(this.Position.Row, this.Position.Column - 1);

            if (Board.IsValid(pos) && PossibleMove(pos))
                m[pos.Row, pos.Column] = true;

            //nw 
            pos.NewValues(this.Position.Row - 1, this.Position.Column - 1);

            if (Board.IsValid(pos) && PossibleMove(pos))
                m[pos.Row, pos.Column] = true;

            
            if(this.MoveCounter == 0 && !_match.Check)
            {
                /*** King-side castling ***/
                Position PosR1 = new Position(Position.Row, Position.Column + 3);
               
                if (TestRookCastling(PosR1))
                {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if(this.Board.GetPiece(p1) == null && this.Board.GetPiece(p2) == null)
                    {
                        m[this.Position.Row, this.Position.Column + 2] = true;
                    }
                }

                /*** Queen-side castling ***/
                Position PosR2 = new Position(Position.Row, Position.Column - 4);

                if (TestRookCastling(PosR1))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (this.Board.GetPiece(p1) == null && this.Board.GetPiece(p2) == null && 
                        this.Board.GetPiece(p3) == null)
                    {
                        m[this.Position.Row, this.Position.Column - 2] = true;
                    }
                }

            }



            return m;
        }
    }
}
