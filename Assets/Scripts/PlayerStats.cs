using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public GameObject lifeSliderObject;
    public GameObject energySliderObject;
    public Slider energySlider;
    public Slider lifeSlider;

    public float lifeAmount;
    public float energyAmount;

    private void Awake()
    {
        energySliderObject = GameObject.Find("EnergySlider");
        energySlider = energySliderObject.GetComponent<Slider>();
        lifeSliderObject = GameObject.Find("LifeSlider");
        lifeSlider = lifeSliderObject.GetComponent<Slider>();
        lifeAmount = 100;
        energyAmount = 100;
    }

    // Update is called once per frame
    void Update()
    {
        lifeSlider.value = lifeAmount;
        energySlider.value = energyAmount;
    }

    public void TakeDamage()
    {
        if (lifeAmount > 0)//Player Take Damage
        {
            lifeAmount -= 0.01f;
        }

    }

    public void LifeRestore()
    {
        if (lifeAmount <= 99.9) // Increase Player Life
        {
            lifeAmount += 0.01f;
        }
    }
}
