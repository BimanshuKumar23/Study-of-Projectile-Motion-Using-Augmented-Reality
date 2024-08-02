using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slidercontroller : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public Text valueText;

    // Update is called once per frame
    void Update()
    {
        // Update the text with the current value of the slider
        valueText.text = slider.value.ToString();
    }
}
