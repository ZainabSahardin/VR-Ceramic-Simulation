using UnityEngine;

public class SphereManipulation : MonoBehaviour
{
    private bool isInteracting;
    private Transform interactionTransform;

    private void Start()
    {
        // Add Mesh Collider, Mesh Filter, and Mesh Renderer components to the sphere object
        gameObject.AddComponent<MeshCollider>();
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
    }

    private void Update()
    {
        // Check if the hand is interacting with the sphere
        if (isInteracting)
        {
            // Get the position of the hand and set it as the position of the sphere
            Vector3 handPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.Hands);
            interactionTransform.position = handPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger collider is from the hand
        if (other.CompareTag("Hand"))
        {
            // Set isInteracting flag to true and record the transform of the hand
            isInteracting = true;
            interactionTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the trigger collider is from the hand
        if (other.CompareTag("Hand"))
        {
            // Set isInteracting flag to false and clear the interactionTransform
            isInteracting = false;
            interactionTransform = null;
        }
    }
}
