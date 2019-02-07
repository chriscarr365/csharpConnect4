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
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Application.Run(new connect4Board());
            this.Hide();
            connect4Board sp = new connect4Board();
            sp.ShowDialog();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Application.Run(new connect4Board());
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
