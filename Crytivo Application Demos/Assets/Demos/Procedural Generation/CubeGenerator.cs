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

    private void Update()
    {
        var mouseRay = this.GetMouseRay();

        Debug.DrawRay(mouseRay.origin, mouseRay.direction, Color.yellow);

        if (Physics.Raycast(mouseRay, out RaycastHit hit, this.rayMaxDistance, this.rayLayerMask))
        {
            Debug.DrawLine(hit.point, hit.point + Vector3.up, Color.green);
        }
    }

    private Ray GetMouseRay()
    {
        return this.camera.ScreenPointToRay(Input.mousePosition);
    }
}
