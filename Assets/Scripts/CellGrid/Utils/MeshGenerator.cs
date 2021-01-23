using System;
using UnityEngine;

namespace CellGrid
{
    public class MeshGenerator
    {

        public static Mesh GetQuad(float size)
        {
            Mesh mesh = new Mesh();

            Vector3[] vertices = new Vector3[4];
            Vector2[] uvs = new Vector2[4];
            int[] triangels = new int[6];

            vertices[0] = new Vector3(0, 0);
            vertices[1] = new Vector3(0, size);
            vertices[2] = new Vector3(size, size);
            vertices[3] = new Vector3(size, 0);

            uvs[0] = new Vector2(0, 0);
            uvs[1] = new Vector2(0, 1);
            uvs[2] = new Vector2(1, 1);
            uvs[3] = new Vector2(1, 0);

            triangels[0] = 0;
            triangels[1] = 1;
            triangels[2] = 2;

            triangels[3] = 0;
            triangels[4] = 2;
            triangels[5] = 3;


            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = triangels;

            return mesh;
        }

    }
}


