using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableDetection : MonoBehaviour
{
    private Erlenmeyer _erlenmeyer;
    private MiniGame1Controller _minigame1Controller;
    private void Start()
    {
        _erlenmeyer = FindObjectOfType<Erlenmeyer>();
        _minigame1Controller = FindObjectOfType<MiniGame1Controller>();
    }
    private void OnDisable()
    {
        _erlenmeyer.volFinal = _erlenmeyer.dropCount2 / 20;
        _erlenmeyer.pHFinal = _erlenmeyer.pH2;
        _minigame1Controller.showFinalPoint = true;
    }
    
}
