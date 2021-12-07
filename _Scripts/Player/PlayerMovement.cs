using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    GameManager _gameManager;
    public Camera thisCamera;
    public Animator animator;

    private MG2Controller _mG2Controller;
    private Minigame3Controller _miniGame3Controller;
    private MiniGame4Controller _miniGame4Controller;

    public GameObject wetTestNotif;
    public GameObject firstStepWet;
    public GameObject scndStepWet;

    public GameObject titulationNotif;

    public GameObject ashesTestNotif;
    public GameObject firstStepAshes;
    public GameObject scndStepAshes;
    public GameObject analiticMarchNotif;

    public bool onMG2Range;
    public bool onMG3Range;
    public bool onMG1Range;
    public bool onMG4Range;
    bool pressSpace;
    bool pressEnter;
    public bool onMicrowaveRange;
    public bool onWaffleRange;
    public bool onLighterRange;
    public bool onCookieRange;
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        animator = FindObjectOfType<Animator>();
        controller = FindObjectOfType<CharacterController>();
        _gameManager = FindObjectOfType<GameManager>();
        _mG2Controller = FindObjectOfType<MG2Controller>();
        _miniGame3Controller = FindObjectOfType<Minigame3Controller>();
        _miniGame4Controller = FindObjectOfType<MiniGame4Controller>();
    }

    // Update is called once per frame
    void Update()
    {

        pressSpace = Input.GetKeyDown(KeyCode.Space);
        pressEnter = Input.GetKeyDown(KeyCode.Return);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

         
      
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move*Time.deltaTime*speed);

        if (_gameManager.activatedMinigame == 3)
        {
            ashesTestNotif.SetActive(false);
        }

        if (onMG1Range && pressSpace)
        {
            this.gameObject.SetActive(false);
            _gameManager.StartMinigame(1);
            analiticMarchNotif.SetActive(false);
        }

        if (onMG4Range && pressEnter)
        {
            this.gameObject.SetActive(false);
            _gameManager.StartMinigame(4);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 8;
        }
        else {
            speed = 6;
        }
     
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool hasHorizontalInput = !Mathf.Approximately(x, 0f);
        bool hasVerticalInput = !Mathf.Approximately(z, 0f);

        bool isWalking = hasHorizontalInput || hasVerticalInput;

        //animator.SetBool("isWalking", isWalking);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MG1"))
        {
            Debug.Log("Rango Tit");
            titulationNotif.SetActive(true);
            onMG1Range = true;
         
        }

        if (other.CompareTag("MG2"))
        {
            wetTestNotif.SetActive(true);
            onMG2Range=true;
            if ((_gameManager.activatedMinigame == 2))
            {
                _mG2Controller.MG2UI.SetActive(true);
            }
            if (pressSpace)
            {
                //_gameManager.StartMinigame(thisCamera, 2);
            }
            if (_mG2Controller.BowlOnHand)
            {
                firstStepWet.SetActive(false);
                onWaffleRange = false;
            }
            if(_mG2Controller.expState==3)
            {
                scndStepWet.SetActive(false);
                onMicrowaveRange = false;
            }
        }

        if (other.CompareTag("MG3"))
        {
            ashesTestNotif.SetActive(true);
            onMG3Range = true;
            if (pressSpace)
            {
                _gameManager.StartMinigame(3);
            }
            if (_miniGame3Controller.BowlOnHand)
            {
                firstStepAshes.SetActive(false);
                onCookieRange = false;
            }
            if (_miniGame3Controller.expState == 3)
            {
                scndStepAshes.SetActive(false);
                onLighterRange = false;
            }
        }
        if (other.CompareTag("MG4"))
        {
            onMG4Range = true;
            if (onMG4Range)
            {
                analiticMarchNotif.SetActive(true);
            }
            else
            {
                analiticMarchNotif.SetActive(false);
            }
         
        }
        
        if (other.CompareTag("Microwave"))
        {
            onMicrowaveRange = true;
            if (_gameManager.activatedMinigame == 2 && _mG2Controller.BowlOnHand       &&  _mG2Controller.expState==2)
            {
                scndStepWet.SetActive(true);
            }
        }
        if (other.CompareTag("Lighter"))
        {
            onLighterRange = true;
            if (_gameManager.activatedMinigame == 3 && _miniGame3Controller.BowlOnHand &&  _miniGame3Controller.expState==2)
            {
                scndStepAshes.SetActive(true);
            }
        
        }
        if(other.CompareTag("WaffleBowl"))
        {
            if (_gameManager.activatedMinigame==2   &&  !_mG2Controller.BowlOnHand       &&  _mG2Controller.expState==1) 
            {
                onWaffleRange = true;
                firstStepWet.SetActive(true);
            }
        }
        if (other.CompareTag("CookieBowl"))
        {
            if (_gameManager.activatedMinigame == 3 &&  !_miniGame3Controller.BowlOnHand  &&  _miniGame3Controller.expState==1)
            {
                firstStepAshes.SetActive(true);
                onCookieRange = true;
            }
 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MG1")) 
        {
            titulationNotif.SetActive(false);
            onMG1Range = false;
        }
        if (other.CompareTag("MG2"))
        {
            wetTestNotif.SetActive(false);
            _mG2Controller.DefaultState();
            _mG2Controller.MG2UI.gameObject.SetActive(false);
            _gameManager.activatedMinigame = 0;
            onMG2Range = false;
        }
        if (other.CompareTag("MG3"))
        {
            ashesTestNotif.SetActive(false);
            onMG3Range = false;
            _miniGame3Controller.DefaultState();
            _gameManager.activatedMinigame = 0;
        }
        if (other.CompareTag("MG4"))
        {
            analiticMarchNotif.SetActive(false);
            _gameManager.activatedMinigame = 0;
            onMG4Range = false;
        }
        if (other.CompareTag("Microwave")|| _mG2Controller.microwaved)
        {
            onMicrowaveRange = false;
            scndStepWet.SetActive(false);
        }
        if(other.CompareTag("Lighter")||_miniGame3Controller.incinerated)  
        {
            onLighterRange = false;
            scndStepAshes.SetActive(false);
        }
        if (other.CompareTag("WaffleBowl"))
        {
            firstStepWet.SetActive(false);
            onWaffleRange=false;
         
        }
        if (other.CompareTag("CookieBowl"))
        {
            firstStepAshes.SetActive(false);
            onCookieRange = false;
        }
        

    }
    void ToggleInfo(GameObject title, bool state)
    {
        title.SetActive(state);
        state = false;
    }
}
