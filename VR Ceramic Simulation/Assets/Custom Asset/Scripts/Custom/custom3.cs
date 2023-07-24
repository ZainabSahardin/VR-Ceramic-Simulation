using UnityEngine;
using System.Collections;
using OculusSampleFramework;

public class custom3 : MonoBehaviour
{
    public OVRHand m_Lhand;
    public OVRHand m_Rhand;
    public float m_radius = 0.04f; // radius of the area around the hand where deformation occurs
    private MeshFilter m_meshFilter;
    private Vector3[] m_originalVertices;
    private Vector3[] m_deformedVertices;

    void start()
    {
        m_meshFilter = GetComponent<MeshFilter>();
        m_originalVertices = m_meshFilter.mesh.vertices;
        m_deformedVertices = m_meshFilter.mesh.vertices;
    }
    void update()
    {
        // Check if both hands are pinching
        bool leftPinching = m_Lhand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool rightPinching = m_Rhand.GetFingerIsPinching(OVRHand.HandFinger.Index);

        if (m_Lhand.IsTracked && m_Rhand.IsTracked)
        {
            Collider[] lColliders = Physics.OverlapSphere(m_Lhand.PointerPose.position, m_radius);
            Collider[] rColliders = Physics.OverlapSphere(m_Rhand.PointerPose.position, m_radius);
            inwardDeform(lColliders, m_Lhand);
            inwardDeform(rColliders, m_Rhand);

            // The Outward Deform procedure here, when pinched
            if (m_Lhand.GetFingerIsPinching(OVRHand.HandFinger.Index) && m_Rhand.GetFingerIsPinching(OVRHand.HandFinger.Index))
            {
                outwardDefrom(m_Lhand);
                outwardDefrom(m_Rhand);
            }

            // The fuction of update meshes
            UpdateMeshVerticies();
        }
    }
    void inwardDeform(Collider[] colliders, OVRHand m_hand)
    {
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == gameObject)
            {
                Vector3 m_touchPoint = transform.InverseTransformPoint(m_hand.PointerPose.position);
                // Calculate deformation based on normal direction
                for (int i = 0; i < m_originalVertices.Length; i++)
                {
                    Vector3 vertex = m_originalVertices[i];
                    Vector3 normal = transform.TransformDirection(m_meshFilter.mesh.normals[i]);

                    float distanceToTouchPoint = Vector3.Distance(vertex, m_touchPoint);
                    if (distanceToTouchPoint <= m_radius)
                    {
                        float deformationAmount = (m_radius - distanceToTouchPoint) * Time.deltaTime;
                        m_deformedVertices[i] = vertex - normal * deformationAmount;
                    }
                }
            }
        }
    }

    void outwardDefrom(OVRHand m_hand)
    {
        Vector3 m_touchPoint = transform.InverseTransformPoint(m_hand.PointerPose.position);
        // Calculate deformation based on normal direction
        for (int i = 0; i < m_originalVertices.Length; i++)
        {
            Vector3 vertex = m_originalVertices[i];
            Vector3 normal = transform.TransformDirection(m_meshFilter.mesh.normals[i]);

            float distanceToTouchPoint = Vector3.Distance(vertex, m_touchPoint);
            if (distanceToTouchPoint <= m_radius)
            {
                float deformationAmount = (m_radius - distanceToTouchPoint) * Time.deltaTime;
                m_deformedVertices[i] = vertex + normal * deformationAmount;
            }
        }
    }
    void UpdateMeshVerticies()
    {
        // Set the deformed vertices and recalculate the normals
        m_meshFilter.mesh.vertices = m_deformedVertices;
        m_meshFilter.mesh.RecalculateNormals();
    }
}
