using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    MiniGame4Controller _miniGame4Controller;
    public AudioSource centrifugueSound;
    bool playSound;
    // Start is called before the first frame update
    void Start()
    {
        _miniGame4Controller = FindObjectOfType<MiniGame4Controller>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.CompareTag("Buzo"))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 1000);
        }
        else
        {
            if (_miniGame4Controller.centrifugueOn)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * 1000);
                if (!centrifugueSound.isPlaying)
                {
                    centrifugueSound.Play();
                }
            }
            else
            {
                centrifugueSound.Stop();
               
            }
        }
     
       
    }
    
}
