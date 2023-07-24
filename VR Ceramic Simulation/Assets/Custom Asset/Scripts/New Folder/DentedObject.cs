using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DentedObject : MonoBehaviour {

    public FlexibleColorPicker fcp;
    public Material material;
    Renderer rend;

    public TextMeshProUGUI progressMessage;

    /*
    //To change texture after FIRING
    public float smoothnessDuration = 2f; // Duration for the texture change
    public float targetSmoothness = 0.65f; // Target value for the "_Smoothness" property
    */
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        
        GameObject.DontDestroyOnLoad(this.gameObject); //to bring the object to the next scene
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Glaze"))
        {
            StartCoroutine(ChangeColorWithDelay());
        }
    }

    private IEnumerator ChangeColorWithDelay()
    {
        yield return new WaitForSeconds(2f);

        if (rend != null)
        {
            rend.material.color = fcp.color;
        }
    }
   /*
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Fire"))
        {
            rend.material.SetFloat("_Smoothness", 0.65f); // Increase smoothness
        }
    }
    
    /*
    private void Start()
    {
        material = GetComponent<Renderer>().material;
        StartCoroutine(ChangeTextureSmoothly());
    }
    

    public void StartFiring()
    {
        StartCoroutine(ChangeTextureSmoothly());
    }

    private System.Collections.IEnumerator ChangeTextureSmoothly()
    {
        float currentSmoothness = material.GetFloat("_Smoothness");
        float elapsedTime = 0f;

        while (elapsedTime < smoothnessDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / smoothnessDuration);
            float smoothness = Mathf.Lerp(currentSmoothness, targetSmoothness, t);
            material.SetFloat("_Smoothness", smoothness);
            yield return null;
        }

        // Ensure the final value is set precisely
        material.SetFloat("_Smoothness", targetSmoothness);

        progressMessage.text = "FIring completed!";
    }

    /*
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Box")
        {
            //rend.sharedMaterial = material[1];
            rend.sharedMaterial = material;

            Debug.Log("Collision detected yeay");
        }
    }
    */
    //FUNCTION TO MAKE TEXTURE CHANGED - FOR FIRING
    /*
    public Material material;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Fire"))
        {
            rend.material.SetFloat("_Smoothness", 0.65f); // Increase smoothness to 1

            Debug.Log("Collision detected yeay");
        }
    }
    */
    /*
    public Material[] material;
    Renderer rend;

    void Start ()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }

    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.tag == "Box")
        {
            rend.sharedMaterial = material[1];
        }
        else
        {
            rend.sharedMaterial = material[2];
        }
    }
    
    public float dentAmount = 0.1f; // Amount to dent the object by
    public float dentRadius = 0.5f; // Radius of the affected area

    private MeshFilter meshFilter;
    private Mesh originalMesh;
    private Mesh deformedMesh;

    private void Start() {
        // Get the mesh filter component
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null) {
            Debug.LogError("DentedObject script requires a MeshFilter component.");
            return;
        }

        // Make a copy of the original mesh
        originalMesh = meshFilter.mesh;
        deformedMesh = Instantiate(originalMesh);

        // Set the new mesh as the object's mesh
        meshFilter.mesh = deformedMesh;
    }

    private void OnCollisionEnter(Collision collision) {
        // Check if the collision is within the dent radius
        foreach (ContactPoint contact in collision.contacts) {
            if (Vector3.Distance(contact.point, transform.position) <= dentRadius) {
                // Calculate the dent position and normal
                Vector3 dentPosition = contact.point - transform.position;
                Vector3 dentNormal = contact.normal;

                // Deform the mesh locally at the dent position
                for (int i = 0; i < deformedMesh.vertices.Length; i++) {
                    Vector3 vertexPosition = deformedMesh.vertices[i];
                    float distanceToDent = Vector3.Distance(vertexPosition, dentPosition);
                    if (distanceToDent <= dentRadius) {
                        float dentFactor = 1 - (distanceToDent / dentRadius);
                        vertexPosition += dentNormal * dentFactor * dentAmount;
                        deformedMesh.vertices[i] = vertexPosition;
                    }
                }

                // Update the mesh's normals and bounds
                deformedMesh.RecalculateNormals();
                deformedMesh.RecalculateBounds();
            }
        }
    }
    */
    /*
    public float dentAmount = 0.1f; // Amount to dent the object by
    public float dentRadius = 0.5f; // Radius of the affected area
    public Color dentColor = Color.black; // Color to change the object to when dented

    private MeshFilter meshFilter;
    private Mesh originalMesh;
    private Mesh deformedMesh;
    private Material originalMaterial;
    private Renderer objectRenderer;

    private void Start()
    {
        // Get the mesh filter component
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("DentedObject script requires a MeshFilter component.");
            return;
        }

        // Get the object's renderer and store the original material
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }
        else
        {
            Debug.LogError("DentedObject script requires a Renderer component.");
            return;
        }

        // Make a copy of the original mesh
        originalMesh = meshFilter.mesh;
        deformedMesh = Instantiate(originalMesh);

        // Set the new mesh as the object's mesh
        meshFilter.mesh = deformedMesh;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is within the dent radius
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Distance(contact.point, transform.position) <= dentRadius)
            {
                // Calculate the dent position and normal
                Vector3 dentPosition = contact.point - transform.position;
                Vector3 dentNormal = contact.normal;

                // Deform the mesh locally at the dent position
                for (int i = 0; i < deformedMesh.vertices.Length; i++)
                {
                    Vector3 vertexPosition = deformedMesh.vertices[i];
                    float distanceToDent = Vector3.Distance(vertexPosition, dentPosition);
                    if (distanceToDent <= dentRadius)
                    {
                        float dentFactor = 1 - (distanceToDent / dentRadius);
                        vertexPosition += dentNormal * dentFactor * dentAmount;
                        deformedMesh.vertices[i] = vertexPosition;
                    }
                }

                // Update the mesh's normals and bounds
                deformedMesh.RecalculateNormals();
                deformedMesh.RecalculateBounds();

                // Change the object's color
                if (objectRenderer != null)
                {
                    objectRenderer.material.color = dentColor;
                }
            }
        }
    }
    */
}
