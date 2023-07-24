using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    public Material[] GlazeColorMat; // Array of materials for different colors of glaze paint
    public Material[] PotColorMat; // Array of materials for different colors of pot
    public Renderer glazeRenderer; // Reference to the glaze object's renderer component
    public Renderer potRenderer; // Reference to the pot object's renderer component
    Material CurrMat;

    private int ColorIndex = 0; // Index of the current glaze color
    private bool isDelayed = false; // Flag to track if delay is in progress

    private void Start()
    {
        //glazeRenderer = this.GetComponent<Renderer>();
        potRenderer = this.GetComponent<Renderer>();

        if (potRenderer != null && PotColorMat.Length > 0)
        {
            //glazeRenderer.material = GlazeColorMat[ColorIndex];
            potRenderer.material = PotColorMat[ColorIndex];
            CurrMat = potRenderer.material;
        }
    }

    public void ChangePotColor(int colorIndex)
    {
        // Check if delay is in progress
        if (isDelayed)
        {
            // Wait for 2 seconds
            if (Time.time >= 2f)
            {
                if (colorIndex < PotColorMat.Length)
                {
                    potRenderer.material = PotColorMat[colorIndex];
                    CurrMat = potRenderer.material;
                }
                // Change the material/color after the delay
                // objectRenderer.material = PotColorMat[currentIndex];

                isDelayed = false; // Reset the delay flag
            }
        }
        /*
        // Update the glaze color based on the colorIndex
        if (potRenderer != null && colorIndex >= 0 && colorIndex < PotColorMat.Length)
        {
            ColorIndex = colorIndex;
            potRenderer.material = PotColorMat[colorIndex];
            CurrMat = potRenderer.material;
        }
        */
    }
    /*
    private void Start()
    {
        glazeRenderer = this.GetComponent<Renderer>();
        potRenderer = this.GetComponent<Renderer>();

        if (potRenderer != null && glazeRenderer != null && PotColorMat.Length > 0 && GlazeColorMat.Length > 0)
        {
            glazeRenderer.material = GlazeColorMat[ColorIndex];
            potRenderer.material = PotColorMat[ColorIndex];
            CurrMat = glazeRenderer.material;
        }
    }

    public void ChangeGlazeColor(int colorIndex)
    {
        // Update the glaze color based on the colorIndex
        if (glazeRenderer != null && colorIndex >= 0 && colorIndex < GlazeColorMat.Length)
        {
            ColorIndex = colorIndex;
            glazeRenderer.material = GlazeColorMat[colorIndex];
            CurrMat = glazeRenderer.material;
        }
    }

    public void ChangePotColor()
    {
        if (!isDelayed)
        {
            // Start the delay
            isDelayed = true;

            // Set the pot renderer material to PotColorMat[ColorIndex] immediately
            if (ColorIndex < PotColorMat.Length)
            {
                potRenderer.material = PotColorMat[ColorIndex];
                CurrMat = potRenderer.material;
            }
        }
    }

    **************************
    public void ChangePotColor(int ColorIndex)
    {
        // Update the pot color based on the potColorIndex
        if (potRenderer != null && ColorIndex >= 0 && ColorIndex < PotColorMat.Length)
        {
            this.ColorIndex = ColorIndex;
            potRenderer.material = PotColorMat[ColorIndex];
            CurrMat = potRenderer.material;
        }
    }
    */
    // Define functions for other color options
    public void BlueColor()
    {
        glazeRenderer.material = GlazeColorMat[1];
        CurrMat = glazeRenderer.material;
        ChangePotColor(1); // Pass the color index for blue
    }

    public void GreyColor()
    {
        glazeRenderer.material = GlazeColorMat[2];
        CurrMat = glazeRenderer.material;
        ChangePotColor(2); // Pass the color index for blue
        //ChangeGlazeColor(2); // Pass the color index for grey
    }

    public void OrangeishColor()
    {
        glazeRenderer.material = GlazeColorMat[3];
        CurrMat = glazeRenderer.material;
        ChangePotColor(3); // Pass the color index for blue
        //ChangeGlazeColor(3); // Pass the color index for grey
    }

    public void WhiteColor()
    {
        glazeRenderer.material = GlazeColorMat[4];
        CurrMat = glazeRenderer.material;
        ChangePotColor(4); // Pass the color index for blue
        //ChangeGlazeColor(4); // Pass the color index for grey
    }


    /*

    // Render blue color
    public void BlueColor()
    {
        glazeRenderer.material = GlazeColorMat[1];
        currentIndex = 1;
        CurrMat = glazeRenderer.material;
        ChangePotColor(currentIndex);
    }

    // Render grey color
    public void GreyColor()
    {
        glazeRenderer.material = GlazeColorMat[2];
        currentIndex = 2;
        CurrMat = glazeRenderer.material;
        ChangePotColor(currentIndex);
    }

    //render orangeish color
    public void OrangeishColor()
    {
        glazeRenderer.material = GlazeColorMat[3];
        currentIndex = 3;
        CurrMat = glazeRenderer.material;
        ChangePotColor(currentIndex);
    }

    //render white color
    public void WhiteColor()
    {
        glazeRenderer.material = GlazeColorMat[4];
        currentIndex = 4;
        CurrMat = glazeRenderer.material;
        ChangePotColor(currentIndex);
    }

    private void ChangePotColor(int currentIndex)
    {
        if (!isDelayed)
        {
            // Start the delay
            isDelayed = true;

            // Wait for 2 seconds before changing the pot color
            StartCoroutine(ChangePotColorAfterDelay(2f, currentIndex));
        }
    }

    private IEnumerator ChangePotColorAfterDelay(float delay, int currentIndex)
    {
        yield return new WaitForSeconds(delay);

        if (currentIndex < PotColorMat.Length)
        {
            potRenderer.material = PotColorMat[currentIndex];
            CurrMat = potRenderer.material;
        }

        isDelayed = false; // Reset the delay flag
    }

    private void Update()
    {
        // Check if delay is in progress
        if (isDelayed)
        {
            // Wait for the specified delay
            // The delay is handled by the coroutine now, so no need to check Time.time here
        }
    }
    

    ***********************
    public Material[] PotColorMat;
    Material CurrMat;
    Renderer renderer;

    // Use this for initialization
    void Start()
    {

        renderer = this.GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    //render blue color
    public void BlueColor()
    {
        renderer.material = GlazeColorMat[0];
        renderer.material = PotColorMat[0];
        CurrMat = renderer.material;
    }

    //render grey color
    public void GreyColor()
    {
        renderer.material = PotColorMat[1];
        CurrMat = renderer.material;
    }

    //render orangeish color
    public void OrangeishColor()
    {
        renderer.material = PotColorMat[2];
        CurrMat = renderer.material;
    }


    //render white color
    public void WhiteColor()
    {
        renderer.material = PotColorMat[3];
        CurrMat = renderer.material;
    }
    */
}