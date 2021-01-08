using GameBoard;

namespace Chess
{
    class King : Piece
    {
        //Constructor

        // @param Color color, Board board
        public King(Color color, Board board) : base(color, board)
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

            return m;
        }
    }
}
