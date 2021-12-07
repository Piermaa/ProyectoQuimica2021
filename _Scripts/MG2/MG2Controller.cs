using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MG2Controller : MonoBehaviour
{
    public GameObject preMicrowaveBowl;
    public GameObject onMicrowaveBowl;
    public GameObject postMicrowaveBowl;
    public GameObject playersPreMicrowavedBowl;
    public Camera _camera;
    public AudioSource _oven;

    public GameObject pressEnterSign;
    public GameObject MG2UI;
    public GameObject info1;
    public GameObject info2;
    public GameObject tutorial;
    public TextMeshProUGUI firstWeight;
    public TextMeshProUGUI endWeight;

    public float initialWeight;
    public float waterPercentage;
    public float finalWeight;

    private GameManager _gameManager;
    private PlayerMovement _playerMovement;
    bool pressSpace;
    public bool BowlOnHand;
    public bool microwaved;
    public int expState;
    public int mgState=0;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        RandomWeight();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerMovement.onMG2Range) {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                mgState++;
            }

            switch (mgState)
            {
                case 1:
                    info1.SetActive(true);
                    break;
                case 2:
                    info2.SetActive(true);
                    info1.SetActive(false);
                    break;
                case 3:
                    info2.SetActive(false);
                    tutorial.SetActive(true);
                    break;
                case 4:
                    tutorial.SetActive(false);
                    _gameManager.StartMinigame(2);
                    expState = 1;
                    mgState++;
                    break;

            }
        }
        if (_gameManager.activatedMinigame == 2)
        {
            
            pressEnterSign.SetActive(false);

            MG2UI.gameObject.SetActive(true);
            _gameManager.player.SetActive(true);
            pressSpace = Input.GetKey(KeyCode.Space);
  
          
            if (pressSpace && !microwaved&&_playerMovement.onWaffleRange)
            {
                preMicrowaveBowl.SetActive(false);
                playersPreMicrowavedBowl.SetActive(true);
                BowlOnHand = true;
                expState = 2;
            }
            if (pressSpace && BowlOnHand && _playerMovement.onMicrowaveRange)
            {
                playersPreMicrowavedBowl.SetActive(false);
                onMicrowaveBowl.SetActive(true);
                BowlOnHand = false;
                expState = 3;
                StartCoroutine(Microwaving());

            }
        }
        firstWeight.text = "Masa Inicial: " + initialWeight + "g";
        if (microwaved)
        {
            endWeight.text = "Masa Final  : " + finalWeight + "g";
        }
        else
        {
            endWeight.text = "Masa Final  : ";
        }
        
        finalWeight = initialWeight - initialWeight * waterPercentage / 100;
        Debug.Log("final: " + finalWeight + " initial: " + initialWeight +"%: "+ waterPercentage);
    }
    IEnumerator Microwaving()
    {
        _oven.Play();
        yield return new WaitForSeconds(5);
        _oven.Stop();
        postMicrowaveBowl.SetActive(true);
        onMicrowaveBowl.SetActive(false);
        microwaved = true;
    }
    public void DefaultState()
    {
        microwaved = false;
        postMicrowaveBowl.SetActive(false);
        onMicrowaveBowl.SetActive(false);
        BowlOnHand = false;
        preMicrowaveBowl.SetActive(true);
        playersPreMicrowavedBowl.SetActive(false);
        RandomWeight();
        mgState = 0;
        expState = 0;
        _playerMovement.wetTestNotif.SetActive(false);
        _playerMovement.firstStepWet.SetActive(false);
        _playerMovement.scndStepWet.SetActive(false);

    }
    public float RandomWeight()
    {
        initialWeight = Random.Range(7.5f,10);
        waterPercentage = Random.Range(20, 43);
        return initialWeight;
    }
}
