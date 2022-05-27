using System.Collections.Generic;
using System.Linq;

namespace Puzzle2
{
    public class AlgorithmX
    {
        private readonly ColumnObject head = new ColumnObject(-1);
        private readonly ColumnObject[] indexedColumns;

        public AlgorithmX(int columns)
        {
            indexedColumns = new ColumnObject[columns];

            for (int i = 0; i < indexedColumns.Length; i++)
            {
                ColumnObject column = new ColumnObject(i);
                head.InsertLeft(column);
                indexedColumns[i] = column;                
            }
        }

        public void AddRows(int[,] rows)
        {
            for (int y = 0; y < rows.GetLength(0); y++)
            {
                DataObject currentColumn = null;
                for (int x = 0; x < rows.GetLength(1); x++)
                {
                    if (rows[y, x] == 0) continue;
                    ColumnObject column = indexedColumns[x];

                    DataObject dataObj = new DataObject(column, y);
                    column.InsertUp(dataObj);
                    column.Size += 1;

                    if (currentColumn == null)
                    {
                        currentColumn = dataObj;
                    }
                    else
                    {
                        currentColumn.InsertLeft(dataObj);
                    }
                }
            }
        }

        public IEnumerable<List<int>> GetAllExactCovers()
        {
            return Search(new Stack<int>());
        }

        private IEnumerable<List<int>> Search(Stack<int> partialSolution)
        {
            DataObject headRight = head.Right;
            if (headRight == head)
            {
                yield return partialSolution.ToList();
            }

            ColumnObject column = headRight.Column;
            int minSize = column.Size;

            for (DataObject dataObjX = headRight.Right; dataObjX != head; dataObjX = dataObjX.Right)
            {
                int colSize = dataObjX.Column.Size;
                if (colSize < minSize)
                {
                    column = dataObjX.Column;
                    minSize = colSize;
                }
            }

            CoverColumn(column);

            for (DataObject dataObjY = column.Down; dataObjY != column; dataObjY = dataObjY.Down)
            {
                partialSolution.Push(dataObjY.RowIndex);

                for (DataObject dataObjX = dataObjY.Right; dataObjX != dataObjY; dataObjX = dataObjX.Right)
                {
                    CoverColumn(dataObjX.Column);
                }

                IEnumerable<List<int>> solutions = Search(partialSolution);
                //List<List<int>> yieldList = Search(partialSolution).ToList();

                /*foreach (var item in yieldList)
                {
                    item.Sort();
                }*/

                foreach (List<int> solution in solutions)
                {
                    yield return solution;
                }

                partialSolution.Pop();

                for (DataObject dataObjX = dataObjY.Left; dataObjX != dataObjY; dataObjX = dataObjX.Left)
                {
                    UnCoverColumn(dataObjX.Column);
                }
            }

            UnCoverColumn(column);
        }

        private void CoverColumn(DataObject column)
        {
            column.Right.Left = column.Left;
            column.Left.Right = column.Right;

            for (DataObject dataObjY = column.Down; dataObjY != column; dataObjY = dataObjY.Down)
            {
                for (DataObject dataObjX = dataObjY.Right; dataObjX != dataObjY; dataObjX = dataObjX.Right)
                {
                    dataObjX.Down.Up = dataObjX.Up;
                    dataObjX.Up.Down = dataObjX.Down;
                    dataObjX.Column.Size -= 1;
                }
            }
        }

        private void UnCoverColumn(DataObject column)
        {
            for (DataObject dataObjY = column.Up; dataObjY != column; dataObjY = dataObjY.Up)
            {
                for (DataObject dataObjX = dataObjY.Left; dataObjX != dataObjY; dataObjX = dataObjX.Left)
                {
                    dataObjX.Column.Size += 1;
                    dataObjX.Down.Up = dataObjX;
                    dataObjX.Up.Down = dataObjX;
                }
            }

            column.Right.Left = column;
            column.Left.Right = column;
        }
    }
}