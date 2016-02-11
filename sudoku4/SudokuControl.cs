using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sudoku4
{
    public partial class SudokuControl : UserControl
    {

        private SudokuGame game;

        public SudokuGame Game
        {
            get { return game; }
            set 
            { 
                game = value;
                this.Invalidate();
            }
        }
        

        public Color SelectedColor { get; set; }
        public Color ErrorColor { get; set; }
        public Color DefaultColor { get; set; }
        public Color LineColor { get; set; }
        public Color ThickLineColor { get; set; }

        public Point SelecedBox { get; set; }

        public SudokuControl()
        {
            this.game = null;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Opaque, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            InitializeComponent();

            this.SelectedColor = Color.AliceBlue;
            this.ErrorColor = Color.Red;
            this.DefaultColor = Color.LightGray;
            this.LineColor = Color.LightGray;
            this.ThickLineColor = Color.Bisque;
            this.SelecedBox = new Point();

            
        }

        private void SudokuControl_SizeChanged(object sender, EventArgs e)
        {
            this.Size = new Size(this.Size.Height, this.Size.Height);
        }

        private void SudokuControl_Paint(object sender, PaintEventArgs e)
        {
            Brush br1 = new SolidBrush(this.BackColor);
            e.Graphics.FillRectangle(br1, e.ClipRectangle);
            br1.Dispose();

            float step_w1 = (this.Width-1) / 9f;
            float step_h1 = (this.Height-1) / 9f;

            if (this.SelecedBox.X != 0 && this.SelecedBox.Y != 0)
            {
                Rectangle rec = new Rectangle((int)((SelecedBox.X - 1) * step_w1), (int)((SelecedBox.Y - 1) * step_h1), (int)step_w1+1, (int)step_h1+1);
                e.Graphics.FillRectangle(Brushes.LightBlue, rec);
            }

            for (int i = 0; i <= 9; i++)
            {
                Pen p = new Pen(this.LineColor);
               
                e.Graphics.DrawLine(p, 0, (int)i * step_h1, this.Width, (int)i * step_h1);
                p.Dispose();
            }
            for (int i = 0; i <= 9; i++)
            {
                Pen p = new Pen(this.LineColor);
                
                e.Graphics.DrawLine(p, (int)i * step_w1, 0, (int)i * step_w1, this.Width);
                p.Dispose();
            }

            float tlw = 3f;
            float step_w = (this.Width - tlw) / 3f;
            float step_h = (this.Height - tlw) / 3f;

            for (int i = 0; i <= 3; i++)
            {
                Pen p = new Pen(this.ThickLineColor, tlw);
                
                e.Graphics.DrawLine(p, 0, (int)tlw/2f+(int)i * step_h, this.Width, (int)tlw/2f+(int)i * step_h);
                p.Dispose();
            }
            for (int i = 0; i <= 3; i++)
            {
                Pen p = new Pen(this.ThickLineColor, tlw);
              
                e.Graphics.DrawLine(p, (int)tlw / 2f + (int)i * step_w, 0, (int)tlw / 2f + (int)i * step_w,  this.Width);
                p.Dispose();
            }

            float scale =0.5f* this.Width / 9f;

            if (this.game != null)
            {
                for (int i = 0; i < game.Cells.Count; i++)
                {
                    SudokuCell cell = game.Cells[i];
                    if (cell != null)
                    {
                        int vl = cell.value;
                        
                        Font ff = new System.Drawing.Font(this.Font.FontFamily, scale, this.Font.Style);

                        SizeF sz = e.Graphics.MeasureString(vl.ToString(), ff);

                        int px = (int)((cell.Column-1) * step_w1 + step_w1 / 2f - sz.Width / 2f);
                        int py = (int)((cell.Row-1) * step_h1 + step_h1/2f - sz.Height /2f);
                        Point ppp = new Point(px, py);
                        Brush br7 = new SolidBrush(this.ForeColor);


                        e.Graphics.DrawString(vl.ToString(), ff, br7, ppp);
                        br7.Dispose();

                    }
                }
            }
        }

        private void SudokuControl_MouseClick(object sender, MouseEventArgs e)
        {
            float step_w = this.Width / 9f;
            float step_h = this.Height / 9f;

            int row = (int)(e.Y / step_h) + 1;
            int col = (int)(e.X / step_w) + 1;
            this.SelecedBox = new Point(col, row);
            this.Invalidate();
            this.ParentForm.Text = "("+ col.ToString() +" , "+ row.ToString() + ")";
        }

        private void SudokuControl_KeyDown(object sender, KeyEventArgs e)
        {
            
            int num = 0;

            if (this.SelecedBox.X != 0 && this.SelecedBox.Y != 0)
            {
                
                    switch (e.KeyCode)
	                {
                        case Keys.D1:
                            num = 1;
                            break;
		                case Keys.D2:
                            num = 2;
                            break;
		                case Keys.D3:
                            num = 3;
                            break;
		                case Keys.D4:
                            num = 4;
                            break;
		                case Keys.D5:
                            num = 5;
                            break;
		                case Keys.D6:
                            num = 6;
                            break;
		                case Keys.D7:
                            num = 7;
                            break;
		                case Keys.D8:
                            num = 8;
                            break;
		                case Keys.D9:
                            num = 9;
                            break;
                        case Keys.D0:
                            num = 0;
                            break;
                        case Keys.NumPad1:
                            num = 1;
                            break;
                        case Keys.NumPad2:
                            num = 2;
                            break;
                        case Keys.NumPad3:
                            num = 3;
                            break;
                        case Keys.NumPad4:
                            num = 4;
                            break;
                        case Keys.NumPad5:
                            num = 5;
                            break;
                        case Keys.NumPad6:
                            num = 6;
                            break;
                        case Keys.NumPad7:
                            num = 7;
                            break;
                        case Keys.NumPad8:
                            num = 8;
                            break;
                        case Keys.NumPad9:
                            num = 9;
                            break;
                        case Keys.NumPad0:
                            num = 0;
                            break;
	                }

                    if (game != null && num > 0 && SelecedBox.X > 0 && SelecedBox.Y > 0) 
                    {
                        game.SetValue(SelecedBox.X, SelecedBox.Y, num);
                        this.Invalidate(true);
                    }
                                
            }
        }

    }
}
