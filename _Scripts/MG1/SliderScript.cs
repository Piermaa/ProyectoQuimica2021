using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;


    public float Ccvalue;
    private Erlenmeyer _erlenmeyer;
    private MiniGame1Controller _miniGame1Controller;
    // Start is called before the first frame update
    void Start()
    {
        //_erlenmeyer.molesdeBasePorGota = 0.0000005f;

           _miniGame1Controller = FindObjectOfType<MiniGame1Controller>();
        _erlenmeyer = FindObjectOfType<Erlenmeyer>();
        _slider.value = 0.01f;
        _sliderText.text = "0.010N";
        _slider.onValueChanged.AddListener((Ccvalue)=>{
            _sliderText.text = Ccvalue.ToString("0.000") + "N";
            _erlenmeyer.molesdeBasePorGota = (0.05f * Ccvalue) / 1000;
            _miniGame1Controller.baseConcentration = Ccvalue;
        });

        
    }

    // Update is called once per frame
    void Update()
    {
        //1000ml----ccvalue
        //0.05ml----molesporgota
       
    }
}
