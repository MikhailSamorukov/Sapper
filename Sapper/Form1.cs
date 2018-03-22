using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sapper
{
    public partial class Form1 : Form
    {
        private int minesCount;
        private Game game;
        private Option options;
        public static int k;
        public Form1()
        {
            InitializeComponent();
            options = new Option();
            minesCount = options.GetMinesCount;
            options.Save += Options_Save;
            CenterToParent();
        }

        private void Options_Save(object sender, MinesCountEventArgs e)
        {
            minesCount = e.value;
            options.Hide();
            новаяИграToolStripMenuItem_Click(sender,e);
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            options.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CenterToScreen();
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Enabled = true;
            panel1.Height = 50 * k;
            panel1.Width = 50 * k;
            Height = panel1.Height + 80;
            Width = panel1.Width + 40;
            CenterToParent();
            game = new Game(panel1, minesCount, label1);
            game.Start();
            label1.Text = "Кол-во доступных флажков:" + options.GetMinesCount.ToString();
        }
    }
}
