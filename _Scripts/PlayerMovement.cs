using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    GameManager _gameManager;
    public Camera thisCamera;

    public GameObject titulationNotif;
  

    bool pressSpace;
    public bool onMicrowaveRange;
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
        controller = FindObjectOfType<CharacterController>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        pressSpace = Input.GetKey(KeyCode.Space);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move*Time.deltaTime*speed);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MG1"))
        {
            titulationNotif.SetActive(true);

            if (pressSpace)
            {
                this.gameObject.SetActive(false);
                _gameManager.StartMinigame(_gameManager.MG1Camera, 1);
            }
        }
       
        if (other.CompareTag("MG2"))
        {
            if (pressSpace)
            {
                this.gameObject.SetActive(false);
                _gameManager.StartMinigame(thisCamera, 2);
            }
        }
        if (other.CompareTag("Microwave"))
        {
            onMicrowaveRange = true;
            Debug.Log("EnRangoDeMicroondas");
        }
        else {
            onMicrowaveRange = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MG1")) 
        {
            titulationNotif.SetActive(false);
        }
    }
}
