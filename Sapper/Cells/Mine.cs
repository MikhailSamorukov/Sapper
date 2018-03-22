using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Sapper.Cells
{
    public class Mine:AbstractCell
    {
        public Mine(int posX, int posY) : base(posX, posY)
        {

        }

        public override void Activate(object sender, MouseEventArgs e)
        {
            base.Activate(sender, e);
            if (ActiveStatus)
            {
                Image.Image = Extentions.GetImage("ActiveMine.png");
            }
        }

        public void Explosion()
        {
            Image.Image = Extentions.GetImage("ExplosiveMine.png");
        }
        public void Show()
        {
            Image.Image = Extentions.GetImage("ShowMine.png");
        }
    }
}