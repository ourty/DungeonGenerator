using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        slider.maxValue = player.GetComponent<PlayerJoystick>().health;
        slider.value = player.GetComponent<PlayerJoystick>().health;
    }

    private void Update()
    {
        SetHealth(player.GetComponent<PlayerJoystick>().health);
    }
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
