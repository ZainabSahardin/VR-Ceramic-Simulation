using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlazingFiring : MonoBehaviour
{
    // GENERAL
    Material material;
    Renderer rend;

    private static GlazingFiring Instance;

    // FOR GLAZING
    public FlexibleColorPicker fcp;
    

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject); //to bring the object to the next scene
    }

    // To put on a tray
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Tray"))
        {
            rend.transform.SetParent(col.gameObject.transform, true);
        }
    }

    // To detect glazing and firing zone
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Glaze"))
        {
            StartCoroutine(ChangeColorWithDelay());
        }
        else if (other.gameObject.CompareTag("Fire"))
        {
            StartCoroutine(FiringWithDelay());
        }
    }

    // To change object's color
    private IEnumerator ChangeColorWithDelay()
    {
        yield return new WaitForSeconds(2f);

        if (rend != null)
        {
            rend.material.color = fcp.color;
        }
    }

    // To change object's texture
    private IEnumerator FiringWithDelay()
    {
        yield return new WaitForSeconds(5f);

        if (rend != null)
        {
            rend.material.SetFloat("_Smoothness", 0.65f); // Increase smoothness
        }
    }
}
