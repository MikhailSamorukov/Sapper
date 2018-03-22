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
    public delegate void MinesCountChange(object sender, MinesCountEventArgs e);

    public partial class Option : Form
    {
        public void Rezhim()
        {
           /* for (int i = groupBox1.Controls.Count-1; i >0; i--)
            {
                groupBox1.Controls[i].Tag = i.ToString();
            }
            for (int i = 0; i < groupBox1.Controls.Count; i++)
            {
                if (((RadioButton)groupBox1.Controls[i]).Checked)
                {
                    Form1.k = Convert.ToInt32(((RadioButton)groupBox1.Controls[i]).Tag)+8;
                    Form1.h = Convert.ToInt32(((RadioButton)groupBox1.Controls[i]).Tag)+;
                }
            }*/
            for (int i = 0; i < groupBox1.Controls.Count; i++)
            {
                if (((RadioButton) groupBox1.Controls[i]).Checked)
                {
                    Form1.k = i * 2 + 8;
                }
            }
            /*if (radioButton1.Checked){Form1.k = 8;Form1.h = 8;}
            if (radioButton2.Checked){Form1.k = 10;Form1.h = 10;}
            if (radioButton3.Checked){Form1.k = 12;Form1.h = 12;}
            if (radioButton4.Checked){Form1.k = 14;Form1.h = 14;}
            if (radioButton5.Checked){Form1.k = 16;Form1.h = 16;}*/
        }
        public int GetMinesCount => Convert.ToInt32(maskedTextBox1.Text);
        public event MinesCountChange Save;
        public Option()
        {
            InitializeComponent();
            Rezhim();
            CenterToParent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rezhim();
            if (Convert.ToInt32(maskedTextBox1.Text) < Form1.k * 7)
                Save(sender, new MinesCountEventArgs(Convert.ToInt32(maskedTextBox1.Text)));
            else
                MessageBox.Show("Быстро поставил меньше мин, пес! Я знаю кто ты, не трогай опции!!! Играй на 10 минах, ты не на столько умен!!!");
        }
    }
}
