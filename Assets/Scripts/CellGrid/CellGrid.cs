using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellGrid.Core;
using CellArray = CellGrid.Core.FourDArray<CellGrid.Cell>;

namespace CellGrid
{
    public class GridContainer
    {
        CellArray cells;

        public GridContainer()
        {
            cells = new CellArray();
        }

        public Cell this[int row, int col]
        {
            get { return cells[row, col]; }

            set { cells[row, col] = value; }
        }

    }
}
