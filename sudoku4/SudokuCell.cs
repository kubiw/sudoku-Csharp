﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace sudoku4
{
    public class SudokuCell
    {
        #region Constructors

        public SudokuCell()
        {
            position = new Point(1, 1);
            _value = 1;
        }

        public SudokuCell(Point position)
            : this()
        {
            this.position = position;
        }

        public SudokuCell(Point position, int value)
            : this(position)
        {
            _value = value;
        }

        public SudokuCell(int value)
            : this(new Point(1, 1), value)
        {
        }

        
        
        #endregion
                
        private Point position;

        public Point Position
        {
            get { return position; }
            set 
            {
                if(value.X >= 1 && value.X <= 9)
                    if (value.Y >= 1 && value.Y <= 9)
                        position = value; 
            }
        }

        public int Row
        {
            get { return this.position.Y; }
        }
        
        public int Column
        {
            get { return this.position.X; }
        }
        
        private int _value;

        public int value
        {
            get { return _value; }
            set 
            {
                if (value > 0 && value < 10)
                    _value = value;
                else
                    value = 1;
            }
        }
        

    }
}
