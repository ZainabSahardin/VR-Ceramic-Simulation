using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyFiring : MonoBehaviour
{
    Material material;
    public Renderer targetRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // To change the texture of the pottery once button clicked
    public void ChangeTextureOfAnotherObject()
    {
        StartCoroutine(ChangeTextureWithDelay());
    }

    // Function called to change the color after 2 seconds of delay
    private IEnumerator ChangeTextureWithDelay()
    {
        yield return new WaitForSeconds(5f); // Wait for 2 seconds

        if (targetRenderer != null)
        {
            targetRenderer.material.SetFloat("_Smoothness", 0.65f); // Increase smoothness
        }
    }
}
