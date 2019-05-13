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
            new Vector3(-0.5f, -0.5f, -0.5f),   // CBL 0
            new Vector3(-0.5f, -0.5f, 0.5f),    // FBL 1
            new Vector3(0.5f, -0.5f, 0.5f),     // FBR 2
            new Vector3(0.5f, -0.5f, -0.5f),    // CBR 3
            new Vector3(-0.5f, 0.5f, -0.5f),    // CTL 4
            new Vector3(-0.5f, 0.5f, 0.5f),     // FTL 5
            new Vector3(0.5f, 0.5f, 0.5f),      // FTR 6
            new Vector3(0.5f, 0.5f, -0.5f),     // CTR 7
        };

        mesh.triangles = new int[]
        {
            2, 1, 0,    // B
            0, 3, 2,
            1, 2, 5,    // F
            2, 6, 5,
            2, 3, 6,    // R
            3, 7, 6,
            3, 0, 4,    // C
            4, 7, 3,
            0, 1, 4,    // L
            1, 5, 4,
            4, 5, 6,    // T
            6, 7, 4
        };

        mesh.Optimize();
        mesh.RecalculateNormals();

        this.meshFilter.mesh = mesh;
    }
}
