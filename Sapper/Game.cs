using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Sapper.Cells;

namespace Sapper
{
    public enum GameState
    {
        Start,
        Stop
    }
    public delegate void MethodForLoop(int i, int j);
    public class Game
    {
        private readonly Label flagIndicator;
        private int flagCount;
        private readonly int countOfMines;
        private readonly Panel gameArea;
        public GameState State;
        private readonly List<Point> minesCoordinates;
        private AbstractCell[,] cells = new AbstractCell[Form1.k, Form1.k];
        public AbstractCell this[int i, int j] => cells[i, j];
        public Game(Panel gameArea, int countOfMines, Label flagIndicator)
        {
            this.flagIndicator = flagIndicator;
            this.countOfMines = countOfMines;
            this.gameArea = gameArea;
            minesCoordinates = new List<Point>();
            SetRandomLocationMine();
        }

        public void For(MethodForLoop methodForLoop)
        {
            for (int i = 0; i < Form1.k; i++)
            {
                for (int j = 0; j < Form1.k; j++)
                {
                    methodForLoop.Invoke(i, j);
                }
            }
        }

        public void Start()
        {
            State = GameState.Start;
            For(LoadCell);
            For(SetNeighbors);
        }
        public void Stop()
        {
            State = GameState.Stop;
        }
        private void LoadCell(int i, int j)
        {
            if (minesCoordinates.Any(e => e.X == i && e.Y == j))
            {
                cells[i, j] = new Mine(i, j);
            }
            else
            {
                cells[i, j] = new UndecidedCell(i, j);
            }
            cells[i, j].OnFlagChange += Game_OnFlagChange;
            cells[i, j].OnActive += Game_OnActive;
            gameArea.Controls.Add(cells[i, j].Image);
        }

        private void Game_OnActive(object sender, FlagEventArgs e)
        {
            if (sender is Mine)
            {
                if (((Mine) sender).ActiveStatus)
                {
                    gameArea.Enabled = false;
                    ShowAllMines();
                    Thread thread = new Thread(ExplosiveAllMines);
                    thread.Start();
                }
            }
            if (flagCount == countOfMines)
            {
                if (IsAllCellActiveOrFlag() && IsFlagCoordinateEqualsMinesCoordinate())
                    MessageBox.Show("Сегодня ты выжил!!!");
            }
        }

        private void ExplosiveAllMines()
        {
            foreach (Point item in minesCoordinates.OrderBy(x => x.X).ThenBy(x => x.Y).ToList())
            {
                (cells[item.X, item.Y] as Mine).Explosion();
                Thread.Sleep(300);
            }
        }
        private void ShowAllMines()
        {
            foreach (Point item in minesCoordinates.OrderBy(x => x.X).ThenBy(x => x.Y).ToList())
            {
                (cells[item.X, item.Y] as Mine).Show();
            }
        }

        private bool IsFlagCoordinateEqualsMinesCoordinate()
        {
            List<Point> flagCoordinates = new List<Point>();
            for (int i = 0; i < Form1.k; i++)
            {
                for (int j = 0; j < Form1.k; j++)
                {
                    if(!cells[i,j].IsFlag)
                        flagCoordinates.Add(new Point(cells[i, j].X, cells[i,j].Y));
                }
            }
            return flagCoordinates.SequenceEqual(minesCoordinates.OrderBy(x => x.X).ThenBy(x => x.Y).ToList());
        }

        private void Game_OnFlagChange(object sender, FlagEventArgs e)
        {
            flagCount = e.value ? ++flagCount : --flagCount;
            flagIndicator.Text = $"Кол-во доступных флажков: {countOfMines - flagCount}";
        }

        public bool IsAllCellActiveOrFlag()
        {
            bool isActiveOrFlag = true;
            for (int i = 0; i < Form1.k; i++)
            {
                for (int j = 0; j < Form1.k; j++)
                {
                    isActiveOrFlag = (!cells[i, j].IsFlag || cells[i, j].ActiveStatus) && isActiveOrFlag;
                }
            }
            return isActiveOrFlag;
        }

        private void SetNeighbors(int i, int j)
        {
            List<AbstractCell> Neighbors = new List<AbstractCell>();
            Neighbors.AddAbstractCell(cells, i - 1, j - 1);
            Neighbors.AddAbstractCell(cells, i - 1, j);
            Neighbors.AddAbstractCell(cells, i - 1, j + 1);
            Neighbors.AddAbstractCell(cells, i, j - 1);
            Neighbors.AddAbstractCell(cells, i, j + 1);
            Neighbors.AddAbstractCell(cells, i + 1, j - 1);
            Neighbors.AddAbstractCell(cells, i + 1, j);
            Neighbors.AddAbstractCell(cells, i + 1, j + 1);
            cells[i, j].SetNeighbors(Neighbors);
        }

        private void SetRandomLocationMine()
        {
            Random rnd = new Random();
            for (int i = 0; i < countOfMines; i++)
            {
                int x = rnd.Next(0, Form1.k);
                int y = rnd.Next(0, Form1.k);
                if (minesCoordinates.Any(e => e.X == x && e.Y == y))
                {
                    i--;
                    continue;
                }
                else
                {
                    minesCoordinates.Add(new Point(x, y));
                }
            }
        }
    }

}