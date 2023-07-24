using UnityEngine;

public class SphereController : MonoBehaviour
{
    public OVRHand leftHand;
    public OVRHand rightHand;
    private bool isGrabbing = false;

    void Start()
    {
        // Get reference to the Oculus Hand component
    }

    void Update()
    {
        // Check if the hand is making a grabbing motion
        if (leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index) && !isGrabbing)
        {
            // If the hand is grabbing for the first time, attach the sphere to the hand
            isGrabbing = true;
            transform.parent = leftHand.transform;
        }
        else if (rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index) && !isGrabbing)
        {
            isGrabbing = true;
            transform.parent = rightHand.transform;
        }

        else if (!leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index) && isGrabbing)
        {
            // If the hand stops grabbing, detach the sphere from the hand and keep its position
            isGrabbing = false;
            transform.parent = null;
        }
        else if (!rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index) && isGrabbing)
        {
            // If the hand stops grabbing, detach the sphere from the hand and keep its position
            isGrabbing = false;
            transform.parent = null;
        }
    }
}
