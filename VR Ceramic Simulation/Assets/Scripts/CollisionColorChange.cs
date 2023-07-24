using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionColorChange : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public Material material;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    void Update()
    {
        material.color = fcp.color;
    }
/*
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Glaze"))
        {
            StartCoroutine(ChangeColorWithDelay());
        }
    }

        private IEnumerator ChangeColorWithDelay()
    {
        //yield return new WaitForSeconds(2f);

        //rend.sharedMaterial = material;

        yield return new WaitForSeconds(2f);

        if (rend != null)
        {
            rend.material.color = fcp.color;
        }
    }
    */
    /*
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Glaze")
        {
            StartCoroutine(ChangeColorWithDelay());
        }
    }
    */
}
