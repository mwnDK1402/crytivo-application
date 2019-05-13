using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshFilter))]
internal sealed class Cuboid : MonoBehaviour
{
    private Rigidbody rb;
    private MeshFilter meshFilter;

    public bool UsePhysics
    {
        get => !this.rb.isKinematic;
        set => this.rb.isKinematic = !value;
    }

    public Vector3 BaseCenter
    {
        get => this.rb.position - (new Vector3(0f, this.Height, 0f) * 0.5f);
        set => this.rb.position = value + (new Vector3(0f, this.Height, 0f) * 0.5f);
    }

    private float Height => this.transform.localScale.y;

    private void Awake()
    {
        this.rb = this.GetComponent<Rigidbody>();
        this.meshFilter = this.GetComponent<MeshFilter>();
    }

    private void Start()
    {
        var mesh = this.meshFilter.mesh;

        mesh.Clear();

        mesh.vertices = new Vector3[]
        {
            // B
            new Vector3(0.5f, -0.5f, -0.5f),    // CBR 0
            new Vector3(0.5f, -0.5f, 0.5f),     // FBR 1
            new Vector3(-0.5f, -0.5f, 0.5f),    // FBL 2
            new Vector3(-0.5f, -0.5f, -0.5f),   // CBL 3

            // F
            new Vector3(0.5f, -0.5f, 0.5f),     // FBR 4
            new Vector3(0.5f, 0.5f, 0.5f),      // FTR 5
            new Vector3(-0.5f, 0.5f, 0.5f),     // FTL 6
            new Vector3(-0.5f, -0.5f, 0.5f),    // FBL 7

            // R
            new Vector3(0.5f, -0.5f, -0.5f),    // CBR 8
            new Vector3(0.5f, 0.5f, -0.5f),     // CTR 9
            new Vector3(0.5f, 0.5f, 0.5f),      // FTR 10
            new Vector3(0.5f, -0.5f, 0.5f),     // FBR 11

            // C
            new Vector3(-0.5f, -0.5f, -0.5f),   // CBL 12
            new Vector3(-0.5f, 0.5f, -0.5f),    // CTL 13
            new Vector3(0.5f, 0.5f, -0.5f),     // CTR 14
            new Vector3(0.5f, -0.5f, -0.5f),    // CBR 15

            // L
            new Vector3(-0.5f, -0.5f, 0.5f),    // FBL 16
            new Vector3(-0.5f, 0.5f, 0.5f),     // FTL 17
            new Vector3(-0.5f, 0.5f, -0.5f),    // CTL 18
            new Vector3(-0.5f, -0.5f, -0.5f),   // CBL 19

            // T
            new Vector3(-0.5f, 0.5f, -0.5f),    // CTL 20
            new Vector3(-0.5f, 0.5f, 0.5f),     // FTL 21
            new Vector3(0.5f, 0.5f, 0.5f),      // FTR 22
            new Vector3(0.5f, 0.5f, -0.5f),     // CTR 23
        };

        mesh.triangles = new int[]
        {
            0, 1, 2,    // B
            2, 3, 0,
            4, 5, 6,    // F
            6, 7, 4,
            8, 9, 10,    // R
            10, 11, 8,
            12, 13, 14,    // C
            14, 15, 12,
            16, 17, 18,    // L
            18, 19, 16,
            20, 21, 22,    // T
            22, 23, 20
        };

        mesh.Optimize();
        mesh.RecalculateNormals();

        this.meshFilter.mesh = mesh;
    }
}
