using System.Collections.Generic;
using UnityEngine;

public class ProceduralMesh1 : MonoBehaviour
{

    public float radius = 0.05f;
    public float smoothness = 0.2f;
    public float elasticity = 0.5f;
    public float deformation = 0.05f;

    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private Mesh mesh;
    private Vector3[] originalVertices;

    public GameObject leftHandObject;
    public GameObject rightHandObject;

    private Transform leftHandTransform;
    private Transform rightHandTransform;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        for (int i = 0; i < originalVertices.Length; i++)
        {
            vertices.Add(originalVertices[i]);
        }
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            triangles.Add(mesh.triangles[i]);
            triangles.Add(mesh.triangles[i + 1]);
            triangles.Add(mesh.triangles[i + 2]);
        }
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.MarkDynamic();

        if (leftHandObject != null)
        {
            leftHandTransform = leftHandObject.transform;
        }
        if (rightHandObject != null)
        {
            rightHandTransform = rightHandObject.transform;
        }
    }

    void Update()
    {
        bool leftHandFound = false;
        bool rightHandFound = false;

        if (leftHandTransform != null)
        {
            Vector3 position = transform.position;
            float distance = Vector3.Distance(leftHandTransform.position, position);
            if (distance < radius)
            {
                leftHandFound = true;
                float deformationAmount = (radius - distance) / radius;
                float smoothnessAmount = Mathf.Lerp(1f, smoothness, deformationAmount);
                Vector3 moldDirection = leftHandTransform.position - position;
                for (int i = 0; i < vertices.Count; i++)
                {
                    Vector3 vertex = vertices[i];
                    float vertexDistance = Vector3.Distance(vertex, position);
                    if (vertexDistance < radius)
                    {
                        float elasticAmount = (radius - vertexDistance) / radius;
                        Vector3 deformationVector = moldDirection.normalized * deformation * elasticAmount;
                        deformationVector = Vector3.Lerp(deformationVector, Vector3.zero, smoothnessAmount);
                        vertices[i] = originalVertices[i] + deformationVector * elasticity;
                    }
                }
                mesh.vertices = vertices.ToArray();
                mesh.RecalculateNormals();
                mesh.RecalculateBounds();
                mesh.MarkDynamic();
            }
        }

        if (rightHandTransform != null)
        {
            Vector3 position = transform.position;
            float distance = Vector3.Distance(rightHandTransform.position, position);
            if (distance < radius)
            {
                rightHandFound = true;
                float deformationAmount = (radius - distance) / radius;
                float smoothnessAmount = Mathf.Lerp(1f, smoothness, deformationAmount);
                Vector3 moldDirection = rightHandTransform.position - position;
                for (int i = 0; i < vertices.Count; i++)
                {
                    Vector3 vertex = vertices[i];
                    float vertexDistance = Vector3.Distance(vertex, position);
                    if (vertexDistance < radius)
                    {
                        float elasticAmount = (radius - vertexDistance) / radius;
                        Vector3 deformationVector = moldDirection.normalized * deformation * elasticAmount;
                        deformationVector = Vector3.Lerp(deformationVector, Vector3.zero, smoothnessAmount);
                        vertices[i] = originalVertices[i] + deformationVector * elasticity;
                    }
                }
                mesh.vertices = vertices.ToArray();
                mesh.RecalculateNormals();
                mesh.RecalculateBounds();
                mesh.MarkDynamic();
            }
        }
        if (!leftHandFound && !rightHandFound)
        {
        return;
        }
    }
}