using UnityEngine;

namespace _Game.Scripts.Etc
{
    public class DemoAnimation : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }
        Vector2 lastMousePosition;
        Vector2 currentMousePosition;
        Vector2 delta;
        // Update is called once per frame
        void Update()
        {
#if UNITY_EDITOR
            
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                currentMousePosition = Input.mousePosition;
                delta = currentMousePosition - lastMousePosition;
            
                lastMousePosition = currentMousePosition;
            }
#endif
            if (delta != Vector2.zero)
                RotateParents(delta);
        }
        public float rotationVelocityX = 0;
        public float targetRotationY = 0;
        public float currentRotationY = 0;
        [Range(0.1f, 50f)]
        public float rotationSpeed = 15f;
        [Range(0f, 1f)]
        public float smoothTime = .25f;
        private void RotateParents(Vector2 delta)
        {
            targetRotationY = currentRotationY - delta.x * rotationSpeed;
            targetRotationY = Mathf.Clamp(targetRotationY, -180, 180);
            currentRotationY = Mathf.SmoothDamp(currentRotationY, targetRotationY, ref rotationVelocityX, smoothTime);

            transform.rotation = Quaternion.Euler(0, currentRotationY, 0);
        }
    }
}
