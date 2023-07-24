using UnityEngine;
using UnityEngine.XR;

public class HandTrackingObject : MonoBehaviour
{
    [SerializeField] private GameObject handObject = null;
    [SerializeField] private MeshFilter meshFilter = null;
    [SerializeField] private float touchRadius = 0.1f;
    [SerializeField] private float touchStrength = 0.5f;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private Vector3 lastPosition;

    private void Awake()
    {
        mesh = new Mesh();
        meshFilter.mesh = mesh;

        lastPosition = handObject.transform.position;
    }

    private void Update()
    {
        if (handObject != null && handObject.activeSelf)
        {
            XRNodeState handState = new XRNodeState();
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 handPosition);
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion handRotation);

            handState.TryGetPosition(out handPosition); // get the real position and rotation from the hand bone transform

            Vector3 currentPosition = handPosition;
            Vector3 deltaPosition = currentPosition - lastPosition;

            if (deltaPosition.magnitude > touchRadius)
            {
                int vertexIndex = AddVertex(currentPosition);
                AddTriangle(vertexIndex - 1, vertexIndex, 0);

                mesh.vertices = vertices;
                mesh.triangles = triangles;

                lastPosition = currentPosition;
            }
        }
    }

    private int AddVertex(Vector3 position)
    {
        int index = vertices.Length;
        Vector3[] newVertices = new Vector3[index + 1];
        vertices.CopyTo(newVertices, 0);
        newVertices[index] = position * touchStrength;
        vertices = newVertices;
        return index;
    }

    private void AddTriangle(int a, int b, int c)
    {
        int index = triangles.Length;
        int[] newTriangles = new int[index + 3];
        triangles.CopyTo(newTriangles, 0);
        newTriangles[index] = a;
        newTriangles[index + 1] = b;
        newTriangles[index + 2] = c;
        triangles = newTriangles;
    }
}
