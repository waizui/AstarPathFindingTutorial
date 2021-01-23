using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CellGrid
{

    public sealed class Cell
    {
        public const float cellSize = 16f;

        public int Col { get; private set; }

        public int Row { get; private set; }


        public string Text
        {
            get { return textMesh.text; }
            set { textMesh.text = value; }
        }

        public Color BackColor
        {
            set
            {
            }

        }

        private TextMeshPro textMesh;

        private Vector3 worldPos;

        internal Cell(int row, int col)
        {
            this.Row = row;
            this.Col = col;

            this.worldPos = new Vector3(col, row) * cellSize + new Vector3(cellSize / 2, cellSize / 2);

            CreateMesh();
        }


        private void CreateMesh(Transform parent = null)
        {
            var obj = new GameObject("cell", typeof(TextMeshPro));
            var tran = obj.transform as RectTransform;

            tran.position = worldPos;
            tran.SetParent(parent, false);
            tran.sizeDelta = new Vector2(cellSize, cellSize);

            var textMesh = obj.GetComponent<TextMeshPro>();
            this.textMesh = textMesh;
            textMesh.alignment = TextAlignmentOptions.Center;

        }

    }

}