using GameBoard;

namespace Chess
{
    class Bishop : Piece
    {
        //Constructor

        // @param Color color, Board board
        public Bishop(Color color, Board board) : base(color, board)
        {
        }

        //Methods

        /* Checks if it is posible to move to a position */
        private bool PossibleMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p == null || p.Color != this.Color;
        }

        public override string ToString()
        {
            return "B";
        }

        /* possible moves = true
        * @return bool[,]
        */
        public override bool[,] GetPossibleMoves()
        {
            bool[,] m = new bool[this.Board.Rows, this.Board.Columns];
            Position pos;

            //ne
            pos = new Position(this.Position.Row - 1, this.Position.Column + 1);
            while (this.Board.IsValid(pos) && PossibleMove(pos))
            {
                m[pos.Row, pos.Column] = true;

                if (this.Board.GetPiece(pos) != null && this.Board.GetPiece(pos).Color != this.Color)
                {
                    break;
                }

                pos.NewValues(pos.Row - 1, pos.Column + 1);
            }

            //se
            pos.NewValues(this.Position.Row + 1, this.Position.Column + 1);
            while (this.Board.IsValid(pos) && PossibleMove(pos))
            {
                m[pos.Row, pos.Column] = true;

                if (this.Board.GetPiece(pos) != null
                   && this.Board.GetPiece(pos).Color != this.Color)
                {
                    break;
                }

                pos.NewValues(pos.Row + 1, pos.Column + 1);
            }

            //sw
            pos.NewValues(this.Position.Row + 1 , this.Position.Column - 1);
            while (this.Board.IsValid(pos) && PossibleMove(pos))
            {
                m[pos.Row, pos.Column] = true;

                if (this.Board.GetPiece(pos) != null
                   && this.Board.GetPiece(pos).Color != this.Color)
                {
                    break;
                }

                pos.NewValues(pos.Row + 1, pos.Column - 1);
            }

            //nw
            pos.NewValues(this.Position.Row - 1, this.Position.Column - 1);
            while (this.Board.IsValid(pos) && PossibleMove(pos))
            {
                m[pos.Row, pos.Column] = true;

                if (this.Board.GetPiece(pos) != null
                   && this.Board.GetPiece(pos).Color != this.Color)
                {
                    break;
                }

                pos.NewValues(pos.Row - 1, pos.Column - 1);
            }
            return m;
        }

    }
}
