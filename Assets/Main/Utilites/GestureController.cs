using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class GestureController : MonoBehaviour
{
    [SerializeField] float zoomSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float dragYSpeed;

    private float prevDist;
    private float prevAngle;
    private Vector2 prevCenter;

    private void OnEnable() => EnhancedTouchSupport.Enable();
    private void OnDisable() => EnhancedTouchSupport.Disable();

    void Update()
    {
        if (Touch.activeTouches.Count == 2)
        {
            Vector2 pos1 = Touch.activeTouches[0].screenPosition;
            Vector2 pos2 = Touch.activeTouches[1].screenPosition;

            float dist = Vector2.Distance(pos1, pos2);
            float distDelta = prevDist > 0 ? dist - prevDist : 0;

            float angle = Mathf.Atan2((pos2 - pos1).y, (pos2 - pos1).x) * Mathf.Rad2Deg;
            float angleDelta = prevAngle != 0 ? Mathf.DeltaAngle(prevAngle, angle) : 0;

            Vector2 center = (pos1 + pos2) * 0.5f;
            float centerYDelta = prevCenter != Vector2.zero ? center.y - prevCenter.y : 0;

            // Choose dominant gesture
            float absDist = Mathf.Abs(distDelta);
            float absAngle = Mathf.Abs(angleDelta) * 4;
            float absY = Mathf.Abs(centerYDelta);

            if (absDist > absAngle && absDist > absY)
            {
                // Pinch
                float scaleChange = 1 + distDelta * zoomSpeed;
                transform.localScale *= scaleChange;
            }
            else if (absAngle > absDist && absAngle > absY)
            {
                // Twist
                transform.Rotate(Vector3.up, - angleDelta * rotationSpeed, Space.World);
            }
            else if (absY > absDist && absY > absAngle)
            {
                // Drag Y
                transform.position += centerYDelta * dragYSpeed * Vector3.up;
            }

            prevDist = dist;
            prevAngle = angle;
            prevCenter = center;
        }
        else
        {
            prevDist = 0;
            prevAngle = 0;
            prevCenter = Vector2.zero;
        }

    }
}
