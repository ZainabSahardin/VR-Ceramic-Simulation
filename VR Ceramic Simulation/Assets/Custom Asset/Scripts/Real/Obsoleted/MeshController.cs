using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    public float radius;

    public float deformationStrength;

    private Mesh mesh;

    private Vector3[] vertices, modifiedVert;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        modifiedVert = mesh.vertices;
    }
    
    void RecalculateMesh() 
    {
        mesh.vertices = modifiedVert;
        GetComponentInChildren<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            for (int v = 0; v < modifiedVert.Length; v++)
            {
                Vector3 distance = modifiedVert[v] - hit.point;

                float smoothingFactor = 2f;
                float force = deformationStrength / (1f + hit.point.sqrMagnitude);

                if (distance.sqrMagnitude < radius)
                {
                    if (Input.GetMouseButton(0))
                    {
                        modifiedVert[v] = modifiedVert[v] + (Vector3.right * force) / smoothingFactor;
                    }
                    else if (Input.GetMouseButton(1))
                    {
                        modifiedVert[v] = modifiedVert[v] + (Vector3.left * force) / smoothingFactor;
                    }
                }
            }
        }

        RecalculateMesh();
    }
}
