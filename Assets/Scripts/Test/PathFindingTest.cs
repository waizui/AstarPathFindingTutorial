using System.Collections;
using System.Collections.Generic;
using CellGrid;
using UnityEngine;
using PathFinding;

public class PathFindingTest : MonoBehaviour
{
    private int rows = 16, cols = 32;

    private GridContainer grid;

    private AStarPathFinding Astar;

    void Start()
    {
        grid = new GridContainer(rows, cols);
        Astar = new AStarPathFinding();
    }


    private void Update()
    {

        if (Input.GetMouseButton(0))
        {
            var worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var cell = grid.GetCell(worldpos);

            if (cell != null)
            {
                cell.Text = "<color=black>right</color>\r\n main \r\n bottom";
                cell.BackColor = Color.red;
            }


            var path = Astar.FindPath(grid, 0, 0, cell.Row, cell.Col);


            if (path != null)
            {
                foreach (var node in path)
                {
                    var pCell = grid.CreateOrGetCell(node.Row, node.Col);
                    pCell.BackColor = Color.green;
                }

            }

        }

    }

}
