using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class Health_Bar : MonoBehaviour
{
    public Slider slider;
    public int _currentHealth;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
