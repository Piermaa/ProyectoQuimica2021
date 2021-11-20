using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float mouseSensitivity=100f;

    public Transform playerBody;

    float xRotation = 0f;

    

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity/1.3f * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-90, 90);


      
        
        playerBody.Rotate(Vector3.up*mouseX);

        if(_gameManager.activatedMinigame==2|| _gameManager.activatedMinigame == 3 || _gameManager.activatedMinigame == 0)
        {
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            Cursor.lockState = CursorLockMode.Locked;
        }
        if(_gameManager.activatedMinigame ==4 || _gameManager.activatedMinigame == 1)
        { 
            Cursor.lockState = CursorLockMode.None;
        }
      
    }
}
