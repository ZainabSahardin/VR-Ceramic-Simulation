using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class custom1 : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;
    public OVRHand OVRLeftHand;
    public OVRHand OVRRightHand;
    public float radius = 0.05f;

    private MeshCollider coll;
    private MeshFilter meshFilter;
    private Vector3[] originalVertices;
    private Vector3[] meshVerticies;

    void Start()
    {
        coll = GetComponent<MeshCollider>();
        meshFilter = GetComponent<MeshFilter>();
        originalVertices = meshFilter.mesh.vertices;
        meshVerticies = meshFilter.mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        bool leftPinching = OVRLeftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool rightPinching = OVRRightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);

        // Define a sphere around the hand position with the given radius
        SphereCollider leftHandSphere = new SphereCollider();
        SphereCollider rightHandSphere = new SphereCollider();
        leftHandSphere.center = leftHand.position;
        rightHandSphere.center = rightHand.position;
        leftHandSphere.radius = radius;
        rightHandSphere.radius = radius;

        // Iterate over each vertex in the mesh and check if it intersects with the sphere

        if (leftPinching)
        {
            for (int i = 0; i < meshVerticies.Length; i++)
            {
                Vector3 vertexPosition = transform.TransformPoint(meshVerticies[i]);
                if (leftHandSphere.bounds.Contains(vertexPosition))
                {
                    meshVerticies[i] = leftHand.InverseTransformPoint(vertexPosition);

                    // Debug.Log("Vertex " + i + " intersects with the sphere!");
                }
            }
        }
        else if (rightPinching)
        {
            for (int i = 0; i < meshVerticies.Length; i++)
            {
                Vector3 vertexPosition = transform.TransformPoint(meshVerticies[i]);
                if (rightHandSphere.bounds.Contains(vertexPosition))
                {
                    meshVerticies[i] = rightHand.InverseTransformPoint(vertexPosition);

                    // Debug.Log("Vertex " + i + " intersects with the sphere!");
                }
            }
        }
        else
        {
            meshVerticies = originalVertices;
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
