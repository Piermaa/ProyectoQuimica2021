using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGame1Controller : MonoBehaviour
{
    public Camera _camera;
    public GameObject embolo;
    public GameObject NaOHDrop;
    
    private GameManager _gameManager;
    private PlayerMovement _playerMovement;
    private Erlenmeyer _erlenmeyer;
    public GameObject info1;
    public GameObject info2;
    public GameObject info3;
  
    public GameObject diffSet;
    public GameObject MG1UIReal;
    public GameObject MG1UIEasy;

    public TextMeshProUGUI erlenmeyerPHReal;
    public TextMeshProUGUI erlenmeyerProtonsCReal;
    public TextMeshProUGUI erlenmeyerBaseMlReal;
    public TextMeshProUGUI erlenmeyerBaseCReal;
    public TextMeshProUGUI erlenmeyerAcidCReal;


    public TextMeshProUGUI erlenmeyerPHEasy;
    public TextMeshProUGUI erlenmeyerAcccEasy;
    public TextMeshProUGUI erlenmeyerProtonsCCEasy;
    public TextMeshProUGUI erlenmeyerBmlEasy;


    float dropRatio;
    
    Vector3 dropStartPos = new Vector3(4.3208f, 2.3532f, -7.1664f);
    
    bool canDrop = true;
    public bool MG1isActive;
    private bool pressSpace;

    public int mgState = 0;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _erlenmeyer = FindObjectOfType<Erlenmeyer>();
    }

    private void Update()
    {
        if (_gameManager.activatedMinigame == 1)
        {
            
               
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    mgState++;
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
                pressSpace = Input.GetKey(KeyCode.Space);

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
                    erlenmeyerProtonsCReal.text ="Concentracion de Protones = "+ _erlenmeyer.ConcentracionProtones;
                    erlenmeyerBaseMlReal.text="Volumen de Base Agregada = "+ _erlenmeyer.mldeBase + "ml";
                    erlenmeyerBaseCReal.text= "Concentracion de Base = 0.001N";
                    erlenmeyerAcidCReal.text="Cantidad de Acido Inicial = 10.0ml";
                }



               

                if (embolo.transform.position.y > 2.82)
                {
                    Debug.Log("Esta por arriba");
                    if (pressSpace)
                    {
                        embolo.transform.Translate(Vector3.down * Time.deltaTime * 0.3f);
                    }
                }
                if (embolo.transform.position.y < 3.015)
                {
                    embolo.transform.Translate(Vector3.up * Time.deltaTime * 0.15f);
                }

                if (embolo.transform.position.y < 2.85f && canDrop && pressSpace)
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
        }
        StopCoroutine(MinigameStardCD(Difficulty));
    }
    IEnumerator DropCD()
    {
        yield return new WaitForSeconds(dropRatio/1.3f);
        canDrop = true;
    }
 
    public void DifficultySet(int Difficulty)
    {
        _erlenmeyer.diff = Difficulty;
        diffSet.SetActive(false);
        StartCoroutine(MinigameStardCD(Difficulty));
        mgState++;
       
    
    }
    public void ExitMinigame()
    {
        DifficultySet(0);
        mgState = 0;
        MG1isActive = false;
        MG1UIEasy.SetActive(false);
        MG1UIReal.SetActive(false);
        info1.SetActive(false);
        _erlenmeyer.DefaultState();
    }
}
   

