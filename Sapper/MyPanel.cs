using System.Windows.Forms;

namespace Sapper
{
    public class MyPanel:Panel
    {
        public MyPanel GetClone()
        {
            return (MyPanel)MemberwiseClone();
        }
    }
}