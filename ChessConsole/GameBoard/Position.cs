
namespace GameBoard
{
    class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }

        // Constructor

        // @param int row, int column
        public Position(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        // Methods 
        public override string ToString()
        {
            return this.Row + ", " + this.Column;
        }




    }
}
