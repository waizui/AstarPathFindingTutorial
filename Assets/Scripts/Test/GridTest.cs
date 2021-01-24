using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellGrid;

public class GridTest : MonoBehaviour
{
    private int rows = 16, cols = 32;

    private GridContainer grid;


    void Start()
    {
        grid = new GridContainer(rows, cols);
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
        }

    }

}
