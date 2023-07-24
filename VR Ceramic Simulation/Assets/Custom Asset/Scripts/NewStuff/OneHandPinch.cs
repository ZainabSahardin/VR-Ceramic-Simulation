using UnityEngine;
using OculusSampleFramework;

public class OneHandPinch : MonoBehaviour
{
    // Reference to OVRHand component for the hand
    public OVRHand hand;

    // Reference to the MeshFilter component
    public MeshFilter meshFilter;

    // Strength of deformation
    public float deformationStrength = 0.1f;

    private Vector3[] originalVertices;

    void Start()
    {
        // Check if hand reference and mesh filter reference are set
        if (hand == null)
        {
            Debug.LogError("Hand reference not set.");
        }
        if (meshFilter == null)
        {
            Debug.LogError("Mesh filter reference not set.");
        }

        // Get the original vertex positions of the mesh
        originalVertices = meshFilter.mesh.vertices;
    }

    void Update()
    {
        // Check if the hand is pinching
        bool isPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);

        if (isPinching)
        {
            // Get the position, rotation, and forward direction of the hand
            Vector3 handPosition = hand.PointerPose.position;
            Quaternion handRotation = hand.PointerPose.rotation;
            Vector3 handDirection = handRotation * Vector3.forward;

            // Deform the mesh based on the position and direction of the hand
            DeformMesh(handPosition, handDirection);
        }
        else
        {
            // Reset the mesh to its original shape
            meshFilter.mesh.vertices = originalVertices;
        }
    }

    void DeformMesh(Vector3 pinchPosition, Vector3 pinchDirection)
    {
        // Get the vertices of the mesh
        Vector3[] vertices = meshFilter.mesh.vertices;

        // Deform each vertex based on its distance from the hand
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertexPosition = meshFilter.transform.TransformPoint(vertices[i]);

            float distanceToPinch = Vector3.Distance(vertexPosition, pinchPosition);

            // Check if the hand is nearby the mesh before applying deformation
            if (distanceToPinch < 0.1f)
            {
                float deformationAmount = Mathf.Clamp01((1 - distanceToPinch) / deformationStrength);

                Vector3 deformationDirection = Vector3.Cross(pinchDirection, vertexPosition - pinchPosition).normalized;

                vertices[i] += deformationDirection * deformationAmount;
            }
        }

        // Set the deformed vertices and recalculate the normals
        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.RecalculateNormals();
    }
}