using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class custom5 : MonoBehaviour
{
    public OVRHand leftHand;
    public OVRHand rightHand;
    private MeshFilter meshFilter;
    private Vector3[] originalVertices;
    private Vector3[] meshVerticies;

    // Start is called before the first frame update
    void Start()
    {
        // Get the original vertex positions of the mesh
        meshFilter = GetComponent<MeshFilter>();
        originalVertices = meshFilter.mesh.vertices;
        meshVerticies = meshFilter.mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if both hands are pinching
        bool leftPinching = leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool rightPinching = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);

        if (leftPinching && rightPinching)
        {
            // Get the position of each hand
            Vector3 leftPosition = leftHand.PointerPose.position;
            Vector3 rightPosition = rightHand.PointerPose.position;

            // Get the normal of the Mesh
            Vector3[] normal = meshFilter.mesh.normals;

            // Get the vertices of the Mesh
            Vector3[] vertices = meshFilter.mesh.vertices;

            // Deform the mesh based on the position and direction of each hand
        }
        else
        {
            // Reset the mesh to its original shape
            meshFilter.mesh.vertices = originalVertices;
        }
    }
    void UpdateMeshVerticies()
    {
        // Set the deformed vertices and recalculate the normals
        meshFilter.mesh.vertices = meshVerticies;
        meshFilter.mesh.RecalculateNormals();
    }
}
