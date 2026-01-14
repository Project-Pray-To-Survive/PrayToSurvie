using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerStaminaUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    public void SetSlide(float value, float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = value;
    }
}
