using UnityEngine;
using System.Collections;
using JP.Framework.Flow;

namespace Gameplay.Cameras
{
    public class CameraController : SingletonBehaviour<CameraController>
    {
        [SerializeField]
        private Transform cameraTarget;

        [SerializeField]
        private Camera camera;

        private Vector3 mousePanStart;
        private Vector3 mouseRotateStart;

        private Vector3 targetPosition;
        private Quaternion targetRotation;

        private void Update()
        {
            Vector3 panMovement = GetPanMovement();
            Vector2 angleMovement = GetAngleMovement();

            targetPosition += cameraTarget.right * panMovement.x + cameraTarget.forward * panMovement.z;
            targetRotation = cameraTarget.rotation * Quaternion.Euler(Vector3.up * angleMovement.x / 5f);

            cameraTarget.position = targetPosition;
            cameraTarget.rotation = targetRotation;
        }

        private Vector3 GetPanMovement()
        {
            Vector3 panMovement = Vector3.zero;

            if (Input.GetKey(KeyCode.A))
                panMovement.x -= 1f;
            if (Input.GetKey(KeyCode.D))
                panMovement.x += 1f;

            if (Input.GetKey(KeyCode.W))
                panMovement.z += 1f;
            if (Input.GetKey(KeyCode.S))
                panMovement.z -= 1f;

            return panMovement;
        }

        private Vector2 GetAngleMovement()
        {
            Vector2 angleMovement = Vector2.zero;

            if (Input.GetKey(KeyCode.LeftArrow))
                angleMovement.x -= 1f;
            if (Input.GetKey(KeyCode.RightArrow))
                angleMovement.x += 1f;

            if (Input.GetKey(KeyCode.UpArrow))
                angleMovement.y += 1f;
            if (Input.GetKey(KeyCode.DownArrow))
                angleMovement.y -= 1f;

            if (Input.GetMouseButtonDown(2))
            {
                mouseRotateStart = Input.mousePosition;
            }
            if (Input.GetMouseButton(2))
            {
                Vector3 mouseRotateDelta = Input.mousePosition - mouseRotateStart;
                angleMovement.x = mouseRotateDelta.x;
            }

            return angleMovement;
        }
    }
}

