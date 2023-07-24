using UnityEngine;

public class CollisionDebug : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.Log("Collision at point: " + contact.point);
            
            MeshFilter meshFilter = contact.thisCollider.gameObject.GetComponent<MeshFilter>();
            if(meshFilter != null)
            {
                Vector3[] vertices = meshFilter.sharedMesh.vertices;
                
                for(int i = 0; i < vertices.Length; i++)
                {
                    if(Vector3.Distance(vertices[i], contact.point) < 0.01f)
                    {
                        Debug.DrawRay(vertices[i], Vector3.up * 0.1f, Color.red, 5f);
                    }
                }
            }
        }
    }
}
