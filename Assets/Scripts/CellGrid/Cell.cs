using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CellGrid
{

    public sealed class Cell
    {
        public PathFinding.PathNode PathNode;

        public const float cellSize = 16f;

        public int Col { get; private set; }

        public int Row { get; private set; }


        public string Text
        {
            get { return textMesh.text; }
            set
            {
                if (textMesh.text != value)
                {
                    textMesh.text = value;
                }
            }
        }

        public Color BackColor
        {
            set
            {
                this.panelMat.SetColor("_Color", value);
            }

        }

        internal GridContainer grid;

        private Material panelMat;

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

            var backPanel = new GameObject("back", typeof(MeshRenderer), typeof(MeshFilter));

            var filter = backPanel.GetComponent<MeshFilter>();
            var meshRenderer = backPanel.GetComponent<MeshRenderer>();

            filter.mesh = MeshGenerator.GetQuad(cellSize);

            var shader = Shader.Find("Unlit/Color");
            var mat = new Material(shader);
            mat.SetColor("_Color", Color.black);

            meshRenderer.material = mat;
            this.panelMat = mat;

            var panelTrans = backPanel.transform;

            panelTrans.position = worldPos - new Vector3(cellSize / 2, cellSize / 2);

        }

        public override string ToString()
        {
            return Row.ToString() + "," + Col.ToString();
        }
    }

}