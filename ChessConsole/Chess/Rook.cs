using GameBoard;
namespace Chess
{
    public class Rook : Piece
    {
        //Constructor

        // @param Color color, Board board
        public Rook(Color color, Board board) : base(color, board)
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
            return "R";
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
            while(this.Board.IsValid(pos) && PossibleMove(pos))
            {
                m[pos.Row, pos.Column] = true;
                
                if(this.Board.GetPiece(pos) != null && this.Board.GetPiece(pos).Color != this.Color)
                {
                    break;
                }

                pos.Row = pos.Row - 1;
            }

            //s
            pos.NewValues(this.Position.Row + 1, this.Position.Column);
            while (this.Board.IsValid(pos) && PossibleMove(pos))
            {
                m[pos.Row, pos.Column] = true;

                if (this.Board.GetPiece(pos) != null
                   && this.Board.GetPiece(pos).Color != this.Color)
                {
                    break;
                }

                pos.Row = pos.Row + 1;
            }

            //e
            pos.NewValues(this.Position.Row, this.Position.Column + 1);
            while (this.Board.IsValid(pos) && PossibleMove(pos))
            {
                m[pos.Row, pos.Column] = true;

                if (this.Board.GetPiece(pos) != null
                   && this.Board.GetPiece(pos).Color != this.Color)
                {
                    break;
                }

                pos.Column = pos.Column + 1;
            }

            //w
            pos.NewValues(this.Position.Row, this.Position.Column - 1);
            while (this.Board.IsValid(pos) && PossibleMove(pos))
            {
                m[pos.Row, pos.Column] = true;

                if (this.Board.GetPiece(pos) != null
                   && this.Board.GetPiece(pos).Color != this.Color)
                {
                    break;
                }

                pos.Column = pos.Column - 1;
            }
            return m;

        }
    }
}
