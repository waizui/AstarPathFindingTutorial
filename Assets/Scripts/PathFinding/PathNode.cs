using System;
using CellGrid;

namespace PathFinding
{

    public class PathNode
    {

        public int Row { get; private set; }

        public int Col { get; private set; }

        public int FCost { get { return GCost + HCost; } }

        public int GCost;

        public int HCost;

        public PathNode PrevNode;

        public bool Walkable { get; set; }

        protected PathNode()
        {
            this.GCost = int.MaxValue;
            PrevNode = null;
        }

        public PathNode(Cell cell) : this()
        {
            this.Row = cell.Row;
            this.Col = cell.Col;
            cell.PathNode = this;
        }

        public PathNode(GridContainer grid, int row, int col) : this()
        {
            this.Row = row;
            this.Col = col;

            var cell = grid.CreateOrGetCell(row, col);

            cell.PathNode = this;
        }


        public override string ToString()
        {
            return Row.ToString() + Col.ToString();
        }

    }

}