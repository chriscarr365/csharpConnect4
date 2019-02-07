using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public partial class connect4Board : Form
    {
        Button[,] btn = new Button[7, 6];
        Button background = new Button();

        public connect4Board()
        {
            InitializeComponent();

            

            for (int x = 0; x<btn.GetLength(0); x++)
            {
                for (int y = 0; y<btn.GetLength(1); y++)
                {
                    btn[x, y] = new Button();
                    btn[x, y].SetBounds(10 + 55 * x, 10 + 55 * y, 45, 45);
                    btn[x, y].BackColor = Color.White;
                    btn[x, y].Click += new EventHandler(this.btnEvent_Click);
                    btn[x, y].Name = x + " " + y;
     //               btn[x, y].MouseEnter += new EventHandler(this.btnEvent_MouseEnter);
     //               btn[x, y].MouseLeave += new EventHandler(this.btnEvent_MouseLeave);
                    Controls.Add(btn[x, y]);
                }
            }
            background.BackColor = Color.Blue;
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

            for (int i = 5; i>=0; i--)
            {
                if (btn[x, i].BackColor == Color.White)
                {
                    btn[x, i].BackColor = playerColor;
                    break;
                }
                else if (i == 0)
                {
                    MessageBox.Show("All of this column is full, try a different one");
                }
            }

            //add out of boubds exception handling
            if (btn[x, y+1].BackColor == Color.Red)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (btn[x, y + i].BackColor != Color.Red)
                    {
                        break;
                    }
                    else if (i == 3)
                    {
                        MessageBox.Show("4 in a row");
                    }
                }
            }
            //if (btn[x+1, y+1].BackColor == Color.Red)
            //{

            //}
            //if (btn[x+1, y].BackColor == Color.Red)
            //{

            //}

            //if (btn[x+1, y-1].BackColor == Color.Red)
            //{

            //}





            turnCount++;

            
           
            //Possibly unnecessary
            //if (btn[x, y-1].BackColor == Color.Red)
            //{

            //}
            //if (btn[x-1, y-1].BackColor == Color.Red)
            //{

            //}
            //if (btn[x-1, y].BackColor == Color.Red)
            //{

            //}
            //if (btn[x-1, y+1].BackColor == Color.Red)
            //{

            //}
            
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
}
