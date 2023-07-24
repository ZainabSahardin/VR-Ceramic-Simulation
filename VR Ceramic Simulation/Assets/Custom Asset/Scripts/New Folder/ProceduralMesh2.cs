using System.Collections.Generic;
using UnityEngine;

public class ProceduralMesh2 : MonoBehaviour
{
    // Declare public variables to configure the deformation of the mesh
    public float radius = 0.05f; // The radius within which hand objects influence the mesh
    public float smoothness = 0.2f; // Determines how quickly the mesh returns to its original shape
    public float elasticity = 0.5f; // The amount by which vertices are deformed
    public float deformation = 0.05f; // The amount of deformation applied

    // Private member variables
    private List<Vector3> vertices = new List<Vector3>(); // A list of vertices that define the mesh
    private List<int> triangles = new List<int>(); // A list of triangles that connect the vertices
    private Mesh mesh; // A reference to the Mesh component attached to this GameObject
    private Vector3[] originalVertices; // An array of the original vertices used as a base for deformation

    // Public variables to assign left and right hand objects
    public GameObject leftHandObject; 
    public GameObject rightHandObject;

    // Private variables to store references to the hand Transform components
    private Transform leftHandTransform;
    private Transform rightHandTransform;

    void Start()
    {
        // Get a reference to the Mesh component and the original vertices
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;

        // Copy the original vertices and triangles to the respective lists
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

        // Clear the mesh and set the vertices and triangles to the copied lists
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        // Recalculate the normals and bounds of the mesh and mark it as dynamic
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.MarkDynamic();

        // Assign the Transform components of the left and right hand objects if they exist
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
        // Initialize hand found flags
        bool leftHandFound = false;
        bool rightHandFound = false;

        // Deform the mesh based on the position of the left hand object
        if (leftHandTransform != null)
        {
            Vector3 position = transform.position;
            float distance = Vector3.Distance(leftHandTransform.position, position);
            if (distance < radius) // Check if the hand is within range to affect the mesh
            {
                leftHandFound = true;
                // Calculate the amount of deformation and smoothness
                float deformationAmount = (radius - distance) / radius;
                float smoothnessAmount = Mathf.Lerp(1f, smoothness, deformationAmount);
                // Calculate the direction in which to mold the mesh
                Vector3 moldDirection = leftHandTransform.position - position;
                // Loop through each vertex in the mesh and apply deformation
                for (int i = 0; i < vertices.Count; i++)
                {
                    Vector3 vertex = vertices[i];
                    float vertexDistance = Vector3.Distance(vertex, position);
                    if (vertexDistance < radius)
                    {
                        // Calculate the amount of elasticity to apply
                        float elasticAmount = (radius - vertexDistance) / radius;
                        // Calculate the deformation vector and smooth it
                        Vector3 deformationVector = moldDirection.normalized * deformation * elasticAmount;
                        deformationVector = Vector3.Lerp(deformationVector, Vector3.zero, smoothnessAmount);
                        // Apply the deformation to the vertex
                        vertices[i] = originalVertices[i] + deformationVector * elasticity;
                    }
                }
                // Set the mesh vertices to the updated values and recalculate the normals and bounds
                mesh.vertices = vertices.ToArray();
                mesh.RecalculateNormals();
                mesh.RecalculateBounds();
                mesh.MarkDynamic();
            }
        }

        // Deform the mesh based on the position of the right hand object
        if (rightHandTransform != null)
        {
            Vector3 position = transform.position;
            float distance = Vector3.Distance(rightHandTransform.position, position);
            if (distance < radius) // Check if the hand is within range to affect the mesh
            {
                rightHandFound = true;
                // Calculate the amount of deformation and smoothness
            float deformationAmount = (radius - distance) / radius;
            float smoothnessAmount = Mathf.Lerp(1f, smoothness, deformationAmount);
            // Calculate the direction in which to mold the mesh
            Vector3 moldDirection = rightHandTransform.position - position;
            // Loop through each vertex in the mesh and apply deformation
            for (int i = 0; i < vertices.Count; i++)
            {
                Vector3 vertex = vertices[i];
                float vertexDistance = Vector3.Distance(vertex, position);
                if (vertexDistance < radius)
                {
                    // Calculate the amount of elasticity to apply
                    float elasticAmount = (radius - vertexDistance) / radius;
                    // Calculate the deformation vector and smooth it
                    Vector3 deformationVector = moldDirection.normalized * deformation * elasticAmount;
                    deformationVector = Vector3.Lerp(deformationVector, Vector3.zero, smoothnessAmount);
                    // Apply the deformation to the vertex
                    vertices[i] = originalVertices[i] + deformationVector * elasticity;
                }
            }
            // Set the mesh vertices to the updated values and recalculate the normals and bounds
            mesh.vertices = vertices.ToArray();
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mesh.MarkDynamic();
        }
    }

    // If no hand object was found, return from the method
    if (!leftHandFound && !rightHandFound)
    {
        return;
    }
}
}