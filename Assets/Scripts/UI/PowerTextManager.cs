using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerTextManager : MonoBehaviour
{
    private CannonController _cannonController;
    [SerializeField] private Slider powerSlider;
    [SerializeField] private Text powerText;
    


    public void UpdateUI(int powerValue)
    {
        powerSlider.value = powerValue;
        powerText.text = powerValue.ToString();
    }
}
