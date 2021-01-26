using UnityEngine;
using System.Collections;
using CellGrid;
using PathFinding;

public class CoroutineTest : MonoBehaviour
{
    private int rows = 16, cols = 32;

    private GridContainer grid;

    private AStarPathFinding Astar;
    //has corotine started
    private bool HasStarted;

    private bool susped;

    // Use this for initialization
    void Start()
    {
        grid = new GridContainer(rows, cols);
        Astar = new AStarPathFinding(grid);

    }


    void Update()
    {
        if (Input.GetMouseButton(0) && !HasStarted)
        {
            var cell = grid.SceenPosToCell(Input.mousePosition);

            if (cell == null)
                return;

            var targetNode = Astar.GetOrCreateNode(cell.Row, cell.Col, true);

            if (!targetNode.Walkable)
                return;

            HasStarted = true;

            StartCoroutine(
                    Astar.FindPathCoroutine(0, 0, cell.Row, cell.Col,
                        () => { return susped; },// if c key not pressed contin
                        () => this.HasStarted = false
                    )
                );

        }
        else if (Input.GetMouseButton(1) && !HasStarted)
        {
            var cell = grid.SceenPosToCell(Input.mousePosition);

            cell.BackColor = Color.white;
            cell.Text = "";

            Astar.GetOrCreateNode(cell.Row, cell.Col, false);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            susped = !susped;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, Cell.cellSize * 64, Cell.cellSize * 4),
            "左键选取目标点，右键标记不可行走区域,空格暂停 \r\n"
            + "left click select target ,right click mark unwalkable area . space for pause");
    }
}
