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
        Astar = new AStarPathFinding(grid);
    }


    private void Update()
    {
        //左键选中目标点（起点默认0，0）
        //left click to mark target point,defaut start point is (0,0)
        if (Input.GetMouseButton(0))
        {

            var cell = MousePosToCell(Input.mousePosition);

            if (cell == null)
                return;

            var targetNode = Astar.GetOrCreateNode(cell.Row, cell.Col, true);

            if (!targetNode.Walkable)
                return;

            var path = Astar.FindPath(0, 0, cell.Row, cell.Col);


            if (path != null)
            {
                foreach (var node in path)
                {
                    var pCell = grid.CreateOrGetCell(node.Row, node.Col);
                    pCell.Text = string.Format("<color=red>G:{0}</color>\r\n <color=red>F:{1}</color>\r\n <color=red>H:{2}</color>",
                        node.GCost, node.FCost, node.HCost);
                    pCell.BackColor = Color.green;
                }

            }

            cell.Text = "<color=black>right</color>\r\n main \r\n bottom";
            cell.BackColor = Color.red;

        }
        //右键标记无法行走
        //right click to mark unwalkable cell
        else if (Input.GetMouseButton(1))
        {
            var cell = MousePosToCell(Input.mousePosition);

            if (cell == null)
                return;

            cell.BackColor = Color.white;
            cell.Text = "";

            Astar.GetOrCreateNode(cell.Row, cell.Col, false);
        }

    }


    private Cell MousePosToCell(Vector3 mousePOs)
    {
        var worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return grid.GetCell(worldpos);
    }

}
