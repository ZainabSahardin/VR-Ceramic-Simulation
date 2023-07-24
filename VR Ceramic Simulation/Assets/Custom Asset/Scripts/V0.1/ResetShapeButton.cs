using UnityEngine;
using UnityEngine.UI;

public class ResetShapeButton : MonoBehaviour
{
    // public GameObject objectToReset;
    public Button button;

    private void Start()
    {

        // Button button = GetComponent<Button>();
        button.onClick.AddListener(CallResetShape);
    }

    private void CallResetShape()
    {
        MeshManipulator meshM = new MeshManipulator();
        meshM.ResetShape();
    }
}
