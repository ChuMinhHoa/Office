using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Etc
{
    public class DemoAnimation : MonoBehaviour
    {
        public List<GameObject> skins = new();
        private int currentSkinIndex = 0;
        private Vector2 lastMousePosition;
        private Vector2 currentMousePosition;
        private Vector2 delta;

        private bool drag;

        private void Start()
        {
            for (var i = 0; i < skins.Count; i++)
            {
                if (skins[i].activeSelf)
                {
                    currentSkinIndex = i;
                }
            }
        }
        
        [Button(50), ButtonGroup("Skins")]
        private void PreviousSkin()
        {
            skins[currentSkinIndex].SetActive(false);
            currentSkinIndex--;
            if (currentSkinIndex < 0)
            {
                currentSkinIndex = skins.Count - 1;
            }
            skins[currentSkinIndex].SetActive(true);
        }
        
        [Button(50), ButtonGroup("Skins")]
        private void NextSkin()
        {
            skins[currentSkinIndex].SetActive(false);
            currentSkinIndex++;
            if (currentSkinIndex >= skins.Count)
            {
                currentSkinIndex = 0;
            }
            skins[currentSkinIndex].SetActive(true);
        }
        
        // Update is called once per frame
        private void Update()
        {
#if UNITY_EDITOR
            
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
                drag = true;
            }
            if (Input.GetMouseButton(0))
            {
                currentMousePosition = Input.mousePosition;
                delta = currentMousePosition - lastMousePosition;
                lastMousePosition = currentMousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                delta = Vector2.zero;
            }
#endif
            
            if (CheckEqual(currentRotationY, targetRotationY) && (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)))
            {
                drag = false;
            }
            
            if (drag)
                RotateParents(delta);
        }
        public float rotationVelocityX = 0;
        public float targetRotationY = 0;
        public float currentRotationY = 0;
        [Range(0.1f, 50f)]
        public float rotationSpeed = 15f;
        [Range(0f, 1f)]
        public float smoothTime = .25f;

        public DemoAnimation(Vector2 currentMousePosition)
        {
            this.currentMousePosition = currentMousePosition;
        }

        private void RotateParents(Vector2 vectorDelta)
        {
            targetRotationY = currentRotationY - vectorDelta.x * rotationSpeed;
            //targetRotationY = Mathf.Clamp(targetRotationY, -180, 180);
            currentRotationY = Mathf.SmoothDamp(currentRotationY, targetRotationY, ref rotationVelocityX, smoothTime);

            transform.rotation = Quaternion.Euler(0, currentRotationY, 0);
        }

        private bool CheckEqual(float value, float target)
        {
            return Mathf.Round(value) == Mathf.Round(target);
        }
    }
}
