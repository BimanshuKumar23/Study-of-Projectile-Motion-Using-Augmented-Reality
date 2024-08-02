using UnityEngine;
using UnityEngine.UI;

public class LauncherProjectile : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectile;
    public float minSpeed = 0f;
    public float maxSpeed = 20f;
    public float minAngle = 0f;
    public float maxAngle = 90f;
    public float launchSpeed = 10f;
    public float launchAngle = 45f;

    [Header("*Trajectory Display*")]
    public LineRenderer lineRenderer;
    public int linePoints = 175;
    public float timeIntervalinPoints = 0.01f;
    public Slider speedSlider;
    public Slider angleSlider;

    void Start()
    {
        // Set default values for sliders
        speedSlider.minValue = minSpeed;
        speedSlider.maxValue = maxSpeed;
        speedSlider.value = launchSpeed;
        angleSlider.minValue = minAngle;
        angleSlider.maxValue = maxAngle;
        angleSlider.value = launchAngle;

        // Add listener methods to sliders
        speedSlider.onValueChanged.AddListener(UpdateSpeed);
        angleSlider.onValueChanged.AddListener(UpdateAngle);

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
            lineRenderer.enabled = false;      }
        else if (Input.GetMouseButtonUp(0))
        {
            lineRenderer.enabled = false;
            LaunchProjectile();
        }
    }

    void UpdateSpeed(float value)
    {
        launchSpeed = value;
        DrawTrajectory();

    }

    void UpdateAngle(float value)
    {
        launchAngle = value;
        DrawTrajectory(); // Update trajectory display when angle changes
        launchPoint.rotation = Quaternion.Euler(0, 0,launchAngle-90);
    }
    void LaunchProjectile()
    {
        var _projectile = Instantiate(projectile, launchPoint.position, launchPoint.rotation);
        _projectile.GetComponent<Rigidbody>().velocity = launchPoint.up * launchSpeed;
    }



    void DrawTrajectory()
    {
        Vector3 origin = launchPoint.position;
        Vector3 startVelocity = launchSpeed * launchPoint.up;
        lineRenderer.positionCount = linePoints;
        float time = 0;
        for (int i = 0; i < linePoints; i++)
        {
            var x = (startVelocity.x * time) + (Physics.gravity.x / 2 * time * time);
            var y = (startVelocity.y * time) + (Physics.gravity.y / 2 * time * time);
            Vector3 point = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, origin + point);
            time += timeIntervalinPoints;
        }
    }

}
