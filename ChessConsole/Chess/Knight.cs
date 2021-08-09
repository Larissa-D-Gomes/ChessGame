using GameBoard;

namespace Chess
{
    public class Knight : Piece
    {
        //Constructor

        // @param Color color, Board board
        public Knight(Color color, Board board) : base(color, board)
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
            return "N";
        }

        /* possible moves = true
         * @return bool[,]
         */
        public override bool[,] GetPossibleMoves()
        {
            bool[,] m = new bool[this.Board.Rows, this.Board.Columns];
            Position pos;

            pos = new Position(this.Position.Row - 1, this.Position.Column - 2);

            if (Board.IsValid(pos) && PossibleMove(pos))
                m[pos.Row, pos.Column] = true;


            pos.NewValues(this.Position.Row - 2, this.Position.Column - 1);

            if (Board.IsValid(pos) && PossibleMove(pos))
                m[pos.Row, pos.Column] = true;

            
            pos.NewValues(this.Position.Row - 2, this.Position.Column + 1);

            if (Board.IsValid(pos) && PossibleMove(pos))
                m[pos.Row, pos.Column] = true;

            
            pos.NewValues(this.Position.Row - 1, this.Position.Column + 2);

            if (Board.IsValid(pos) && PossibleMove(pos))
                m[pos.Row, pos.Column] = true;

           
            pos.NewValues(this.Position.Row + 1, this.Position.Column + 2);

            if (Board.IsValid(pos) && PossibleMove(pos))
                m[pos.Row, pos.Column] = true;

            
            pos.NewValues(this.Position.Row + 2, this.Position.Column + 1);

            if (Board.IsValid(pos) && PossibleMove(pos))
                m[pos.Row, pos.Column] = true;

            
            pos.NewValues(this.Position.Row + 2, this.Position.Column - 1);

            if (Board.IsValid(pos) && PossibleMove(pos))
                m[pos.Row, pos.Column] = true;

            
            pos.NewValues(this.Position.Row + 1, this.Position.Column - 2);

            if (Board.IsValid(pos) && PossibleMove(pos))
                m[pos.Row, pos.Column] = true;

            return m;
        }
    }
}
