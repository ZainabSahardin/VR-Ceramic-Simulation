using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialColor : MonoBehaviour
{
    public Material[] material;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }

    public void changeDoor()
    {
        if (rend.sharedMaterial != material[0])
        {
            rend.sharedMaterial = material[0];
        }
        else
        {
            rend.sharedMaterial = material[1];
        }
    }
    /*
    public Material[] material;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Glaze")
        {
            rend.sharedMaterial = material[1];
        }
        else
        {
            rend.sharedMaterial = material[2];
        }
    }
    */

}
