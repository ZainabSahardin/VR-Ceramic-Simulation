using UnityEngine;
using System.Collections.Generic;

public class MeshDeformer : MonoBehaviour
{
    public OVRHand leftHand;
    public OVRHand rightHand;
    public float deformSpeed = 0.1f;
    public float maxDeformDistance = 0.1f;

    private Mesh mesh;
    private Vector3[] originalVertices;
    private List<int>[] vertexTriangles;
    private bool[] verticesMoved;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        vertexTriangles = new List<int>[originalVertices.Length];
        verticesMoved = new bool[originalVertices.Length];

        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            int i0 = mesh.triangles[i];
            int i1 = mesh.triangles[i + 1];
            int i2 = mesh.triangles[i + 2];

            if (vertexTriangles[i0] == null) vertexTriangles[i0] = new List<int>();
            if (vertexTriangles[i1] == null) vertexTriangles[i1] = new List<int>();
            if (vertexTriangles[i2] == null) vertexTriangles[i2] = new List<int>();

            vertexTriangles[i0].Add(i);
            vertexTriangles[i1].Add(i);
            vertexTriangles[i2].Add(i);
        }
    }

    void Update()
    {
        DeformMesh(leftHand, true);
        DeformMesh(rightHand, false);
    }

    void DeformMesh(OVRHand hand, bool leftHand)
    {
        if (hand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {
            Vector3 pinchPosition = hand.PointerPose.position;
            Vector3 localPinchPosition = transform.InverseTransformPoint(pinchPosition);

            for (int i = 0; i < originalVertices.Length; i++)
            {
                if (verticesMoved[i]) continue;

                Vector3 vertexPosition = transform.TransformPoint(originalVertices[i]);
                float distance = Vector3.Distance(vertexPosition, pinchPosition);

                if (distance < maxDeformDistance)
                {
                    float deformAmount = (maxDeformDistance - distance) * deformSpeed;

                    if (leftHand)
                    {
                        if (localPinchPosition.x > 0)
                        {
                            Vector3 deformedPosition = originalVertices[i] + Vector3.right * deformAmount;
                            mesh.vertices[i] = deformedPosition;
                            verticesMoved[i] = true;
                        }
                    }
                    else
                    {
                        if (localPinchPosition.x < 0)
                        {
                            Vector3 deformedPosition = originalVertices[i] + Vector3.left * deformAmount;
                            mesh.vertices[i] = deformedPosition;
                            verticesMoved[i] = true;
                        }
                    }

                    List<int> triangles = vertexTriangles[i];
                    for (int j = 0; j < triangles.Count; j++)
                    {
                        int triangleIndex = triangles[j];
                        int i0 = mesh.triangles[triangleIndex];
                        int i1 = mesh.triangles[triangleIndex + 1];
                        int i2 = mesh.triangles[triangleIndex + 2];

                        if (verticesMoved[i0] && verticesMoved[i1] && verticesMoved[i2]) continue;

                        Vector3 v0 = transform.TransformPoint(mesh.vertices[i0]);
                        Vector3 v1 = transform.TransformPoint(mesh.vertices[i1]);
                        Vector3 v2 = transform.TransformPoint(mesh.vertices[i2]);

                        Vector3 normal = Vector3.Cross(v1 - v0, v2 - v0).normalized;
                        float dot = Vector3.Dot(normal, pinchPosition - v0);

                        if (dot > 0)
                        {
                            Vector3 deformedPosition = mesh.vertices[i] + normal * (deformAmount / 2f);
                            mesh.vertices[i0] = deformedPosition;
                            mesh.vertices[i1] = deformedPosition;
                            mesh.vertices[i2] = deformedPosition;

                            verticesMoved[i0] = true;
                            verticesMoved[i1] = true;
                            verticesMoved[i2] = true;
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < originalVertices.Length; i++)
            {
                if (verticesMoved[i])
                {
                    mesh.vertices[i] = originalVertices[i];
                    verticesMoved[i] = false;
                }
            }
        }

        mesh.RecalculateNormals();
    }
}
