using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
internal sealed class HexGridGenerator : MonoBehaviour
{
    private static float AngleInRadians = Mathf.Deg2Rad * 60f;
    private static float HalfAngleInRadians = AngleInRadians * 0.5f;
    private static float InnerCircumferenceRadius = Mathf.Cos(HalfAngleInRadians);

    private static Vector3 Up = Vector3.up;
    private static Vector3 Down = Vector3.down;
    private static Vector3 TopLeft = Up.RotateCounterClockwise(AngleInRadians);
    private static Vector3 TopRight = Up.RotateClockwise(AngleInRadians);
    private static Vector3 BottomLeft = Down.RotateClockwise(AngleInRadians);
    private static Vector3 BottomRight = Down.RotateCounterClockwise(AngleInRadians);

    private static Vector3 LeftOffset = Up.RotateCounterClockwise(AngleInRadians + HalfAngleInRadians) * InnerCircumferenceRadius * 2f;
    private static Vector3 RightOffset = Up.RotateClockwise(AngleInRadians + HalfAngleInRadians) * InnerCircumferenceRadius * 2f;
    private static Vector3 TopLeftOffset = Up.RotateCounterClockwise(HalfAngleInRadians) * InnerCircumferenceRadius * 2f;
    private static Vector3 TopRightOffset = Up.RotateClockwise(HalfAngleInRadians) * InnerCircumferenceRadius * 2f;
    private static Vector3 BottomLeftOffset = Down.RotateClockwise(HalfAngleInRadians) * InnerCircumferenceRadius * 2f;
    private static Vector3 BottomRightOffset = Down.RotateCounterClockwise(HalfAngleInRadians) * InnerCircumferenceRadius * 2f;

    private MeshFilter meshFilter;
    private new MeshCollider collider;

    private void Reset()
    {
        this.GetComponent<MeshFilter>().mesh = new Mesh();
    }

    private void Awake()
    {
        this.meshFilter = this.GetComponent<MeshFilter>();
        this.collider = this.GetComponent<MeshCollider>();
    }

    private void Start()
    {
        this.GenerateHorizontalHexGrid(10, 10);
    }
    
    private void GenerateHorizontalHexGrid(int xSize, int ySize)
    {
        if (xSize == 0 || ySize == 0) return;

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        Vector3 offset;
        for (int y = 0; y < ySize; y++)
        {
            offset = this.transform.position + (y % 2 == 0 ? Up * 1.5f * y : TopRightOffset + Up * 1.5f * (y - 1));

            for (int x = 0; x < xSize; x++)
            {
                float extrusion = UnityEngine.Random.Range(0f, 0.4f);

                int rootIndex = vertices.Count;
                vertices.Add(offset + Down + Vector3.back * extrusion);
                vertices.Add(offset + BottomLeft + Vector3.back * extrusion);
                vertices.Add(offset + TopLeft + Vector3.back * extrusion);
                vertices.Add(offset + Up + Vector3.back * extrusion);
                vertices.Add(offset + TopRight + Vector3.back * extrusion);
                vertices.Add(offset + BottomRight + Vector3.back * extrusion);

                triangles.Add(rootIndex + 0);
                triangles.Add(rootIndex + 1);
                triangles.Add(rootIndex + 5);

                triangles.Add(rootIndex + 1);
                triangles.Add(rootIndex + 2);
                triangles.Add(rootIndex + 4);

                triangles.Add(rootIndex + 4);
                triangles.Add(rootIndex + 5);
                triangles.Add(rootIndex + 1);

                triangles.Add(rootIndex + 2);
                triangles.Add(rootIndex + 3);
                triangles.Add(rootIndex + 4);

                offset += RightOffset;
            }
        }

        var mesh = this.meshFilter.mesh;
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        this.collider.sharedMesh = mesh;

        this.meshFilter.mesh = mesh;
    }
}
