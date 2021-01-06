namespace GameBoard
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementsCounter { get; protected set; }
        public Board Board { get; protected set; }

        // Constructor

        // @param Color color, Board board
        public Piece(Color color, Board board)
        {
            this.Position = null;
            this.Color = color;
            this.Board = board;
            this.MovementsCounter = 0;
        }
    }
}
