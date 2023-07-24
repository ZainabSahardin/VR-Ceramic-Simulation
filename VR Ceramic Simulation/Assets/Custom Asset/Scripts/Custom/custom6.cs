using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class custom6 : MonoBehaviour
{
    public OVRHand leftHand;
    public OVRHand rightHand;
    public float radius = 0.05f;
    private MeshFilter meshFilter;
    private Vector3[] originalVertices;
    private Vector3[] meshVertices;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        originalVertices = meshFilter.mesh.vertices;
        meshVertices = meshFilter.mesh.vertices;
    }

    void Update()
    {
        bool leftPinching = leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool rightPinching = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);

        if (leftPinching && rightPinching)
        {
            Vector3 leftPosition = leftHand.PointerPose.position;
            Vector3 rightPosition = rightHand.PointerPose.position;

            Vector3[] normals = meshFilter.mesh.normals;
            Vector3[] vertices = meshFilter.mesh.vertices;

            // Deform the object mesh based on the position and direction of the mesh normal.
            // The deformations area of the mesh is determined by radius variable
            // The mesh deform inward follow its normal direction.

            for (int i = 0; i < vertices.Length; i++)
            {
                float distanceToLeft = Vector3.Distance(vertices[i], leftPosition);
                float distanceToRight = Vector3.Distance(vertices[i], rightPosition);

                if (distanceToLeft < radius && distanceToRight < radius)
                {
                    float deformAmount = (radius - Mathf.Max(distanceToLeft, distanceToRight)) / radius;
                    meshVertices[i] -= normals[i] * deformAmount;
                }
            }

            UpdateMeshVertices();
        }
        // else
        // {
        //     meshFilter.mesh.vertices = originalVertices;
        // }
    }

    void UpdateMeshVertices()
    {
        meshFilter.mesh.vertices = meshVertices;
        meshFilter.mesh.RecalculateNormals();
    }
}
