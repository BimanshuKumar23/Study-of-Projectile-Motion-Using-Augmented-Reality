using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AngleSliderController : MonoBehaviour
{
    public LauncherProjectile launcherProjectile; // Reference to your launcher projectile script
    public Slider angleSlider; // Reference to the angle slider in the UI
    public TextMeshProUGUI angleText; // Reference to the TextMeshProUGUI component to display angle value

    public void Start()
    {
        // Find the Slider components in the scen
            angleSlider = GameObject.Find("angleSlider").GetComponent<Slider>();

        if (angleSlider == null)
        {
            Debug.LogError("Speed or Angle Slider reference not found!");
            return;
        }

        // Add listeners for the slider value changes
        angleSlider.onValueChanged.AddListener(delegate { OnAngleSliderChange(); });
    }

    public void OnAngleSliderChange()
    {
        // Update the launch angle in the launcher projectile script
        launcherProjectile.launchAngle = angleSlider.value;

        // Update the TextMeshProUGUI component to display angle value
        angleText.text = angleSlider.value.ToString(); // Assuming you want to display the slider value as text
    }
}

