using UnityEngine;
using UnityEngine.UI;

public class verticalProjectile: MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectile;
    public float minSpeed = 5f;
    public float maxSpeed = 20f;
    public float minAngle = 30f;
    public float maxAngle = 60f;
    public float minHeight = 0f; // Minimum height
    public float maxHeight = 10f; // Maximum height
    public float launchSpeed = 10f;
    public float launchAngle = 45f;
    public float launchHeight = 5f; // Initial height

    [Header("*Trajectory Display*")]
    public LineRenderer lineRenderer;
    public int linePoints = 175;
    public float timeIntervalinPoints = 0.01f;
    public Slider speedSlider;
    public Slider angleSlider;
    public Slider heightSlider; // Reference to the slider controlling the height parameter

    void Start()
    {
        // Set default values for sliders
        speedSlider.minValue = minSpeed;
        speedSlider.maxValue = maxSpeed;
        speedSlider.value = launchSpeed;
        angleSlider.minValue = minAngle;
        angleSlider.maxValue = maxAngle;
        angleSlider.value = launchAngle;
        heightSlider.minValue = minHeight;
        heightSlider.maxValue = maxHeight;
        heightSlider.value = launchHeight;

        // Add listener methods to sliders
        speedSlider.onValueChanged.AddListener(UpdateSpeed);
        angleSlider.onValueChanged.AddListener(UpdateAngle);
        heightSlider.onValueChanged.AddListener(UpdateHeight);

        // Draw trajectory initially
        DrawTrajectory();
    }

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                DrawTrajectory();
                lineRenderer.enabled = true;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lineRenderer.enabled = false;
                LaunchProjectile();
            }
        }
        // Check for mouse input
        else if (Input.GetMouseButtonDown(0))
        {
            DrawTrajectory();
            lineRenderer.enabled = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            lineRenderer.enabled = false;
            LaunchProjectile();
        }
    }

    void UpdateSpeed(float value)
    {
        launchSpeed = value;
    }

    void UpdateAngle(float value)
    {
        launchAngle = value;
        DrawTrajectory(); // Update trajectory display when angle changes
    }

    void UpdateHeight(float value)
    {
        launchHeight = value;
        DrawTrajectory(); // Update trajectory display when height changes
    }

    void LaunchProjectile()
    {
        float angleInRadians = launchAngle * Mathf.Deg2Rad;
        Vector3 launchDirection = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0);

        // Apply height to the launch direction
        launchDirection.y *= launchHeight;

        // Instantiate the projectile at the launch point
        var _projectile = Instantiate(projectile, launchPoint.position, Quaternion.identity);

        // Apply velocity to the projectile Rigidbody
        _projectile.GetComponent<Rigidbody>().velocity = launchDirection * launchSpeed;
    }

    void DrawTrajectory()
    {
        Vector3 origin = launchPoint.position;
        Vector3 startVelocity = launchSpeed * Vector3.up; // Vertical initial velocity
        lineRenderer.positionCount = linePoints;
        float time = 0;
        for (int i = 0; i < linePoints; i++)
        {
            var x = (startVelocity.x * time) + (Physics.gravity.x / 2 * time * time);
            var y = (startVelocity.y * time) + (Physics.gravity.y / 2 * time * time) + launchHeight; // Add launch height
            Vector3 point = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, origin + point);
            time += timeIntervalinPoints;
        }
    }
}
