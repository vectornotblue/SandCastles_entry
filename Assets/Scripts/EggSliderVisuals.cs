using UnityEngine;
using UnityEngine.UI;

public class EggSliderVisuals : MonoBehaviour
{
    [SerializeField] private Slider eggSlider;
    
    public void UpdateSlider()
    {
        eggSlider.SetValueWithoutNotify(EggsManager.main.GetEggPercentage());
    }
}
