using UnityEngine;
using UnityEngine.UI;

public class RuneStoneSlider : MonoBehaviour
{
    [SerializeField] Slider _slider;

    bool _enabled = false;
    public float sliderRate = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = 0;
        _enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_enabled)
        {
            _slider.value += (sliderRate * Time.deltaTime * 30);

            if (_slider.value >= 1)
            {
                _slider.value = 1;
                sliderRate = -sliderRate;
            }
            if (_slider.value <= 0)
            {
                _slider.value = 0;
                sliderRate = -sliderRate;
            }
        }
        else
        {
            return;
        }
    }

    public void OnClickConfirmButton()
    {
        _enabled = _enabled == true ? false : true;
        Debug.Log(_slider.value);
    }
}
