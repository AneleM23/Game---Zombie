using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientSlider : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fillImage;

    void Start()
    {
        fillImage = slider.fillRect.GetComponent<Image>();
        UpdateGradient(slider.value);
    }

    public void UpdateGradient(float value)
    {
        fillImage.color = gradient.Evaluate(value);
    }
}
