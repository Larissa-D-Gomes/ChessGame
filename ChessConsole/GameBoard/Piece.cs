namespace GameBoard
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MoveCounter { get; protected set; }
        public Board Board { get; protected set; }

        // Constructor

        // @param Color color, Board board
        public Piece(Color color, Board board)
        {
            this.Position = null;
            this.Color = color;
            this.Board = board;
            this.MoveCounter = 0;
        }

        // Methods

        // Increases the MoveCounter by one
        public void IncreaseMoveCounter()
        {
            this.MoveCounter++;
        }

        // Decreases the MoveCounter by one
        public void DecreaseMoveCounter()
        {
            this.MoveCounter--;
        }

        /* Checks if a piece can move to a position
         * @param Position pos
         * @return bool GetPossibleMoves()[p.Row, p.Column]
         */
        public bool CanMoveTo(Position p)
        {
            return GetPossibleMoves()[p.Row, p.Column];
        }

        
        /* Checks if a piece contains a possible move
         * @return bool
         */
        public bool HasPossibleMoves()
        {
            bool[,] m = GetPossibleMoves();
            for(int  i = 0; i < this.Board.Rows; i++)
                for (int j = 0; j < this.Board.Rows; j++)
                    if(m[i, j])
                        return true;

            return false;
        }

        /* possible moves = true
         * @return bool[,]
         */
        public abstract bool[,] GetPossibleMoves();
    }
}
