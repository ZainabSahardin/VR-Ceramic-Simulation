using UnityEngine;
using OculusSampleFramework;

public class HandPinchMeshDeformer : MonoBehaviour
{
    // Reference to OVRHand components for both hands
    public OVRHand leftHand;
    public OVRHand rightHand;
    // Strength of deformation
    public float deformationStrength = 0.1f;

    // Reference to the MeshFilter component
    private MeshFilter meshFilter;
    private Vector3[] originalVertices;
    private Vector3[] meshVerticies;

    void Start()
    {
        // Check if hand references and mesh filter reference are set
        if (leftHand == null || rightHand == null)
        {
            Debug.LogError("Hand references not set.");
        }

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

        if (leftPinching && rightPinching)
        {
            // Get the position, rotation, and forward direction of each hand
            Vector3 leftPosition = leftHand.PointerPose.position;
            Quaternion leftRotation = leftHand.PointerPose.rotation;
            Vector3 leftDirection = leftRotation * Vector3.forward;

            Vector3 rightPosition = rightHand.PointerPose.position;
            Quaternion rightRotation = rightHand.PointerPose.rotation;
            Vector3 rightDirection = rightRotation * Vector3.forward;

            // Deform the mesh based on the position and direction of each hand
            DeformMesh(leftPosition, leftDirection, rightPosition, rightDirection);
        }
        else
        {
            // Reset the mesh to its original shape
            meshFilter.mesh.vertices = originalVertices;
        }
    }

    void DeformMesh(Vector3 leftPinchPosition, Vector3 leftPinchDirection, Vector3 rightPinchPosition, Vector3 rightPinchDirection)
    {

        // Deform each vertex based on its distance from each hand
        for (int i = 0; i < meshVerticies.Length; i++)
        {
            Vector3 vertexPosition = meshFilter.transform.TransformPoint(meshVerticies[i]);

            float leftDistanceToPinch = Vector3.Distance(vertexPosition, leftPinchPosition);
            float rightDistanceToPinch = Vector3.Distance(vertexPosition, rightPinchPosition);

            float deformationAmount = Mathf.Clamp01((1 - (leftDistanceToPinch + rightDistanceToPinch)) / deformationStrength);

            Vector3 leftDeformationDirection = Vector3.Cross(leftPinchDirection, vertexPosition - leftPinchPosition).normalized;
            Vector3 rightDeformationDirection = Vector3.Cross(rightPinchDirection, vertexPosition - rightPinchPosition).normalized;

            meshVerticies[i] += (leftDeformationDirection + rightDeformationDirection) * deformationAmount;
        }

        UpdateMeshVerticies();

    }

    void UpdateMeshVerticies()
    {
        // Set the deformed vertices and recalculate the normals
        meshFilter.mesh.vertices = meshVerticies;
        meshFilter.mesh.RecalculateNormals();
    }
}
