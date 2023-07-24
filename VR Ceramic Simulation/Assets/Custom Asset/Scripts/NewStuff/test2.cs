using UnityEngine;
using OculusSampleFramework;

public class test2 : MonoBehaviour
{
    // Reference to OVRHand components for both hands
    public OVRHand leftHand;
    public OVRHand rightHand;

    // Strength of deformation
    public float deformationStrength = 0.01f;

    // Radius of the deformation area
    public float deformationRadius = 0.03f;

    // Reference to the MeshFilter component
    private MeshFilter meshFilter;
    private Vector3[] originalVertices;
    private Vector3[] meshVerticies;

    void Start()
    {
        // Get the original vertex positions of the mesh
        meshFilter = GetComponent<MeshFilter>();
        originalVertices = meshFilter.mesh.vertices;
        meshVerticies = meshFilter.mesh.vertices;
    }

    void Update()
    {
        // Check if both hands are pinching
        bool leftPinching = leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool rightPinching = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool leftGrab = leftHand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
        bool rightGrab = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Ring);

        if (leftPinching && rightPinching)
        {
            DeformMesh();
        }

        else if (leftGrab && rightGrab)
        {
            DeformMeshOut();
        }

        // else if (!leftPinching && !rightPinching)
        // {
        //     //* Reset the mesh to its original shape (debug purpose)
        //     meshFilter.mesh.vertices = originalVertices;
        //     UpdateMeshVertices();
        // }
    }

    void DeformMesh()
    {
        Vector3 leftPosition = leftHand.PointerPose.position;
        Vector3 leftDirection = leftHand.PointerPose.forward;
        Vector3 leftVelocity = leftHand.GetFingerPinchStrength(OVRHand.HandFinger.Index) * leftHand.PointerPose.forward;

        Vector3 rightPosition = rightHand.PointerPose.position;
        Vector3 rightDirection = rightHand.PointerPose.forward;
        Vector3 rightVelocity = rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Index) * rightHand.PointerPose.forward;

        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertexPosition = transform.TransformPoint(originalVertices[i]);
            float distanceToLeftHand = Vector3.Distance(vertexPosition, leftPosition);
            float distanceToRightHand = Vector3.Distance(vertexPosition, rightPosition);

            if (distanceToLeftHand <= deformationRadius)
            {
                float deformationAmount = deformationStrength * (1 - distanceToLeftHand / deformationRadius);
                Vector3 deformationVector = leftVelocity * deformationAmount;
                meshVerticies[i] += transform.InverseTransformDirection(deformationVector);
            }

            if (distanceToRightHand <= deformationRadius)
            {
                float deformationAmount = deformationStrength * (1 - distanceToRightHand / deformationRadius);
                Vector3 deformationVector = rightVelocity * deformationAmount;
                meshVerticies[i] += transform.InverseTransformDirection(deformationVector);
            }
        }

        UpdateMeshVertices();
    }

    void DeformMeshOut()
    {
        Vector3 leftPosition = leftHand.PointerPose.position;
        Vector3 leftDirection = leftHand.PointerPose.forward;
        Vector3 leftVelocity = leftHand.GetFingerPinchStrength(OVRHand.HandFinger.Ring) * leftHand.PointerPose.forward;

        Vector3 rightPosition = rightHand.PointerPose.position;
        Vector3 rightDirection = rightHand.PointerPose.forward;
        Vector3 rightVelocity = rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Ring) * rightHand.PointerPose.forward;

        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertexPosition = transform.TransformPoint(originalVertices[i]);
            float distanceToLeftHand = Vector3.Distance(vertexPosition, leftPosition);
            float distanceToRightHand = Vector3.Distance(vertexPosition, rightPosition);

            if (distanceToLeftHand <= deformationRadius)
            {
                float deformationAmount = deformationStrength * (1 - distanceToLeftHand / deformationRadius);
                Vector3 deformationVector = -leftVelocity * deformationAmount;
                meshVerticies[i] += transform.InverseTransformDirection(deformationVector);
            }

            if (distanceToRightHand <= deformationRadius)
            {
                float deformationAmount = deformationStrength * (1 - distanceToRightHand / deformationRadius);
                Vector3 deformationVector = -rightVelocity * deformationAmount;
                meshVerticies[i] += transform.InverseTransformDirection(deformationVector);
            }
        }

        UpdateMeshVertices();
    }

    // Set the deformed vertices and recalculate the normals
    void UpdateMeshVertices()
    {
        meshFilter.mesh.vertices = meshVerticies;
        meshFilter.mesh.RecalculateNormals();
    }
}