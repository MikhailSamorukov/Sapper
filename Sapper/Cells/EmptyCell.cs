using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Sapper.Cells
{
    public class EmptyCell:AbstractCell 
    {
        public override void Activate(object sender, MouseEventArgs e)
        {
            if (ActiveStatus)
            {
                Image.Image = Extentions.GetImage("ActiveEmptyCell.png");
                Neighbors
                    .Where(i => !i.ActiveStatus && i.IsFlag)
                    .ToList()
                    .ForEach(i => i.Activate(sender, e));
            }
        }

        public EmptyCell(int posX, int posY) : base(posX, posY)
        {
        }
    }
}