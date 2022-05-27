namespace Puzzle2
{
    class ColumnObject : DataObject
    {
        //private readonly int index;

        //public int Index { get { return index; } }
        public int Index { get; }
        public int Size { get; set; }

        public ColumnObject(int index) : base(null, -1)
        {
            Index = index;
            Column = this;
        }
    }
}