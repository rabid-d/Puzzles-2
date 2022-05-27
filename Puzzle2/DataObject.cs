namespace Puzzle2
{
    class DataObject
    {
        public DataObject Left { get; set; }
        public DataObject Right { get; set; }
        public DataObject Up { get; set; }
        public DataObject Down { get; set; }

        public ColumnObject Column { get; protected set; }

        public int RowIndex { get; set; }

        public DataObject(ColumnObject column, int rowIndex)
        {
            Left = this;
            Right = this;
            Up = this;
            Down = this;

            Column = column;

            RowIndex = rowIndex;
        }

        public void InsertLeft(DataObject dataObj)
        {
            DataObject left = Left;
            Left = dataObj;
            dataObj.Right = this;
            dataObj.Left = left;
            left.Right = dataObj;
        }

        /*public void InsertRight(DataObject dataOjb)
        {
            DataObject right = Right;
            Right = dataOjb;
            dataOjb.Left = this;
            dataOjb.Right = right;
            right.Left = dataOjb;
        }

        public void InsertDown(DataObject dataObj)
        {
            DataObject down = Down;
            Down = dataObj;
            dataObj.Up = this;
            dataObj.Down = down;
            down.Up = dataObj;
        }*/

        public void InsertUp(DataObject dataObj)
        {
            DataObject up = Up;
            Up = dataObj;
            dataObj.Down = this;
            dataObj.Up = up;
            up.Down = dataObj;
        }
    }
}
