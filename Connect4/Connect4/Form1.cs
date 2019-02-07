using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public partial class connect4Board : Form
    {
        RoundButton[,] btn = new RoundButton[7, 6];
        Button background = new Button();

        public connect4Board()
        {
            InitializeComponent();

            

            for (int x = 0; x<btn.GetLength(0); x++)
            {
                for (int y = 0; y<btn.GetLength(1); y++)
                {
                    btn[x, y] = new RoundButton();
                    btn[x, y].SetBounds(10 + 55 * x, 10 + 55 * y, 45, 45);
                    btn[x, y].BackColor = Color.White;
                    btn[x, y].FlatStyle = FlatStyle.Flat;
                    btn[x, y].FlatAppearance.BorderSize = 0;
                    btn[x, y].Click += new EventHandler(this.btnEvent_Click);
                    btn[x, y].Name = x + " " + y;
     //               btn[x, y].MouseEnter += new EventHandler(this.btnEvent_MouseEnter);
     //               btn[x, y].MouseLeave += new EventHandler(this.btnEvent_MouseLeave);
                    Controls.Add(btn[x, y]);
                }
            }
            background.BackColor = Color.DodgerBlue;
            background.SetBounds(0, 0, 400, 350);
            Controls.Add(background);
            
        }

        int turnCount = 1;
        Color playerColor = new Color();
        
        
       

        void btnEvent_Click(object sender, EventArgs e)
        {
           if (turnCount%2 == 0)
            {
                playerColor = Color.Blue;
            }
            else
            {
                playerColor = Color.Red;
            }
            Button clickedButton = (Button)sender;

            string[] coordinates = clickedButton.Name.Split(new Char[] { ' ' });
            int x = Convert.ToInt32(coordinates[0]);
            int y = Convert.ToInt32(coordinates[1]);
            int yDropped = -1;
            for (int i = 5; i>=0; i--)
            {
                if (btn[x, i].BackColor == Color.White)
                {
                    btn[x, i].BackColor = playerColor;

                    yDropped = i;
                    break;
                }
                else if (i == 0)
                {
                    turnCount--;
                    MessageBox.Show("All of this column is full, try a different one");
                }
            }

            try
            {
                //checks if there is 4 in a row down from where the object was placed
                if (btn[x, yDropped + 1].BackColor == playerColor)
                {
                    for (int i = 0; i < 4; i++)
                {
                        if (btn[x, yDropped + i].BackColor != playerColor)
                        {
                            break;
                        }
                        else if (i == 3)
                        {
                            MessageBox.Show("4 in a row, " + playerColor.Name + " wins");
                        }
                    }
                }
            }
            catch(IndexOutOfRangeException)
            {

            }


            try
            {
                //checks if there is 4 in a row down and to the right from where the object was placed
                if (btn[x + 1, yDropped + 1].BackColor == playerColor)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (btn[x + i, yDropped + i].BackColor != playerColor)
                        {
                            break;
                        }
                        else if (i == 3)
                        {
                            MessageBox.Show("4 in a row, " + playerColor.Name + " wins");
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {

            }

            try
            {
                //checks if there is 4 in a row to the right of where the object was placed
                if (btn[x + 1, yDropped].BackColor == playerColor)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (btn[x + i, yDropped].BackColor != playerColor)
                        {
                            break;
                        }
                        else if (i == 3)
                        {
                            MessageBox.Show("4 in a row, " + playerColor.Name + " wins");
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {

            }

            try
            {
                //checks if there is 4 in a row down and to the left of where the object was placed
                if (btn[x - 1, yDropped + 1].BackColor == playerColor)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (btn[x - i, yDropped + i].BackColor != playerColor)
                        {
                            break;
                        }
                        else if (i == 3)
                        {
                            MessageBox.Show("4 in a row, " + playerColor.Name + " wins");
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {

            }

            try
            {
                //checks if there is 4 in a row to the left of where the object was placed
                if (btn[x - 1, yDropped].BackColor == playerColor)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (btn[x - i, yDropped].BackColor != playerColor)
                        {
                            break;
                        }
                        else if (i == 3)
                        {
                            MessageBox.Show("4 in a row, " + playerColor.Name + " wins");
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {

            }



            turnCount++;           
        }

        //void btnEvent_MouseEnter(object sender, EventArgs e)
        //{
        //    Console.WriteLine(((Button)sender).Text);
        //}

        //void btnEvent_MouseLeave(object sender, EventArgs e)
        //{
        //    Console.WriteLine(((Button)sender).Text);
        //}

        private void connect4Board_Load(object sender, EventArgs e)
        {
            
        }
    }

    public class RoundButton : Button
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(grPath);
            base.OnPaint(e);
        }
    }
}
