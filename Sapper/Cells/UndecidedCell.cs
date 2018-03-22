using System.Linq;
using System.Windows.Forms;

namespace Sapper.Cells
{
    public class UndecidedCell:AbstractCell
    {
        private AbstractCell target;
        public UndecidedCell(int posX, int posY) : base(posX, posY)
        {
        }

        public override void Activate(object sender, MouseEventArgs e)
        {
            base.Activate(sender, e);
            if (ActiveStatus)
            {
                if (Neighbors.Any(i => i is Mine))
                {
                    target = new NumericCell(PosX, PosY);
                    target.IsFlag = false;
                    target.ActiveStatus = true;
                    target.SetNeighbors(Neighbors);
                    target.Activate(sender, e);
                }
                else
                {
                    target = new EmptyCell(PosX, PosY);
                    target.IsFlag = false;
                    target.ActiveStatus = true;
                    target.SetNeighbors(Neighbors);
                    target.Activate(sender, e);
                }
                this.Image.Image = target.Image.Image;
            }
        }
    }
}