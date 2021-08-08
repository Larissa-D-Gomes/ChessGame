
namespace GameBoard
{
    public class Position
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
        /* Defines new values */
        public void NewValues(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
        public override string ToString()
        {
            return this.Row + ", " + this.Column;
        }




    }
}
