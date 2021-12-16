using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGame1Controller : MonoBehaviour
{
    public Camera _camera;
    public GameObject plunger;
    public GameObject NaOHDrop;
    public GameObject washBottleOnTable;
    public GameObject washBottleOnHand;
    
    private GameManager _gameManager;
    private PlayerMovement _playerMovement;
    private Erlenmeyer _erlenmeyer;
    public GameObject info1;
    public GameObject info2;
    public GameObject info3;
  
    public GameObject diffSet;
    public GameObject MG1UIReal;
    public GameObject MG1UIEasy;
    public GameObject ccSet;
    public GameObject finalPoint;
    public Slider _slider;
    public GameObject dropRatioUI;

    public TextMeshProUGUI erlenmeyerPHReal;
    public TextMeshProUGUI erlenmeyerProtonsCReal;
    public TextMeshProUGUI erlenmeyerBaseMlReal;
    public TextMeshProUGUI erlenmeyerBaseCReal;
    public TextMeshProUGUI erlenmeyerAcidCReal;


    public TextMeshProUGUI erlenmeyerPHEasy;
    public TextMeshProUGUI erlenmeyerAcccEasy;
    public TextMeshProUGUI erlenmeyerProtonsCCEasy;
    public TextMeshProUGUI erlenmeyerBmlEasy;

    public TextMeshProUGUI finalVolume;
    public TextMeshProUGUI finalPH;

    public float dropRatio;
    public float baseConcentration; 
    
    Vector3 dropStartPos = new Vector3(4.3208f, 2.3532f, -7.1664f);
    public bool showFinalPoint;
    bool canDrop = true;
    public bool MG1isActive;
    private bool pressSpace;

    public int dropState=3;
    public int mgState = 0;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _erlenmeyer = FindObjectOfType<Erlenmeyer>();
        baseConcentration = 0.01f;
    }

    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.DownArrow) && dropState > 1)
        {
            dropState--;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && dropState < 5)
        {
            dropState++;
        }
        _slider.SetValueWithoutNotify(dropState);

        if (_gameManager.activatedMinigame == 1)
        {
            pressSpace = Input.GetKey(KeyCode.Space);
            _camera.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    mgState++;
                }


            if (Input.GetKeyDown(KeyCode.E))
            {
                washBottleOnTable.SetActive(false);
                washBottleOnHand.SetActive(true);
                StartCoroutine(PisetaCD());
                _erlenmeyer.dropCount2 += 0.5f;
            }

            switch (mgState)
            {
                case 0:
                    _playerMovement.titulationNotif.SetActive(false);
                    info1.SetActive(true);
               
                    break;
                case 1:
                    info1.SetActive(false);
                    info2.SetActive(true);
                    break;
                case 2:
                    info2.SetActive(false);
                    info3.SetActive(true);

                    break;
                case 3:
                    info3.SetActive(false);
                    diffSet.SetActive(true);
                    break;
            }
       
            if (MG1isActive)
            {
                if (_erlenmeyer.diff == 1)
                {
                    erlenmeyerPHEasy.text = "pH = " + _erlenmeyer.pH;
                    erlenmeyerAcccEasy.text = "Concentracion de Acido Inicial= "+_erlenmeyer.initialAcidCC+"N";
                    erlenmeyerProtonsCCEasy.text = "Concentracion de Protones = " + _erlenmeyer.acidCC +"M";
                    erlenmeyerBmlEasy.text = "Volumen de Base = " + _erlenmeyer.naohml + "ml";
                }

                if (_erlenmeyer.diff == 2)
                {
                    erlenmeyerPHReal.text= "pH = "+_erlenmeyer.pH2;
                    erlenmeyerProtonsCReal.text ="Concentracion de Cargas Libres = "+ _erlenmeyer.ccCargas +"M";
                    erlenmeyerBaseMlReal.text="Volumen de Base Agregada = "+ _erlenmeyer.mldeBase + "ml";
                    erlenmeyerBaseCReal.text= "Concentracion de Base = "+ baseConcentration+ "N";
                    erlenmeyerAcidCReal.text="Cantidad de Acido Inicial = 10.0ml";

                    if (showFinalPoint)
                    {
                        finalVolume.text = "Volumen Final: " + _erlenmeyer.volFinal;
                        finalPH.text = "pH Final: " + _erlenmeyer.pHFinal;
                    }
                    else {
                        finalVolume.text = "Volumen Final: ";
                        finalPH.text = "pH Final: ";
                    }
                }
               

                if (plunger.transform.position.y > 2.82)
                {
                    Debug.Log("Esta por arriba");
                    if (pressSpace)
                    {
                        plunger.transform.Translate(Vector3.down * Time.deltaTime * 0.3f);
                    }
                }
                if (plunger.transform.position.y < 3.015)
                {
                    plunger.transform.Translate(Vector3.up * Time.deltaTime * 0.15f);
                }

                if (plunger.transform.position.y < 2.85f && canDrop && pressSpace)
                {
                    SpawnDrop();
                    dropRatio = 0.2f;
                }
            }
        }
    }
        
        
    
  
    void SpawnDrop()
    {
        canDrop = false;
        Instantiate(NaOHDrop, dropStartPos, transform.rotation);
        StartCoroutine(DropCD());

    }

    IEnumerator PisetaCD()
    {
        yield return new WaitForSeconds(1);
        washBottleOnHand.SetActive(false);
        washBottleOnTable.SetActive(true);
    }
    IEnumerator MinigameStardCD(int Difficulty)
    {
        yield return new WaitForSeconds(0.2f);
        MG1isActive = true;
        if (Difficulty == 1)
        {
            MG1UIEasy.SetActive(true);
            
        }
        if (Difficulty == 2)
        {
            MG1UIReal.SetActive(true);
            finalPoint.SetActive(true);
            _erlenmeyer.molesDeAcidoen10ml = Random.Range(0.001f,0.003f);
        }
        StopCoroutine(MinigameStardCD(Difficulty));
    }
    IEnumerator DropCD()
    {
        yield return new WaitForSeconds(dropRatio/dropState);
        canDrop = true;
    }
 
    public void DifficultySet(int Difficulty)
    {
        if (Difficulty==1) 
        {
            _erlenmeyer.diff = Difficulty;
            diffSet.SetActive(false);
            StartCoroutine(MinigameStardCD(Difficulty));
            mgState++;
            
        }
        if (Difficulty == 2)
        {
            ccSet.SetActive(true);
            _erlenmeyer.diff = Difficulty;
            diffSet.SetActive(false);
            mgState++;
        }
    }
    public void ExitMinigame()
    {
        _playerMovement.gameObject.SetActive(true);
        DifficultySet(0);
        mgState = 0;
        MG1isActive = false;
        MG1UIEasy.SetActive(false);
        MG1UIReal.SetActive(false);
        finalPoint.SetActive(false);
        info1.SetActive(false);
        _erlenmeyer.DefaultState();
        showFinalPoint = false;
        dropRatioUI.SetActive(false);
    }
    public void StartMinigame()
    {
        StartCoroutine(MinigameStardCD(2));
        ccSet.SetActive(false);
        showFinalPoint = false;
        dropRatioUI.SetActive(true);
    }
}
   

