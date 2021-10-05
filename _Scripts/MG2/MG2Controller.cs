using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG2Controller : MonoBehaviour
{
    public GameObject preMicrowaveBowl;
    public GameObject onMicrowaveBowl;
    public GameObject postMicrowaveBowl;
    public GameObject playersPreMicrowavedBowl;
    public Camera _camera;

    private GameManager _gameManager;
    private PlayerMovement _playerMovement;
    bool pressSpace;
    bool BowlOnHand;
    bool microwaved;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (_gameManager.activatedMinigame == 2)
        {
            _gameManager.player.SetActive(true);
            pressSpace = Input.GetKey(KeyCode.Space);
            if (pressSpace&&!microwaved)
            {
                preMicrowaveBowl.SetActive(false);
                playersPreMicrowavedBowl.SetActive(true);
                BowlOnHand = true;
                microwaved = true;
            }
            if (pressSpace && BowlOnHand && _playerMovement.onMicrowaveRange)
            {
                playersPreMicrowavedBowl.SetActive(false);
                onMicrowaveBowl.SetActive(true);
                BowlOnHand = false;
                StartCoroutine(Microwaving());
            }
        }

    }
    IEnumerator Microwaving()
    {
        yield return new WaitForSeconds(5);
        postMicrowaveBowl.SetActive(true);
        onMicrowaveBowl.SetActive(false);
    }
}
