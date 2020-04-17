using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*This script sets up the health bar UI, lets the developer set the player health and set the max health*/

public class HealthBar : MonoBehaviour {
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    [SerializeField] private Text currentHP_Display;
    [SerializeField] private Text maxHP_Display;
    private Player player;

    void Awake() {
        player = FindObjectOfType<Player>();
    }

    
    void Update() {
        currentHP_Display.text = player.getCurrentHealth().ToString();
        maxHP_Display.text = player.getMaxHealth().ToString();
    }

    public void setHealth(int health) {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void setMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }
}//end of class

/*
Sources:
1) B., Brackeys, 'How to make a HEALTH BAR in Unity!', 2020. [Online]. Available: https://www.youtube.com/watch?v=BLfNP4Sc_iA&t=440s
*/
