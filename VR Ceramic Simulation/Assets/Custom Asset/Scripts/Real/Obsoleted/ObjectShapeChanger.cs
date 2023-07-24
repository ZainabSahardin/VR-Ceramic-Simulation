using UnityEngine;
using OculusSampleFramework;

public class ObjectShapeChanger : MonoBehaviour
{   
    public OVRHand LeftHand;
    public OVRHand RightHand;
    private OVRHand[] hands = new OVRHand[2];
    [SerializeField] private float pinchThreshold = 0.7f;
    [SerializeField] private Mesh[] shapes;
    private int currentShapeIndex = 0;
    private MeshRenderer meshRenderer;

    void Start()
    {
        hands[0] = LeftHand;
        hands[1] = RightHand;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        foreach (var hand in hands)
        {
            if (hand.GetFingerIsPinching(OVRHand.HandFinger.Index) && hand.GetFingerIsPinching(OVRHand.HandFinger.Thumb))
            {
                if (hand.GetFingerPinchStrength(OVRHand.HandFinger.Index) > pinchThreshold && hand.GetFingerPinchStrength(OVRHand.HandFinger.Thumb) > pinchThreshold)
                {
                    ChangeShape();
                }
            }
        }
    }

    private void ChangeShape()
    {
        currentShapeIndex = (currentShapeIndex + 1) % shapes.Length;
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            meshFilter.mesh = shapes[currentShapeIndex];
            meshRenderer.material = new Material(Shader.Find("Standard"));
        }
    }
}
