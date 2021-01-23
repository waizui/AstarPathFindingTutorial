using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellGrid;

public class GridTest : MonoBehaviour
{
    private int rows = 16, cols = 32;

    void Start()
    {
        var grid = new GridContainer(rows, cols);

    }

}
