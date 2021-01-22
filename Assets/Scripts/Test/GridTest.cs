using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellGrid;

public class GridTest : MonoBehaviour
{
    void Start()
    {
        var grid = new GridContainer();

        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                var cell = new Cell(r, c);
                cell.Text = (r + c).ToString();
                grid[r, c] = cell;
            }
        }
    }

}
