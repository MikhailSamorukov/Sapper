using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Sapper.Cells
{
    public class AbstractCell
    {
        public delegate void OnClick(object sender, FlagEventArgs e);
        public event OnClick OnFlagChange;
        public event OnClick OnActive;
        public bool ActiveStatus { get; set; }
        public bool IsFlag { get; set; } = true;
        public int X => PosX / 50;
        public int Y => PosY / 50;
        public PictureBox Image { get; set; } = new PictureBox() { Image = Extentions.GetImage("DeactiveCell.png") };
        public int PosX { get; set; }
        public int PosY { get; set; }
        protected List<AbstractCell> Neighbors { get; set; }

        public AbstractCell(int posX, int posY)
        {
            PosX = posX * 50;
            PosY = posY * 50;
            Image.Location = new Point(PosX, PosY);
            Image.Size = new Size(50, 50);
            Image.SizeMode = PictureBoxSizeMode.StretchImage;
            Image.MouseClick += Activate;
        }

        public virtual void Activate(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && IsFlag)
            {
                ActiveStatus = true;
            }
            else if(e.Button == MouseButtons.Right)
            {
                if (!ActiveStatus)
                {
                    Image.Image = IsFlag ? Extentions.GetImage("Flag.png") : Extentions.GetImage("DeactiveCell.png");
                    OnFlagChange(this, new FlagEventArgs(IsFlag));
                    IsFlag = !IsFlag;
                }
            }
            OnActive(this, null);
        }

        public void SetNeighbors(List<AbstractCell> neighbors)
        {
            Neighbors = neighbors;
        }
    }
}