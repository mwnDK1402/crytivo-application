using UnityEngine;
using UnityObject = UnityEngine.Object;

internal sealed class CuboidGenerator : MonoBehaviour
{
    [SerializeField]
    private Cuboid cuboidPrefab;

    [SerializeField]
    private float rayMaxDistance = float.PositiveInfinity;

    [SerializeField]
    private LayerMask rayLayerMask;

    private new Camera camera;
    private Cuboid selectedCuboid;

    private void Awake()
    {
        // Cache reference to camera
        this.camera = Camera.main;
    }

    // I write 'DEBUG_' so that I can easily find things I need to remove later (redundant?)
    private int DEBUG_frameCount = 0;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var mouseWorldPoint = this.GetMouseWorldPoint();

            if (Input.GetMouseButtonDown(0))
            {
                // Create new cuboid

                if (mouseWorldPoint.HasValue)
                {
                    this.selectedCuboid = UnityObject.Instantiate(
                        this.cuboidPrefab,
                        mouseWorldPoint.Value,
                        Quaternion.identity);
                }

                Debug.Log($"MouseDown {this.DEBUG_frameCount}");
            }

            // Update cuboid position

            if (this.selectedCuboid && mouseWorldPoint.HasValue)
            {
                this.selectedCuboid.BaseCenter = mouseWorldPoint.Value;
            }

            Debug.Log($"Mouse {this.DEBUG_frameCount}");
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Activate cuboid physics
            this.selectedCuboid.UsePhysics = true;
            this.selectedCuboid = null;

            Debug.Log($"MouseUp {this.DEBUG_frameCount}");
        }

        this.DEBUG_frameCount++;
    }

    private Vector3? GetMouseWorldPoint()
    {
        var mouseRay = this.GetMouseRay();

        Debug.DrawRay(mouseRay.origin, mouseRay.direction, Color.yellow);

        if (Physics.Raycast(
            mouseRay,
            out RaycastHit hit,
            this.rayMaxDistance,
            this.rayLayerMask))
        {
            Debug.DrawLine(hit.point, hit.point + Vector3.up, Color.green);
            return hit.point;
        }

        return null;
    }

    private Ray GetMouseRay()
    {
        return this.camera.ScreenPointToRay(Input.mousePosition);
    }

    // Todo: Turn the generation process into a state machine
    private enum GenerationState
    {
        Idle,
        CreateCuboid,       // Mouse pressed
        SetCuboidBase,      // Mouse held
        SetCuboidHeight,    // Mouse released
        SetCuboidPhysics    // Mouse pressed
    }
}
