using UnityEngine;

public class CollisionDebugChange : MonoBehaviour
{
    MeshFilter meshFilter;
    Vector3[] vertices;
    int[] triangles;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        vertices = meshFilter.sharedMesh.vertices;
        triangles = meshFilter.sharedMesh.triangles;
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.thisCollider.gameObject == gameObject)
            {
                for (int i = 0; i < triangles.Length; i += 3)
                {
                    Vector3 vertex1 = transform.TransformPoint(vertices[triangles[i]]);
                    Vector3 vertex2 = transform.TransformPoint(vertices[triangles[i + 1]]);
                    Vector3 vertex3 = transform.TransformPoint(vertices[triangles[i + 2]]);

                    if (IsInsideTriangle(vertex1, vertex2, vertex3, contact.point))
                    {
                        Debug.DrawLine(vertex1, vertex2, Color.red, 5f);
                        Debug.DrawLine(vertex2, vertex3, Color.red, 5f);
                        Debug.DrawLine(vertex3, vertex1, Color.red, 5f);
                    }
                }
            }
        }
    }

    bool IsInsideTriangle(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 point)
    {
        float d1, d2, d3;
        bool has_neg, has_pos;

        d1 = Sign(point, v1, v2);
        d2 = Sign(point, v2, v3);
        d3 = Sign(point, v3, v1);

        has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

        return !(has_neg && has_pos);
    }

    float Sign(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
    }
}
