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

        private TextMeshPro textMesh;

        private Vector3 worldPos;

        public Cell(int row, int col)
        {
            this.Row = row;
            this.Col = col;

            this.worldPos = new Vector3(row, col) * cellSize;

            CreateMesh();
        }


        private void CreateMesh(Transform parent = null)
        {
            var obj = new GameObject("cell", typeof(TextMeshPro));
            obj.transform.SetParent(parent, false);
            obj.transform.position = worldPos;

            var textMesh = obj.GetComponent<TextMeshPro>();
            this.textMesh = textMesh;
            textMesh.alignment = TextAlignmentOptions.Center;
        }

    }

}