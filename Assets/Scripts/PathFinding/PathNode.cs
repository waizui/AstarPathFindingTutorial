using System;
using CellGrid;

namespace PathFinding
{

    public class PathNode
    {
        protected Cell Cell;

        public int Row { get; private set; }

        public int Col { get; private set; }

        public int FCost { get { return GCost + HCost; } }

        public int GCost;

        public int HCost;

        public PathNode PrevNode;


        protected PathNode()
        {
            this.GCost = int.MaxValue;
            PrevNode = null;
        }

        public PathNode(Cell cell) : this()
        {
            this.Cell = cell;
            this.Row = cell.Row;
            this.Col = cell.Col;
        }

        public PathNode(int row, int col) : this()
        {
            this.Row = row;
            this.Col = col;

        }

    }

}