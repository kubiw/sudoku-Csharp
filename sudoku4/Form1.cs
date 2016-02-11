using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sudoku4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (this.sudokuControl1.Game != null)
            {
                bool val = this.sudokuControl1.Game.Validate();
                if (val == false)
                {
                    
                    MessageBox.Show("Invalid!");
                }
                else
                {
                    MessageBox.Show("valid");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sudokuControl1.Game = new SudokuGame();

            int m = 0;
            Random rand = new Random();

            while (m < 20)
            {

                int c = rand.Next(1, 10);
                int r = rand.Next(1, 10);
                int v = rand.Next(1, 10);

                sudokuControl1.Game.SetValue(c, r, v);

                bool val = this.sudokuControl1.Game.Validate();
                while (val == false)
                {
                    sudokuControl1.Game.SetValue(c, r, rand.Next(1, 10));
                    val = this.sudokuControl1.Game.Validate();
                    
                }
                m++;
            }
            
        }
    }
}

