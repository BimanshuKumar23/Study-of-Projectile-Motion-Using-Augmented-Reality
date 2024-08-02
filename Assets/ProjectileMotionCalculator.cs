using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProjectileMotionCalculator : MonoBehaviour
{
    public Slider speedSlider;
    public Slider angleSlider;
    public float gravity = 9.81f;

    public TextMeshProUGUI timeOfFlightText;
    public TextMeshProUGUI maximumHeightText;
    public TextMeshProUGUI totalDistanceText;

    // Start is called before the first frame update
    void Start()
    {
        // Add listeners to the sliders
        speedSlider.onValueChanged.AddListener(delegate { CalculateAndDisplay(); });
        angleSlider.onValueChanged.AddListener(delegate { CalculateAndDisplay(); });

        // Calculate and display initially
        CalculateAndDisplay();
    }

    // Calculate time of flight
    public void CalculateAndDisplay()
    {
        float launchSpeed = speedSlider.value;
        float launchAngle = angleSlider.value;

        float angleInRadians = launchAngle * Mathf.Deg2Rad;
        float timeOfFlight = (2 * launchSpeed * Mathf.Sin(angleInRadians)) / gravity;
        float maximumHeight = (Mathf.Pow(launchSpeed, 2) * Mathf.Pow(Mathf.Sin(angleInRadians), 2)) / (2 * gravity);
        float totalDistance = launchSpeed * Mathf.Cos(angleInRadians) * timeOfFlight;

        // Update TextMeshPro components with the calculated values
        timeOfFlightText.text = "Time of Flight: " + timeOfFlight.ToString("F2") + " s";
        maximumHeightText.text = "Maximum Height: " + maximumHeight.ToString("F2") + " m";
        totalDistanceText.text = "Total Distance: " + totalDistance.ToString("F2") + " m";
    }
}
