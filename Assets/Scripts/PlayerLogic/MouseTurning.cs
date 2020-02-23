using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SA
{
    //mouuuuuse
    public class MouseTurning : MonoBehaviour
    {
        int floorMask;
        float camRayLength = 100f;
        StateManager states;

        private void Start()
        {
            states = GetComponent<StateManager>();
            floorMask = LayerMask.GetMask("Floor");
        }

        public void Turning()
        {

            // Create a ray from the mouse cursor on screen in the direction of the camera.
            Ray camRay = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            // Create a RaycastHit variable to store information about what was hit by the ray.
            RaycastHit floorHit;

            // Perform the raycast and if it hits something on the floor layer...
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = floorHit.point - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                states.transform.rotation = Quaternion.Slerp(states.transform.rotation, newRotation, states.delta / states.rotateSpeed);

            }
        }
    }
}