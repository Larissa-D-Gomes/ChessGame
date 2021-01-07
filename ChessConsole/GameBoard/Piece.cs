namespace GameBoard
{
    class Piece
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

        // Increase the MoveCounter by one
        public void IncreaseMoveCounter()
        {
            this.MoveCounter++;
        }
    }
}
