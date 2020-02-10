using UnityEngine;
using JP.Framework.Flow;

namespace Gameplay.Cameras
{
    public class CameraController : SingletonBehaviour<CameraController>
    {
        [SerializeField]
        private Transform cameraTarget;

        [SerializeField]
        private Camera camera;

        [SerializeField]
        private float normalMovementSpeed = 0.08f;

        [SerializeField]
        private float fastMovementSpeed = 0.15f;

        [SerializeField]
        private float movementTime = 10f;

        [SerializeField]
        private float rotationSpeed = 0.3f;

        [SerializeField]
        private float zoomSpeed = 0.3f;

        private float movementSpeed;

        private Vector3 targetPosition;
        private Quaternion targetRotation;
        private Vector3 zoomDirection;
        private float zoom = 20f;

        private Vector3 mouseDragStart;
        private Vector3 mouseDragCurrent;
        private Vector3 mouseRotateStart;
        private Vector3 mouseRotateCurrent;

        private Plane mousePlane;

        private void Awake()
        {
            targetPosition = cameraTarget.position;
            targetRotation = cameraTarget.rotation;

            zoomDirection = camera.transform.position - cameraTarget.position;
            zoomDirection.x = 0f;
            zoomDirection.Normalize();

            mousePlane = new Plane(Vector3.up, Vector3.zero);
        }

        private void Update()
        {
            HandleMouseInput();
            HandleKeyboardInput();

            camera.transform.LookAt(cameraTarget);
        }

        private void HandleMouseInput()
        {
            zoom -= Input.mouseScrollDelta.y;

            if (Input.GetMouseButtonDown(0))
            {
                Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);

                float distance;
                if (mousePlane.Raycast(mouseRay, out distance))
                    mouseDragStart = mouseRay.GetPoint(distance);
            }

            if (Input.GetMouseButton(0))
            {
                Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);

                float distance;
                if (mousePlane.Raycast(mouseRay, out distance))
                    mouseDragCurrent = mouseRay.GetPoint(distance);

                targetPosition = cameraTarget.position + mouseDragStart - mouseDragCurrent;
            }

            if (Input.GetMouseButtonDown(2))
                mouseRotateStart = Input.mousePosition;
            
            if (Input.GetMouseButton(2))
            {
                mouseRotateCurrent = Input.mousePosition;

                Vector3 delta = mouseRotateStart - mouseRotateCurrent;
                mouseRotateStart = mouseRotateCurrent;

                targetRotation *= Quaternion.Euler(Vector3.up * (-delta.x / 5f));
            }
        }

        private void HandleKeyboardInput()
        {
            movementSpeed = Input.GetKey(KeyCode.LeftShift) ? fastMovementSpeed : normalMovementSpeed;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                targetPosition += cameraTarget.forward * movementSpeed;

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                targetPosition += cameraTarget.forward * -movementSpeed;

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                targetPosition += cameraTarget.right * movementSpeed;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                targetPosition += cameraTarget.right * -movementSpeed;

            if (Input.GetKey(KeyCode.Q))
                targetRotation *= Quaternion.Euler(Vector3.up * rotationSpeed);
            if (Input.GetKey(KeyCode.E))
                targetRotation *= Quaternion.Euler(Vector3.up * -rotationSpeed);

            if (Input.GetKey(KeyCode.R))
                zoom -= zoomSpeed;
            if (Input.GetKey(KeyCode.F))
                zoom += zoomSpeed;
            zoom = Mathf.Clamp(zoom, 1f, 200f);

            cameraTarget.position = Vector3.Lerp(cameraTarget.position, targetPosition, Time.deltaTime * movementTime);
            cameraTarget.rotation = Quaternion.Lerp(cameraTarget.rotation, targetRotation, Time.deltaTime * movementTime);
            camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, zoomDirection * zoom, Time.deltaTime * movementTime);
        }
    }
}