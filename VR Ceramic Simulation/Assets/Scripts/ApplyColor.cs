using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyColor : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public Material material;
    public Renderer targetRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        material.color = fcp.color;
    }

    // To change color of the pottery once button clicked
    public void ChangeColorOfAnotherObject()
    {
        StartCoroutine(ChangeColorWithDelay());
    }

    // Function called to change the color after 2 seconds of delay
    private IEnumerator ChangeColorWithDelay()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        if (targetRenderer != null)
        {
            targetRenderer.material.color = fcp.color;
        }
    }
}
