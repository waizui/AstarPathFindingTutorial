using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellGrid.Core;
using System;
using CellArray = CellGrid.Core.FourDArray<CellGrid.Cell>;

namespace CellGrid
{
    public class GridContainer
    {

        public int Rows;
        public int Cols;

        CellArray cells;

        public HashSet<PathFinding.PathNode> PathNodesCaches = new HashSet<PathFinding.PathNode>();

        public GridContainer()
        {
            cells = new CellArray();
        }

        public GridContainer(int rows, int cols) : this()
        {
            this.Rows = rows;
            this.Cols = cols;

            for (int r = 0; r < rows; r++)
            {
                Debug.DrawLine(new Vector3(0, r * Cell.cellSize), new Vector3(cols * Cell.cellSize, r * Cell.cellSize), Color.white, 1000f);

                for (int c = 0; c < cols; c++)
                {
                    var cell = CreateOrGetCell(r, c);
                    cell.Text = r.ToString() + "," + c.ToString();

                    Debug.DrawLine(new Vector3(c * Cell.cellSize, 0), new Vector3(c * Cell.cellSize, rows * Cell.cellSize), Color.white, 1000f);
                }

                Debug.DrawLine(new Vector3(cols * Cell.cellSize, 0), new Vector3(cols * Cell.cellSize, rows * Cell.cellSize), Color.white, 1000f);

            }
            Debug.DrawLine(new Vector3(0, rows * Cell.cellSize), new Vector3(cols * Cell.cellSize, rows * Cell.cellSize), Color.white, 1000f);
        }

        public Cell this[int row, int col]
        {
            get { return cells[row, col]; }

            set { cells[row, col] = value; }
        }


        public Cell GetCell(int row, int col)
        {
            return cells[row, col];
        }

        public Cell CreateOrGetCell(int row, int col)
        {
            Cell cell = GetCell(row, col);

            if (cell == null)
            {
                cell = new Cell(row, col);
                cell.grid = this;
                cells[row, col] = cell;
            }

            return cell;
        }

        protected void GetPosByWorldPos(Vector3 worldPos, out int x, out int y)
        {
            x = -1;
            y = -1;

            if (worldPos.x >= 0 && worldPos.y >= 0)
            {
                x = Mathf.FloorToInt(worldPos.y / Cell.cellSize);
                y = Mathf.FloorToInt(worldPos.x / Cell.cellSize);
            }
        }

        public Cell GetCell(Vector3 worldPos)
        {
            GetPosByWorldPos(worldPos, out int row, out int col);

            if (row != -1 && col != -1)
            {
                return GetCell(row, col);
            }

            return null;
        }


        public void IterateCell(Func<int, int, Cell, int> iterator)
        {
            cells.IterateWithNull(iterator);
        }

        public Cell SceenPosToCell(Vector3 mousePos)
        {
            var worldpos = Camera.main.ScreenToWorldPoint(mousePos);

            return this.GetCell(worldpos);
        }
    }
}
