using UnityEngine;

public class PlayerRotationCalculator
{
    private readonly Camera m_camera;
    private readonly Transform m_playerTransform;

    public PlayerRotationCalculator(Camera camera, Transform playerTransform)
    {
        m_camera = camera;
        m_playerTransform = playerTransform;
    }

    public Vector3 Calculate(Vector3 mousePosition)
    {
        var playerScreenPosition = m_camera.WorldToScreenPoint(m_playerTransform.position);
        var delta = (Vector2)mousePosition - (Vector2)playerScreenPosition;

        var cameraRight = m_camera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        var cameraForward = m_camera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        var worldDirection = cameraRight * delta.x + cameraForward * delta.y;
        worldDirection.y = 0;

        if (worldDirection.sqrMagnitude > 0.0001f)
        {
            return m_playerTransform.position + worldDirection;
        }

        return Vector3.zero;
    }
}
