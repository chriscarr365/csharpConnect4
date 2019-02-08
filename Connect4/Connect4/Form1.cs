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
                    btn[x, y].SetBounds(10 + 55 * x, 40 + 55 * y, 45, 45);
                    btn[x, y].BackColor = Color.White;
                    btn[x, y].FlatStyle = FlatStyle.Flat;
                    btn[x, y].FlatAppearance.BorderSize = 0;
                    btn[x, y].Click += new EventHandler(this.btnEvent_Click);
                    btn[x, y].Name = x + " " + y;
                    Controls.Add(btn[x, y]);
                }
            }
            background.BackColor = Color.DodgerBlue;
            background.SetBounds(0, 30, 400, 350);
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
                 //   btn[x, i].BackColor = playerColor;

                    yDropped = i;
                    break;
                }
                else if (i == 0)
                {
                    turnCount--;
                    MessageBox.Show("All of this column is full, try a different one");
                }
            }

            /////////////////////////////////////////////////lines from https://stackoverflow.com/questions/4125698/how-to-play-wav-audio-file-from-resources
            System.IO.Stream str = Properties.Resources.chips;
            System.Media.SoundPlayer sound = new System.Media.SoundPlayer(str);
            //////////////////////////////////////////////////////////////////////
            
            for (int i = 0; i <= yDropped; i++)
            {
  //              tmrChipFall.Start();
                btn[x, i].BackColor = playerColor;
                
  //              btn[x, i].BackColor = Color.White;
                 ////////////////////////////////////////////////lines taken from https://social.msdn.microsoft.com/Forums/windows/en-US/b9a82989-2cd3-46d4-854f-f6ddfd8df294/how-to-sleepdelay-within-a-quotforquot-loop-in-c-?forum=winforms
                Application.DoEvents();
                System.Threading.Thread.Sleep(50);
                /////////////////////////////////////////////////
                btn[x, i].BackColor = Color.White;
                if (i == yDropped)
                {
                    sound.Play();
                    btn[x, i].BackColor = playerColor;
                    break;
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
                            DialogResult result = MessageBox.Show("4 in a row, " + playerColor.Name + " wins. Restart?", "congratulations",  MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                for (int j = 0; j < btn.GetLength(0); j++)
                                {
                                    for (int k = 0; k < btn.GetLength(1); k++)
                                    {
                                        btn[j, k].BackColor = Color.White;
                                    }
                                }
                            }
                            else
                            {
                                Application.Exit();
                            }

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
                        else if (i == 2 && x-1>=0 && yDropped-1 >= 0)
                        {
                            if (btn[x - 1, yDropped - 1].BackColor == playerColor)
                            {
                                DialogResult result = MessageBox.Show("4 in a row, " + playerColor.Name + " wins. Restart?", "congratulations", MessageBoxButtons.YesNo);
                                if (result == DialogResult.Yes)
                                {
                                    for (int j = 0; j < btn.GetLength(0); j++)
                                    {
                                        for (int k = 0; k < btn.GetLength(1); k++)
                                        {
                                            btn[j, k].BackColor = Color.White;
                                        }
                                    }
                                }
                                else
                                {
                                    Application.Exit();
                                }
                            }
                        }
                        else if (i == 3)
                        {
                            DialogResult result = MessageBox.Show("4 in a row, " + playerColor.Name + " wins. Restart?", "congratulations", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                for (int j = 0; j < btn.GetLength(0); j++)
                                {
                                    for (int k = 0; k < btn.GetLength(1); k++)
                                    {
                                        btn[j, k].BackColor = Color.White;
                                    }
                                }
                            }
                            else
                            {
                                Application.Exit();
                            }
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
                        else if (i == 2 && x - 1 >= 0)
                        {
                            if (btn[x - 1, yDropped].BackColor == playerColor)
                            {
                                DialogResult result = MessageBox.Show("4 in a row, " + playerColor.Name + " wins. Restart?", "congratulations", MessageBoxButtons.YesNo);
                                if (result == DialogResult.Yes)
                                {
                                    for (int j = 0; j < btn.GetLength(0); j++)
                                    {
                                        for (int k = 0; k < btn.GetLength(1); k++)
                                        {
                                            btn[j, k].BackColor = Color.White;
                                        }
                                    }
                                }
                                else
                                {
                                    Application.Exit();
                                }
                            }
                        }
                        else if (i == 3)
                        {
                            DialogResult result = MessageBox.Show("4 in a row, " + playerColor.Name + " wins. Restart?", "congratulations", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                for (int j = 0; j < btn.GetLength(0); j++)
                                {
                                    for (int k = 0; k < btn.GetLength(1); k++)
                                    {
                                        btn[j, k].BackColor = Color.White;
                                    }
                                }
                            }
                            else
                            {
                                Application.Exit();
                            }
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
                        else if (i == 2 && x + 1 <= 6 )
                        {
                            if (btn[x + 1, yDropped].BackColor == playerColor)
                            {
                                DialogResult result = MessageBox.Show("4 in a row, " + playerColor.Name + " wins. Restart?", "congratulations", MessageBoxButtons.YesNo);
                                if (result == DialogResult.Yes)
                                {
                                    for (int j = 0; j < btn.GetLength(0); j++)
                                    {
                                        for (int k = 0; k < btn.GetLength(1); k++)
                                        {
                                            btn[j, k].BackColor = Color.White;
                                        }
                                    }
                                }
                                else
                                {
                                    Application.Exit();
                                }
                            }
                        }
                        else if (i == 3)
                        {
                            DialogResult result = MessageBox.Show("4 in a row, " + playerColor.Name + " wins. Restart?", "congratulations", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                for (int j = 0; j < btn.GetLength(0); j++)
                                {
                                    for (int k = 0; k < btn.GetLength(1); k++)
                                    {
                                        btn[j, k].BackColor = Color.White;
                                    }
                                }
                            }
                            else
                            {
                                Application.Exit();
                            }
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
                        else if (i == 2 && x + 1 <= 6 && yDropped - 1 >= 0)
                        {
                            if (btn[x + 1, yDropped - 1].BackColor == playerColor)
                            {
                                DialogResult result = MessageBox.Show("4 in a row, " + playerColor.Name + " wins. Restart?", "congratulations", MessageBoxButtons.YesNo);
                                if (result == DialogResult.Yes)
                                {
                                    for (int j = 0; j < btn.GetLength(0); j++)
                                    {
                                        for (int k = 0; k < btn.GetLength(1); k++)
                                        {
                                            btn[j, k].BackColor = Color.White;
                                        }
                                    }
                                }
                                else
                                {
                                    Application.Exit();
                                }
                            }
                        }
                        else if (i == 3)
                        {
                            DialogResult result = MessageBox.Show("4 in a row, " + playerColor.Name + " wins. Restart?", "congratulations", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                for (int j = 0; j < btn.GetLength(0); j++)
                                {
                                    for (int k = 0; k < btn.GetLength(1); k++)
                                    {
                                        btn[j, k].BackColor = Color.White;
                                    }
                                }
                            }
                            else
                            {
                                Application.Exit();
                            }
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {

            }

            try
            {
                //checks if there is 4 in a row up and to the left of where the object was placed
                if (btn[x - 1, yDropped-1].BackColor == playerColor)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (btn[x - i, yDropped-i].BackColor != playerColor)
                        {
                            break;
                        }
                        else if (i == 2 && x + 1 <= 6 && yDropped + 1 <= 5)
                        {
                            if (btn[x + 1, yDropped + 1].BackColor == playerColor)
                            {
                                DialogResult result = MessageBox.Show("4 in a row, " + playerColor.Name + " wins. Restart?", "congratulations", MessageBoxButtons.YesNo);
                                if (result == DialogResult.Yes)
                                {
                                    for (int j = 0; j < btn.GetLength(0); j++)
                                    {
                                        for (int k = 0; k < btn.GetLength(1); k++)
                                        {
                                            btn[j, k].BackColor = Color.White;
                                        }
                                    }
                                }
                                else
                                {
                                    Application.Exit();
                                }
                            }
                        }
                        else if (i == 3)
                        {
                            DialogResult result = MessageBox.Show("4 in a row, " + playerColor.Name + " wins. Restart?", "congratulations", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                for (int j = 0; j < btn.GetLength(0); j++)
                                {
                                    for (int k = 0; k < btn.GetLength(1); k++)
                                    {
                                        btn[j, k].BackColor = Color.White;
                                    }
                                }
                            }
                            else
                            {
                                Application.Exit();
                            }
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {

            }

            try
            {
                //checks if there is 4 in a row up and to the right of where the object was placed
                if (btn[x + 1, yDropped - 1].BackColor == playerColor)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (btn[x + i, yDropped - i].BackColor != playerColor)
                        {
                            break;
                        }
                        else if (i == 2 && x - 1 >= 0 && yDropped + 1 <= 5)
                        {
                            if (btn[x - 1, yDropped + 1].BackColor == playerColor)
                            {
                                DialogResult result = MessageBox.Show("4 in a row, " + playerColor.Name + " wins. Restart?", "congratulations", MessageBoxButtons.YesNo);
                                if (result == DialogResult.Yes)
                                {
                                    for (int j = 0; j < btn.GetLength(0); j++)
                                    {
                                        for (int k = 0; k < btn.GetLength(1); k++)
                                        {
                                            btn[j, k].BackColor = Color.White;
                                        }
                                    }
                                }
                                else
                                {
                                    Application.Exit();
                                }
                            }
                        }
                        else if (i == 3)
                        {
                            DialogResult result = MessageBox.Show("4 in a row, " + playerColor.Name + " wins. Restart?", "congratulations", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                for (int j = 0; j < btn.GetLength(0); j++)
                                {
                                    for (int k = 0; k < btn.GetLength(1); k++)
                                    {
                                        btn[j, k].BackColor = Color.White;
                                    }
                                }
                            }
                            else
                            {
                                Application.Exit();
                            }
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {

            }


            turnCount++;           
        }


        private void connect4Board_Load(object sender, EventArgs e)
        {
            
        }

        private void tmrChipFall_Tick(object sender, EventArgs e)
        {

        }

        private void reloadBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < btn.GetLength(0); x++)
            {
                for (int y = 0; y < btn.GetLength(1); y++)
                {
                    btn[x, y].BackColor = Color.White;
                }
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
