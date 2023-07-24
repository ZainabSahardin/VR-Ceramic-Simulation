using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FireControl : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public TextMeshProUGUI progressMessage;
    public Image uiFill;
    public TextMeshProUGUI countdownText;

    public TextMeshProUGUI temperature;

    private float currentTime = 0f;
    //private float startingTime = 12f;
    public float startingTime;
    private float remainingTime;

    private bool isFiring = false;

    private void Start()
    {
        particleSystem.Stop(); // Stop the emission initially
        ResetTimer();
    }

    public void StartFiring()
    {
        temperature.text = "900 °C";
        if (!isFiring)
        {
            isFiring = true;
            particleSystem.Play(); // Start the emission
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        currentTime = startingTime;
        remainingTime = startingTime - currentTime;
        countdownText.text = currentTime.ToString("0");
        uiFill.fillAmount = 1f;
    }

    void Update()
    {
        if (isFiring)
        {
            currentTime -= Time.deltaTime;
            countdownText.text = currentTime.ToString("0");

            progressMessage.text = "Firing in progress...";

            if (currentTime <= 0)
            {
                StopFiring();
                progressMessage.text = "Firing completed";
            }

            remainingTime = startingTime - currentTime;
            uiFill.fillAmount = Mathf.InverseLerp(0, startingTime, remainingTime);
        }
    }

    public void StopFiring()
    {
        isFiring = false;
        particleSystem.Stop(); // Stop the emission
        ResetTimer();
    }
}
