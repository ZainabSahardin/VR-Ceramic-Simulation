using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ShapingScript : MonoBehaviour
{
    private float currentTime = 0f;
    private float startingTime = 5f;
    private float remainingTime;

    [SerializeField] private Image uiFill;
    public TextMeshProUGUI countdownText;

    public GameObject[] shapes;
    public GameObject[] handGuides;
    public TextMeshProUGUI displayStep;
    public string[] stepText;
    int indexNo = 0;

    private bool timerStarted = false; // Added flag to track if the timer has started

    void Start()
    {
        
    }

    void Update()
    {
        if (timerStarted) // Added check to run the timer only if it has started
        {
            StartTimer();
        }
    }

    void StartTimer()
    {
        currentTime -= Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;

            indexNo++;
            if (indexNo >= shapes.Length)
            {
                indexNo = shapes.Length - 1; // Set the index to the last shape in the list
            }
            
            SetShape(indexNo);
            timerStarted = false; // Stop the timer after transitioning to the next object
            Invoke("StartTimerAfterDelay", 5f); // Invoke the timer to start again after 5 seconds
        }

        remainingTime = startingTime - currentTime;
        uiFill.fillAmount = Mathf.InverseLerp(0, startingTime, remainingTime);
    }

    // Function to start the timer when the button is clicked
    public void StartTimerOnClick()
    {
        displayStep.text = stepText[indexNo];

        if (!timerStarted) // Added check to prevent multiple timer starts on button click
        {
            currentTime = startingTime;
            countdownText.text = currentTime.ToString("0");
            timerStarted = true;
        }
    }
    
    // Invoke the timer to start again after 5 seconds
    void StartTimerAfterDelay()
    {
        StartTimer();
    }
    
    // Set the current shape to the given index
    public void SetShape(int index)
    {
        // Deactivate all shapes
        foreach (GameObject shape in shapes)
        {
            shape.SetActive(false);
        }

        // Activate the selected shape
        shapes[index].SetActive(true);

        foreach (GameObject shape in handGuides)
        {
            shape.SetActive(false);
        }

        // Activate the selected shape
        handGuides[index].SetActive(true);

        displayStep.text = stepText[index];
    }
}
