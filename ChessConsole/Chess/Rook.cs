using GameBoard;
namespace Chess
{
    class Rook : Piece
    {
        //Constructor

        // @param Color color, Board board
        public Rook(Color color, Board board) : base(color, board)
        {

        }

        //Methods
        public override string ToString()
        {
            return "R";
        }
    }
}
