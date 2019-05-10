using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
internal sealed class Cuboid : MonoBehaviour
{
    private Rigidbody rb;

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
    }
}
