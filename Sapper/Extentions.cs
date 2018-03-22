using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Sapper.Cells;

namespace Sapper
{
    public static class Extentions
    {
        public static void AddAbstractCell(this List<AbstractCell> list, AbstractCell[,] array, int i, int j)
        {
            try
            {
                list.Add(array[i, j]);
            }
            catch
            {
                // ignored
            }
        }

        public static Bitmap GetImage(string ImageName)
        {
            return new Bitmap(Directory.GetCurrentDirectory() + $@"\Images\{ImageName}");
        }
    }
    public class FlagEventArgs : EventArgs
    {
        public bool value { get; }
        public FlagEventArgs(bool value)
        {
            this.value = value;
        }
    }
    public class MinesCountEventArgs : EventArgs
    {
        public int value { get; }
        public MinesCountEventArgs(int value)
        {
            this.value = value;
        }
    }
}