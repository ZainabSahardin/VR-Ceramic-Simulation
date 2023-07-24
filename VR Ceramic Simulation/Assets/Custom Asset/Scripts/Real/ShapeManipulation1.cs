using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManipulation1 : MonoBehaviour
{
    [Range(0, 10)]
    public float deformRadius = 0.2f;
    [Range(0, 10)]
    public float maxDeform = 0.1f;
    public float minForce = 0.1f;
    private MeshFilter filter;
    private MeshCollider coll;
    private Vector3[] startingVerticies;
    private Vector3[] meshVerticies;

    void Start()
    {
        filter = GetComponent<MeshFilter>();

        if (GetComponent<MeshCollider>())
            coll = GetComponent<MeshCollider>();

        startingVerticies = filter.mesh.vertices;
        meshVerticies = filter.mesh.vertices;
    }

    void OnCollisionEnter(Collision collision)
    {
        float collisionForce = collision.impulse.magnitude;

        if (collisionForce > minForce)
        {
            foreach (ContactPoint point in collision.contacts)
            {
                for (int i = 0; i < meshVerticies.Length; i++)
                {
                    Vector3 vertexPosition = meshVerticies[i];
                    Vector3 pointPosition = transform.InverseTransformPoint(point.point);
                    float distanceFromCollision = Vector3.Distance(vertexPosition, pointPosition);
                    float distanceFromOriginal = Vector3.Distance(startingVerticies[i], vertexPosition);

                    if (distanceFromCollision < deformRadius && distanceFromOriginal < maxDeform) // If within collision radius and within max deform
                    {
                        float falloff = 1 - (distanceFromCollision / deformRadius) * 1;

                        float xDeform = pointPosition.x * falloff;
                        float yDeform = pointPosition.y * falloff;
                        float zDeform = pointPosition.z * falloff;

                        xDeform = Mathf.Clamp(xDeform, 0, maxDeform);
                        yDeform = Mathf.Clamp(yDeform, 0, maxDeform);
                        zDeform = Mathf.Clamp(zDeform, 0, maxDeform);

                        Vector3 deform = new Vector3(xDeform, yDeform, zDeform);
                        meshVerticies[i] -= deform * 1;
                    }
                }
            }
            UpdateMeshVerticies();
        }
    }
    void UpdateMeshVerticies()
    {
        filter.mesh.vertices = meshVerticies;
        coll.sharedMesh = filter.mesh;
    }
}
