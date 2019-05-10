using UnityEngine;

internal sealed class CubeGenerator : MonoBehaviour
{
    private new Camera camera;

    [SerializeField]
    private float rayMaxDistance = float.PositiveInfinity;

    [SerializeField]
    private LayerMask rayLayerMask;

    private void Awake()
    {
        // Cache reference to camera
        this.camera = Camera.main;
    }

    // I write 'DEBUG_' so that I can easily find things I need to remove later (redundant?)
    private int DEBUG_frameCount = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Todo: Create new mesh

            Debug.Log($"MouseDown {this.DEBUG_frameCount}");
        }

        if (Input.GetMouseButton(0))
        {
            // Todo: Update mesh

            var mouseRay = this.GetMouseRay();

            Debug.DrawRay(mouseRay.origin, mouseRay.direction, Color.yellow);

            if (Physics.Raycast(mouseRay, out RaycastHit hit, this.rayMaxDistance, this.rayLayerMask))
            {
                Debug.DrawLine(hit.point, hit.point + Vector3.up, Color.green);
            }

            Debug.Log($"Mouse {this.DEBUG_frameCount}");
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Todo: Give mesh Rigidbody

            Debug.Log($"MouseUp {this.DEBUG_frameCount}");
        }

        this.DEBUG_frameCount++;
    }

    private Ray GetMouseRay()
    {
        return this.camera.ScreenPointToRay(Input.mousePosition);
    }
}
