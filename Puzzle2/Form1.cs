using NumSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Puzzle2
{
    public partial class Form1 : Form
    {
        private int[,] incidenceMatrix;
        private IEnumerator<List<int>> solutionEnumerator;
        private IEnumerable<List<int>> sols;
        private Dictionary<int, int> colors;
        private int GridSize => int.Parse(sizeTextBox.Text);

        public Form1()
        {
            InitializeComponent();

            int[,] board = new int[8, 8] {
             // 1  2  3  4  5  6  7  8
               {1, 1, 2, 3, 3, 3, 3, 5},//1
               {1, 2, 2, 2, 3, 4, 4, 5},//2
               {1, 1, 2, 4, 4, 4, 8, 5},//3
               {6, 6, 7, 7, 8, 8, 8, 5},//4
               {6, 6, 6, 7, 8,-4, 8,-3},//5
               {9, 6,-1,-4,-4,-4,-3,-3},//6
               {9, 9,-1,-1,-1,-2,-2,-3},//7
               {9, 9, 9,-1,-2,-2,-3,-3},//8
            };
            int[,] board2 = new int[8, 8] {
             // 0  1  2  3  4  5  6  7  8
               {1, 1, 1, 1, 2, 2, 2, 2},//0
               {1, 1, 1, 1, 2, 2, 2, 2},//1
               {1, 1, 1, 1, 2, 2, 2, 2},//2
               {3, 3, 3, 3, 2, 2, 2, 2},//3
               {3, 3, 3, 3, 2, 2, 2, 2},//4
               {3, 3, 3, 3, 5, 5, 5, 5},//5
               {3, 3, 3, 3, 5, 5, 5, 5},//6
               {3, 3, 3, 3, 5, 5, 5, 5},//7
            };
            int[,] board3 = new int[8, 8] {
             // 0  1  2  3  4  5  6  7  8
               {1, 1, 1, 1, 2, 2, 2, 2},//0
               {1, 1, 1, 2, 2, 2, 2, 2},//1
               {1, 1, 1, 1, 2, 2, 2, 2},//2
               {3, 3, 3, 3, 2, 2, 2, 2},//3
               {3, 3, 3, 3, 2, 2, 2, 2},//4
               {3, 3, 3, 3, 5, 5, 5, 5},//5
               {3, 3, 3, 5, 5, 5, 5, 5},//6
               {3, 3, 3, 3, 5, 5, 5, 5},//7
            };
            InitBoard(board, board.GetLength(0));
            int[,] b = new int[8, 8] {
             // 0  1  2  3  4  5  6  7  8
               {1, 1, 2, 3, 3, 3, 3, 5},//0
               {1, 2, 2, 2, 3, 4, 4, 5},//1
               {1, 1, 2, 4, 4, 4, 8, 5},//2
               {6, 6, 7, 7, 8, 8, 8, 5},//3
               {6, 6, 6, 7, 8,-4, 8,-3},//4
               {9, 6,-1,-4,-4,-4,-3,-3},//5
               {9, 9,-1,-1,-1,-2,-2,-3},//6
               {9, 9, 9,-1,-2,-2,-3,-3},//7
            };
        }

        private void ShowSolution(List<int> solution)
        {
            Random rnd = new Random();
            Color color;
            int index = 1;
            solution.Sort();
            foreach (int rowIndex in solution)
            {
                int[] row = Enumerable.Range(0, incidenceMatrix.GetLength(1))
                    .Select(y => incidenceMatrix[rowIndex, y])
                    .ToArray();
                color = Color.FromArgb(colors.FirstOrDefault(x => x.Value == index).Key);

                index++;
                for (int j = 0; j < GridSize; j++)
                {
                    for (int k = 0; k < GridSize; k++)
                    {
                        int cellId = j * GridSize + k;
                        if (row[cellId] == 1)
                        {
                            solvedDataGridView.Rows[j].Cells[k].Style.BackColor = color;
                        }
                    }
                }
            }
        }

        private int[,] GetIncidenceMatrix(List<int[,]> pieces)
        {
            List<int[]> matrix = new List<int[]>();
            int[,] tempBoard = new int[GridSize, GridSize];
            foreach (int[,] origPiece in pieces)
            {
                List<int[,]> rotatedPieces = RotatePiece(origPiece);
                foreach (int[,] piece in rotatedPieces)
                {
                    int[] row = new int[GridSize * GridSize + pieces.Count];
                    for (int x1 = 0; x1 < tempBoard.GetLength(0); x1++)
                    {
                        for (int y1 = 0; y1 < tempBoard.GetLength(0); y1++)
                        {
                            if (x1 + piece.GetLength(0) > tempBoard.GetLength(0)) continue;
                            if (y1 + piece.GetLength(1) > tempBoard.GetLength(1)) continue;
                            for (int x3 = 0; x3 < piece.GetLength(0); x3++)
                            {
                                for (int y3 = 0; y3 < piece.GetLength(1); y3++)
                                {
                                    tempBoard[x1 + x3, y1 + y3] = piece[x3, y3];
                                }
                            }
                            int index = 0;
                            for (int x3 = 0; x3 < tempBoard.GetLength(0); x3++)
                            {
                                for (int y3 = 0; y3 < tempBoard.GetLength(1); y3++)
                                {
                                    if (tempBoard[x3, y3] != 0)
                                    {
                                        row[index] = 1;
                                    }
                                    index++;
                                }
                            }
                            //ShowBoard(tempBoard);
                            row[GridSize * GridSize + pieces.IndexOf(origPiece)] = 1;
                            matrix.Add(row);
                            //Debug.WriteLine(string.Join("", row));
                            //Debug.WriteLine("-----");
                            tempBoard = new int[GridSize, GridSize];
                            row = new int[GridSize * GridSize + pieces.Count];
                        }

                    }
                    /*for (int x = 0; x < piece.GetLength(0); x++)
                    {
                        for (int y = 0; y < piece.GetLength(1); y++)
                        {
                            var index = x * 8 + y;
                            Debug.WriteLine(index);
                        }
                    }*/
                }
            }
            int[,] matrixArray = new int[matrix.Count, GridSize * GridSize + pieces.Count];
            int index2 = 0;
            foreach (int[] row in matrix)
            {
                for (int y = 0; y < row.Length; y++)
                {
                    matrixArray[index2, y] = row[y];
                }
                index2++;
            }

            /*for (int x1 = 0; x1 < matrix.Count; x1++)
            {
                    Debug.WriteLine(string.Join("", matrix[x1]));
                    Debug.WriteLine(string.Join("", Enumerable.Range(0, matrixArray.GetLength(1)).Select(y => matrixArray[x1, y]).ToArray()));
                    Debug.WriteLine("-----");
            }*/

            return matrixArray;
        }

        private List<int[,]> RotatePiece(int[,] origPiece)
        {
            List<int[,]> result = new List<int[,]>();
            result.Add(origPiece);
            int[,] rotated = origPiece;
            for (int i = 0; i < 4; i++)
            {
                rotated = RotateArrayClockwise(rotated);
                foreach (int[,] piece in result)
                {
                    if (piece.GetLength(0) == rotated.GetLength(0) && piece.Cast<int>().SequenceEqual(rotated.Cast<int>()))
                    {
                        goto ContinueLoop;
                    }
                }
                result.Add(rotated);
                ContinueLoop:
                string test3 = "";
            }
            /*foreach (var resultPiece in result)
            {
                PrintPiece(resultPiece);
            }*/
            return result;
        }

        private int[,] RotateArrayClockwise(int[,] src)
        {
            int width = src.GetUpperBound(0) + 1;
            int height = src.GetUpperBound(1) + 1;
            int[,] dst = new int[height, width];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    dst[height - (row + 1), col] = src[col, row];
                }
            }

            return dst;
        }

        private void PrintPiece(int[,] piece)
        {
            for (int i = 0; i < piece.GetLength(0); i++)
            {
                for (int j = 0; j < piece.GetLength(1); j++)
                {
                    Debug.Write(piece[i, j]);
                }
                Debug.WriteLine("");
            }
            Debug.WriteLine("-----");
        }

        private void ShowBoard(int[,] board)
        {
            Debug.WriteLine("------------");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Debug.Write(board[i, j] + "\t");
                }
                Debug.WriteLine("");
            }
            Debug.WriteLine("------------");
        }

        private int[,] ColorsToArray()
        {
            colors = new Dictionary<int, int>();
            int index = 1;
            int[,] board = new int[GridSize, GridSize];
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    int argb = cell.Style.BackColor.ToArgb();
                    if (argb == -1) continue;
                    if (!colors.ContainsKey(argb))
                    {
                        colors[argb] = index++;
                    }
                    board[cell.RowIndex, cell.ColumnIndex] = colors[argb];
                }
            }
            return board;
        }

        private List<int[,]> GetAllPieces(int[,] board)
        {
            List<int[,]> allPieces = new List<int[,]>();
            IEnumerable<int> flattened = Enumerable.Range(0, board.GetLength(0)).SelectMany(x => Enumerable.Range(0, board.GetLength(1)).Select(y => board[x, y]));
            List<int> distinctBoardItems = flattened.Distinct().ToList();
            distinctBoardItems.Sort();
            NDArray numArray = new NDArray(board);
            foreach (int item in distinctBoardItems)
            {
                if (item == 0) continue; // 0 - white (empty).
                int maxX = int.MinValue;
                int maxY = int.MinValue;
                int minX = int.MaxValue;
                int minY = int.MaxValue;
                for (int i = 0; i < board.GetLength(0); i++)
                    for (int j = 0; j < board.GetLength(1); j++)
                        if (board[i, j] == item)
                        {
                            if (i < minX) minX = i;
                            if (i > maxX) maxX = i;
                            if (j < minY) minY = j;
                            if (j > maxY) maxY = j;
                        }
                //var piece = Slice(board, maxX, maxY, minX, minY);
                NDArray slice = numArray[$"{minX}:{maxX + 1},{minY}:{maxY + 1}"];
                int[,] piece = slice.ToMuliDimArray<int>() as int[,];
                for (int i = 0; i < piece.GetLength(0); i++)
                {
                    for (int j = 0; j < piece.GetLength(1); j++)
                    {
                        if (piece[i, j] != item) piece[i, j] = 0;
                    }
                }
                allPieces.Add(piece);
                //PrintFigure(figure);
            }
            return allPieces;
        }

        private void PrintFigure(int[,] figure)
        {
            Debug.WriteLine("------------");
            for (int i = 0; i < figure.GetLength(0); i++)
            {
                for (int j = 0; j < figure.GetLength(1); j++)
                {
                    Debug.Write(figure[i,j] + "\t");
                }
                Debug.WriteLine("");
            }
            Debug.WriteLine("------------");
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void solvedDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            solvedDataGridView.ClearSelection();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.BackColor = colorDialog1.Color;
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = style;
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (Control.MouseButtons == MouseButtons.Left)
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.BackColor = colorDialog1.Color;
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = style;
            }
        }

        private void InitBoard(int[,] a, int size)
        {
            DataGridView[] grids = new DataGridView[2] { dataGridView1, solvedDataGridView };
            foreach (DataGridView grid in grids)
            {
                grid.ColumnCount = size;
                grid.RowCount = size;
                grid.ColumnHeadersVisible = false;
                grid.RowHeadersVisible = false;
                grid.AllowUserToResizeColumns = false;
                grid.AllowUserToResizeRows = false;
                grid.MultiSelect = false;
                foreach (DataGridViewColumn col in grid.Columns)
                {
                    col.Width = dataGridView1.Rows[0].Height;
                }
            }

            if (a == null) return;

            IEnumerable<int> flattened = Enumerable.Range(0, a.GetLength(0)).SelectMany(x => Enumerable.Range(0, a.GetLength(1)).Select(y => a[x, y]));
            List<int> distinct = flattened.Distinct().ToList();
            distinct.Sort();

            Random rnd = new Random();
            Dictionary<int, Color> colors = new Dictionary<int, Color>();
            foreach (int element in distinct)
            {
                colors[element] = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    int value = a[cell.RowIndex, cell.ColumnIndex];
                    style.BackColor = colors[value];
                    cell.Style = style;
                }
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            colorPanel.BackColor = colorDialog1.Color;
        }

        private void solveButton_Click(object sender, EventArgs e)
        {
            ClearBoard(false);
            int[,] board = ColorsToArray();
            List<int[,]> pieces = GetAllPieces(board);
            /*piecesRichTextBox.Clear();
            foreach (var piece in pieces)
            {
                for (int i = 0; i < piece.GetLength(0); i++)
                {
                    for (int j = 0; j < piece.GetLength(1); j++)
                    {
                        piecesRichTextBox.AppendText(piece[i, j] + " ");
                    }
                    piecesRichTextBox.AppendText(Environment.NewLine);
                }
                piecesRichTextBox.AppendText(Environment.NewLine);
            }*/
            //var matrix = GetIncidenceMatrix(pieces);
            incidenceMatrix = GetIncidenceMatrix(pieces);
            AlgorithmX algorithm = new AlgorithmX(incidenceMatrix.GetLength(1));
            algorithm.AddRows(incidenceMatrix);
            sols = algorithm.GetAllExactCovers();
            solutionEnumerator = sols.GetEnumerator();
            if (solutionEnumerator.MoveNext())
            {
                ShowSolution(solutionEnumerator.Current);
                /*var rows = new List<int[]>();
                //solutionEnumerator.Current.Sort();
                foreach (var rowIndex in solutionEnumerator.Current)
                {
                    rows.Add(Enumerable.Range(0, incidenceMatrix.GetLength(1)).Select(y => incidenceMatrix[rowIndex, y]).ToArray());
                }
                foreach (var row in rows)
                {
                    Debug.WriteLine(string.Join("", row));
                }
                Debug.WriteLine("-------");*/
            }
            //Debug.WriteLine("");
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (solutionEnumerator.MoveNext())
            {
                ShowSolution(solutionEnumerator.Current);
            } else
            {
                ClearBoard(false);
                solutionEnumerator = sols.GetEnumerator();
            }
        }

        private void sizeOkButton_Click(object sender, EventArgs e)
        {
            int size = int.Parse(sizeTextBox.Text);
            InitBoard(null, size);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearBoard(true);
        }

        private void ClearBoard(bool left)
        {
            DataGridView grid = left ? dataGridView1 : solvedDataGridView;
            foreach (DataGridViewRow row in grid.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.BackColor = Color.White;
                    cell.Style = style;
                }
            }
        }
    }
}
