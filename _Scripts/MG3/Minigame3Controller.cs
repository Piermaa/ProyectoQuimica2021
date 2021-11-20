using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Minigame3Controller : MonoBehaviour
{
    public ParticleSystem lighterFire;
    public ParticleSystem cookieFire;
    public ParticleSystem cookieSmoke;
    public ParticleSystem ashesSmoke;

    public AudioSource fireSound;

    public GameObject preIncineratedCookie;
    public GameObject playersPreIncineratedCookie;
    public GameObject incineratedCookie;
    public GameObject ashes;
   

    public GameObject MG3UI;
    public GameObject info1;
    public GameObject info2;
    public GameObject tutorial;
    public TextMeshProUGUI firstWeight;
    public TextMeshProUGUI endWeight;
   
    public Camera _camera;

    private GameManager _gameManager;
    private PlayerMovement _playerMovement;

    bool pressSpace;
    bool pressEnter;
    public bool BowlOnHand;
    public bool incinerated;

    public float initialWeight;
    public float ashesWeight;
    public float ashesPercentage;


    public int expState;
    public int mgState = 0;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        RandomWeight();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerMovement.onMG3Range)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                mgState++;
            }
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
                expState = 1;
                _gameManager.StartMinigame(3);
                mgState++;
                break;
        }
        if (_gameManager.activatedMinigame == 3)
        {
            _gameManager.player.SetActive(true);
            pressSpace = Input.GetKey(KeyCode.Space);
            MG3UI.gameObject.SetActive(true);

            if (pressSpace && !incinerated &&_playerMovement.onCookieRange)
            {
                preIncineratedCookie.SetActive(false);
                playersPreIncineratedCookie.SetActive(true);
                BowlOnHand = true;
                expState = 2;

               
            }
            if (pressSpace && BowlOnHand && _playerMovement.onLighterRange)
            {

                playersPreIncineratedCookie.SetActive(false);
                incineratedCookie.SetActive(true);
                BowlOnHand = false;
                expState = 3;
                StartCoroutine(Incinerating());
            }
        }
        else {
            MG3UI.gameObject.SetActive(false);
        }
        firstWeight.text = "Masa Inicial: " + initialWeight + "g";
        if (incinerated)
        {
            endWeight.text = "Masa de Cenizas: " + ashesWeight + "g";
        }
        else
        {
            endWeight.text = "Masa de Cenizas  : ";
        }
        ashesWeight = initialWeight * ashesPercentage / 100;
        Debug.Log("final: " + ashesWeight + " initial: " + initialWeight + " %: " + ashesPercentage);
    }
    IEnumerator Incinerating()
    {
        lighterFire.Play();
        fireSound.Play();
        yield return new WaitForSeconds(2);
        cookieSmoke.Play();
        yield return new WaitForSeconds(1);
        cookieFire.Play();
        yield return new WaitForSeconds(6);
        lighterFire.Stop();
        cookieFire.Stop();
        cookieSmoke.Stop();
        yield return new WaitForSeconds(1);
        fireSound.Stop();
        ashes.SetActive(true);
        incineratedCookie.SetActive(false);
        incinerated = true;
    }
    public void DefaultState()
    {
        ashes.SetActive(false);
        incineratedCookie.SetActive(false);
        incinerated = false;
        BowlOnHand = false;
        lighterFire.Stop();
        cookieFire.Stop();
        cookieSmoke.Stop();
        preIncineratedCookie.SetActive(true);
        StopCoroutine(Incinerating());
        RandomWeight();
        mgState=0;
        expState = 0;
        playersPreIncineratedCookie.SetActive(false);
    }
    public float RandomWeight()
    {
        initialWeight = Random.Range(7.5f, 10);
        ashesPercentage = Random.Range(1,3);
        return initialWeight;
    }
}

