using UnityEngine;

internal static class VectorExtensions
{
    public static Vector2 RotateClockwise(this Vector2 vector, float radians)
    {
        return vector.RotateCounterClockwise(-radians);
    }

    public static Vector2 RotateCounterClockwise(this Vector2 vector, float radians)
    {
        return new Vector2(
            Mathf.Cos(radians) * vector.x - Mathf.Sin(radians) * vector.y,
            Mathf.Sin(radians) * vector.x + Mathf.Cos(radians) * vector.y);
    }

    public static Vector3 RotateClockwise(this Vector3 vector, float radians)
    {
        return ((Vector2)vector).RotateClockwise(radians);
    }

    public static Vector3 RotateCounterClockwise(this Vector3 vector, float radians)
    {
        return ((Vector2)vector).RotateCounterClockwise(radians);
    }
}
