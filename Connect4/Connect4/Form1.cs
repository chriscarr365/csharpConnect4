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

        /// Acknowledgements
        /// chips.wav: http://soundbible.com/2204-Poker-Chips.html (Attribution 3.0 license)
        /// bensound-relaxing.wav: https://www.bensound.com/royalty-free-music/track/relaxing
        /// chip.png & logo.png adapted from: https://icons8.it/icon/68517/%C5%BCeton
        /// 
        RoundButton[,] btn = new RoundButton[7, 6];
        Button background = new Button();         
        System.Media.SoundPlayer ambient = new System.Media.SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + "\\bensound-relaxing.wav");


        int turnCount = 1;

        public connect4Board()
        {
            InitializeComponent();

            
            //Creates 7x6 grid of white named round buttons & adds click event handling to each
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
           
             
        Color playerColor = new Color();
        int player1Score = 0;
        int player2Score = 0;
    
        void btnEvent_Click(object sender, EventArgs e)
        {
            //starts timer on first chip placed
            if(turnCount == 1)
            {
                tmrChipFall.Start();
            }
            //checks which chips turn it is
           if (turnCount%2 == 0)
            {
                playerColor = Color.Blue;
            }
            else
            {
                playerColor = Color.Red;
            }

            Button clickedButton = (Button)sender;
            //splits button name to read location in array
            string[] coordinates = clickedButton.Name.Split(new Char[] { ' ' });
            int x = Convert.ToInt32(coordinates[0]);
            int y = Convert.ToInt32(coordinates[1]);
            int yDropped = -1;
 
            //finds lowest white button in column, if no space left, show error message
            for (int i = 5; i>=0; i--)
            {               
                if (btn[x, i].BackColor == Color.White)
                {
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
            

            //animation of chip dropping dowm the board
            for (int i = 0; i <= yDropped; i++)
            {
                btn[x, i].BackColor = playerColor;
                 ////////////////////////////////////////////////lines taken from https://social.msdn.microsoft.com/Forums/windows/en-US/b9a82989-2cd3-46d4-854f-f6ddfd8df294/how-to-sleepdelay-within-a-quotforquot-loop-in-c-?forum=winforms
                Application.DoEvents();
                System.Threading.Thread.Sleep(50);
                /////////////////////////////////////////////////
                btn[x, i].BackColor = Color.White;
                if (i == yDropped)
                {
                    sound.Play();
                    btn[x, i].BackColor = playerColor;
                    btn[x, i].BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\chip.png");
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
                            winMessage();                            
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
                                winMessage();
                            }
                        }
                        else if (i == 3)
                        {
                            winMessage();
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
                                winMessage();
                            }
                        }
                        else if (i == 3)
                        {
                            winMessage();
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
                                winMessage();
                            }
                        }
                        else if (i == 3)
                        {
                            winMessage();
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
                                winMessage();
                            }
                        }
                        else if (i == 3)
                        {
                            winMessage();
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
                                winMessage();
                            }
                        }
                        else if (i == 3)
                        {
                            winMessage();
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
                                winMessage();
                            }
                        }
                        else if (i == 3)
                        {
                            winMessage();
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

            ambient.Play();
        }

        int timeTaken = 0;

        //ticks every second and displays time on form
        private void tmrChipFall_Tick(object sender, EventArgs e)
        {
            timeTaken++;
            lblTimer.Text = timeTaken.ToString();
        }

        //empties board through menu strip
        private void reloadBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turnCount = 1;
            resetBoard();
        }

        //displays rules
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            button1.Visible = true;
        }

        //hides rules
        private void btnBack_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            button1.Visible = false;
        }

        //closes application
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ambient.Stop();
            Application.Exit();
        }

        //adds 1 to counter of the player how won & displays on form
        private void addScore()
        {
            if (turnCount % 2 == 0)
            {
                player2Score++;
                lblPlayer2Score.Text = player2Score.ToString();
            }
            else
            {
                player1Score++;
                lblPlayer1Score.Text = player1Score.ToString();
            }
            turnCount = 0;
        }

        //runs through array of buttons making all of them white
        private void resetBoard()
        {
            for (int j = 0; j < btn.GetLength(0); j++)
            {
                for (int k = 0; k < btn.GetLength(1); k++)
                {
                    btn[j, k].BackColor = Color.White;
                    btn[j, k].BackgroundImage = null;
                }
            }
            lblTimer.Text = "0";
            timeTaken = 0;
        }

        //displays who won and asks to restart. yes will restart, no will close application
        private void winMessage()
        {
            tmrChipFall.Stop();
            DialogResult result = MessageBox.Show("4 in a row, " + playerColor.Name + " wins. Restart?", "congratulations", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                
                resetBoard();
                addScore();
            }
            else
            {
                ambient.Stop();
                Application.Exit();
            }
        }

        //displays about message box
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult about = MessageBox.Show("Connect 4. Made by Christian Hegarty, Christopher Carr", "Connect 4", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            textBox2.Visible = false;
            pictureBox1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ambient.Stop();
            Application.Exit();
        }
    }

    //creates new object inheriting button. same as button but circular
    /// 
    /// //////////////////////////////////////////////////////////////////////////////////////////////
    /// Code used  has been taken from: https://stackoverflow.com/questions/3708113/round-shaped-buttons
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
    ////////////////////////////////////////////////////////////////////////////////////////////////////
}
