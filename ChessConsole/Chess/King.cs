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
        public override string ToString()
        {
            return "K";
        }
    }
}
