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


    public partial class aiboard : Form
    {

        Button[,] btn = new Button[7, 6];
        Button background = new Button();
        public aiboard()
        {
            InitializeComponent();



            for (int x = 0; x < btn.GetLength(0); x++)
            {
                for (int y = 0; y < btn.GetLength(1); y++)
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


        int x;
        int yDropped = -1;
        void btnEvent_Click(object sender, EventArgs e)
        {

            //int x = 0;
            if (turnCount % 2 == 0)
            {
                playerColor = Color.Blue;
            }
            else
            {
                playerColor = Color.Red;
            }

            if (playerColor == Color.Red)
            {
                Button clickedButton = (Button)sender;

                string[] coordinates = clickedButton.Name.Split(new Char[] { ' ' });
                int x = Convert.ToInt32(coordinates[0]);
                int y = Convert.ToInt32(coordinates[1]);
                yDropped = -1;
                for (int i = 5; i >= 0; i--)
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
            }
            if (playerColor == Color.Blue) //for ai
            {
                bool dropped = false;
                Button clickedButton = (Button)sender;
                while (dropped != true)
                {

                    Random rnd = new Random();
                    int x = rnd.Next(1, 7);
                    yDropped = -1;
                    for (int i = 5; i >= 0; i--)
                    {
                        if (btn[x, i].BackColor == Color.White)
                        {
                            btn[x, i].BackColor = playerColor;

                            yDropped = i;
                            dropped = true;
                            break;
                        }
                        else if (i == 0)
                        {
                            turnCount--;
                        }
                    }
                }
            }
            //add out of boubds exception handling

            try
            {
                //checks if there is 4 in a row down from where the object was placed
                //WORKING
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
            catch (IndexOutOfRangeException)
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
                //WORKING INCONSISTENTLY
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



            //likely unnecessary
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


        private void aiboard_Load(object sender, EventArgs e)
        {

        }

    }
}