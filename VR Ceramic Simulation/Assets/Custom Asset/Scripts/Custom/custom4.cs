using UnityEngine;
using System.Collections;
using OculusSampleFramework;

public class custom4 : MonoBehaviour
{
    public OVRHand m_hand1;
    public OVRHand m_hand2;
    public float m_radius = 0.05f; // radius of the area around the hand where deformation occurs
    private MeshFilter m_meshFilter;
    private Vector3[] m_originalVertices;
    private Vector3[] m_deformedVertices;
    // private bool m_isTouching1 = false;
    // private bool m_isTouching2 = false;
    private Vector3 m_touchPoint1;
    private Vector3 m_touchPoint2;

    void Start()
    {
        m_meshFilter = GetComponent<MeshFilter>();
        m_originalVertices = m_meshFilter.mesh.vertices;
        m_deformedVertices = new Vector3[m_originalVertices.Length];
        for (int i = 0; i < m_deformedVertices.Length; i++)
        {
            m_deformedVertices[i] = m_originalVertices[i];
        }
    }

    void Update()
    {
        if (m_hand1.IsTracked)
        {
            Collider[] colliders = Physics.OverlapSphere(m_hand1.PointerPose.position, m_radius);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject == gameObject)
                {
                    // m_isTouching1 = true;
                    m_touchPoint1 = transform.InverseTransformPoint(m_hand1.PointerPose.position);
                    // Calculate deformation based on normal direction
                    for (int i = 0; i < m_originalVertices.Length; i++)
                    {
                        Vector3 vertex = m_originalVertices[i];
                        Vector3 normal = transform.TransformDirection(m_meshFilter.mesh.normals[i]);
                        float distanceToTouchPoint = Vector3.Distance(vertex, m_touchPoint1);
                        if (distanceToTouchPoint <= m_radius)
                        {
                            float deformationAmount = (m_radius - distanceToTouchPoint) * Time.deltaTime;
                            if (m_hand1.GetFingerIsPinching(OVRHand.HandFinger.Index))
                            { // outward deformation when pinching
                                m_deformedVertices[i] = vertex + normal * deformationAmount;
                            }
                            else
                            { // inward deformation when touching
                                m_deformedVertices[i] = vertex - normal * deformationAmount;
                            }
                        }
                    }
                    break;
                }
            }
        }
        if (m_hand2.IsTracked)
        {
            Collider[] colliders = Physics.OverlapSphere(m_hand2.PointerPose.position, m_radius);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject == gameObject)
                {
                    // m_isTouching2 = true;
                    m_touchPoint2 = transform.InverseTransformPoint(m_hand2.PointerPose.position);
                    // Calculate deformation based on normal direction
                    for (int i = 0; i < m_originalVertices.Length; i++)
                    {
                        Vector3 vertex = m_originalVertices[i];
                        Vector3 normal = transform.TransformDirection(m_meshFilter.mesh.normals[i]);
                        float distanceToTouchPoint = Vector3.Distance(vertex, m_touchPoint2);
                        if (distanceToTouchPoint <= m_radius)
                        {
                            float deformationAmount = (m_radius - distanceToTouchPoint) * Time.deltaTime;
                            if (m_hand2.GetFingerIsPinching(OVRHand.HandFinger.Index))
                            { // outward deformation when pinching
                                m_deformedVertices[i] = vertex + normal * deformationAmount;
                            }
                            else
                            { // inward deformation when touching
                                m_deformedVertices[i] = vertex - normal * deformationAmount;
                            }
                        }
                    }
                    break;
                }
            }
        }
        // if (!m_isTouching1 && !m_isTouching2)
        // { // reset deformation when not touching
        //     for (int i = 0; i < m_deformedVertices.Length; i++)
        //     {
        //         m_deformedVertices[i] = m_originalVertices[i];
        //     }
        // }
        m_meshFilter.mesh.vertices = m_deformedVertices;
        m_meshFilter.mesh.RecalculateNormals();
    }
}
