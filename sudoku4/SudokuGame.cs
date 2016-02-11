using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sudoku4
{
    public class SudokuGame
    {
        private List<SudokuLine> lines;

        public List<SudokuLine> Lines
        {
            get { return lines; }
            set { lines = value; }
        }

        private List<SudokuColumn> columns;

        public List<SudokuColumn> Columns
        {
            get { return columns; }
            set { columns = value; }
        }

        private List<SudokuSquare> squares;

        public List<SudokuSquare> Squares
        {
            get { return squares; }
            set { squares = value; }
        }

        private List<SudokuCell> cells;

        public List<SudokuCell> Cells
        {
            get { return cells; }
            set { cells = value; }
        }
                      
        public SudokuGame()
        {
            lines = new List<SudokuLine>(9);
            columns = new List<SudokuColumn>(9);
            squares = new List<SudokuSquare>(9);
            cells = new List<SudokuCell>(81);
            InitializeGame();
        }

        void InitializeGame()
        {
            for (int i = 0; i < 9; i++)
            {
                SudokuLine sdl = new SudokuLine();
                sdl.LineNumber = i + 1;
                lines.Add(sdl);
            }
            for (int i = 0; i < 9; i++)
            {
                SudokuColumn sdc = new SudokuColumn();
                sdc.ColumnNumber = i + 1;
                columns.Add(sdc);
            }
            for (int i = 0; i < 9; i++)
            {
                SudokuSquare sds = new SudokuSquare();
                sds.SquareNumber = i + 1;
                squares.Add(sds);
            }
            for (int i = 0; i < 81; i++)
            {
                cells.Add(null);
            }
        }

        public void SetValue(int column, int row, int value)
        {
            int index = GetCellIndexFromPosition(column, row);
            cells[index] = new SudokuCell(new System.Drawing.Point(column, row), value);
            lines[row - 1].Cells[column - 1] = cells[index];
            columns[column - 1].Cells[row - 1] = cells[index];
            int sq_i = GetSquareIndexFromCellIndex(index);
            int internal_i = GetInternalSquareIndexFromCellIndex(index);
            squares[sq_i].Cells[internal_i] = cells[index];
        }

        int GetCellIndexFromPosition(int column, int row)
        {
            return (row - 1) * 9 + column - 1;
        }

        System.Drawing.Point GetPositionFromCellIndex(int cellIndex)
        {
            System.Drawing.Point pt = new System.Drawing.Point();
            pt.X = cellIndex % 9 + 1;
            pt.Y = cellIndex / 9 + 1;
            return pt;
        }

        int GetSquareIndexFromCellIndex(int cellIndex)
        {
            System.Drawing.Point pt = GetPositionFromCellIndex(cellIndex);
            if (pt.X <= 3) // square 0.3 or 6
            {
                if (pt.Y <= 3) // square 0.1 or 2
                    return 0;
                if (pt.Y > 3 && pt.Y <= 6) // square 3.4 or 5
                    return 3;
                if (pt.Y > 6 && pt.Y <= 10) // square 6.7 or 8
                    return 6;
            }
            if (pt.X > 3 && pt.X <= 6) // square 1.4 or 7
            {
                if (pt.Y <= 3) // square 0.1 or 2
                    return 1;
                if (pt.Y > 3 && pt.Y <= 6) // square 3.4 or 5
                    return 4;
                if (pt.Y > 6 && pt.Y <= 10) // square 6.7 or 8
                    return 7;
            }
            if (pt.X > 6 && pt.X < 10) // square 2.5 or 8
            {
                if (pt.Y <= 3) // square 0.1 or 2
                    return 2;
                if (pt.Y > 3 && pt.Y <= 6) // square 3.4 or 5
                    return 5;
                if (pt.Y > 6 && pt.Y < 10) // square 6.7 or 8
                    return 8;
            }
            return 0;
        }

        int GetInternalSquareIndexFromCellIndex(int cellIndex)
        {
            System.Drawing.Point pt = GetPositionFromCellIndex(cellIndex);
            int p_x = (pt.X-1) % 3;
            int p_y = (pt.Y-1) % 3;
            return p_y * 3 + p_x;
        }
 
        public bool Validate()
        {
            bool val = true;
            foreach (SudokuCellGroup group in lines)
                val &= group.Validate();

            foreach (SudokuCellGroup group in columns)
                val &= group.Validate();

            foreach (SudokuCellGroup group in squares)
                val &= group.Validate();

            return val;
        }
    }
}
