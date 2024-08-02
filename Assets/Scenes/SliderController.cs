using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderController : MonoBehaviour
{
    public LauncherProjectile launcherProjectile; // Reference to your launcher projectile script
    public Slider speedSlider; // Reference to the speed slider in the UI
    public TextMeshProUGUI speedText; // Reference to the TextMeshProUGUI component to display speed value

    public void Start()
    {
        // Find the Slider components in the scene
        speedSlider = FindObjectOfType<Slider>();

        if (speedSlider == null)
        {
            Debug.LogError("Speed or Angle Slider reference not found!");
            return;
        }

        // Add listeners for the slider value changes
        speedSlider.onValueChanged.AddListener(delegate { OnSpeedSliderChange(); });
    }

   public void OnSpeedSliderChange()
    {
        // Update the launch speed in the launcher projectile script
        launcherProjectile.launchSpeed = speedSlider.value;

        // Update the TextMeshProUGUI component to display speed value
        speedText.text = speedSlider.value.ToString(); // Assuming you want to display the slider value as text
    }
}
