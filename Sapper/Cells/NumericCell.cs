using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Sapper.Cells
{
    public class NumericCell:AbstractCell
    {

        public NumericCell(int posX, int posY) : base(posX, posY)
        {

        }

        public override void Activate(object sender, MouseEventArgs e)
        {
            if (ActiveStatus)
            {
                Image.Image = Extentions.GetImage($"{ Neighbors.Count(i => i is Mine)}.png");
            }
        }
    }
}