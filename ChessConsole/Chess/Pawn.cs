using GameBoard;

namespace Chess
{
    class Pawn : Piece
    {
        //Constructor

        // @param Color color, Board board
        public Pawn(Color color, Board board) : base(color, board)
        {

        }

        //Methods
        /* Check if there is an enemy 
         * @param Position pos
         * @return bool
         */
        private bool HasEnemy(Position pos)
        {
            Piece p = this.Board.GetPiece(pos);
            return p != null && p.Color != this.Color;
        }

        /*Check id a position is empty
         * @param Position pos
         * @return bool
         */
        private bool EmptyPos(Position pos)
        {
            return this.Board.GetPiece(pos) == null;
        }

        /* Checks if it is posible to move to a position */
        private bool PossibleMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p == null || p.Color != this.Color;
        }
        public override string ToString()
        {
            return "P";
        }

        /* possible moves = true
         * @return bool[,]
         */
        public override bool[,] GetPossibleMoves()
        {
            bool[,] m = new bool[this.Board.Rows, this.Board.Columns];
            Position pos;

            if (this.Color == Color.White)
            {
                pos = new Position(this.Position.Row - 1, this.Position.Column);
                if (this.Board.IsValid(pos) && EmptyPos(pos))
                    m[pos.Row, pos.Column] = true;
                
                pos.NewValues(this.Position.Row - 2, this.Position.Column);
                if (this.Board.IsValid(pos) && EmptyPos(pos) && this.MoveCounter == 0)
                    m[pos.Row, pos.Column] = true;

                pos = new Position(this.Position.Row - 1, this.Position.Column - 1);
                if (this.Board.IsValid(pos) && HasEnemy(pos))
                    m[pos.Row, pos.Column] = true;

                pos = new Position(this.Position.Row - 1, this.Position.Column + 1);
                if (this.Board.IsValid(pos) && HasEnemy(pos))
                    m[pos.Row, pos.Column] = true;
            }
            else
            {
                pos = new Position(this.Position.Row + 1, this.Position.Column);
                if (this.Board.IsValid(pos) && EmptyPos(pos))
                    m[pos.Row, pos.Column] = true;

                pos.NewValues(this.Position.Row + 2, this.Position.Column);
                if (this.Board.IsValid(pos) && EmptyPos(pos) && this.MoveCounter == 0)
                    m[pos.Row, pos.Column] = true;

                pos = new Position(this.Position.Row + 1, this.Position.Column - 1);
                if (this.Board.IsValid(pos) && HasEnemy(pos))
                    m[pos.Row, pos.Column] = true;

                pos = new Position(this.Position.Row + 1, this.Position.Column + 1);
                if (this.Board.IsValid(pos) && HasEnemy(pos))
                    m[pos.Row, pos.Column] = true;
            }
            return m;
        }
    }
}
