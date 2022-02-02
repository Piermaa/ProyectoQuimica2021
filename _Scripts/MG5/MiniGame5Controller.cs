using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame5Controller : MonoBehaviour
{
    public GameObject valve;
    public GameObject rodamin;
    public GameObject methyleneBlue;
    public GameObject methylOrange;
    public Camera _camera;
    [SerializeField] GameObject info1;
    [SerializeField] GameObject info2;
    [SerializeField] GameObject info3;

    private BureteDeposit[] _bureteDeposit;
    private GameManager _gameManager;
    private PlayerMovement _playerMovement;
    private MoveErlenmeyers _moveErlenmeyers;

    public ColoredSubstance[] _coloredSubstance;

    public BoxCollider valveCollider;


    //essay start and ui
    bool mG5IsActive;
    int mgState;
    bool pressEnter;
    int analits;
    public GameObject nextErlenArrow;

    //experiment logic
    public bool pressSpace;

    public float eluentSpeedCorrected;
    public float eluentSpeed;
    public bool valveIsOpen;

    public float rodaminSpeed = 0.43f;
    public float methylOrangeSpeed = 0.6f;
    public float methyleneBlueSpeed = 0.78f;




    //burete logic
    float eluentSpeedStandarization= 0.6870875f;
    float SpeedCorrection = 0.2412667469280711f;
    public float closedValveValue=0.7f;
    float dropRatio=0.2f;
    float fullyOpenedValve = 0.9999f;
    public float rodaminDropRatio = 0.4f;
    public float blueDropRatio = 1;
    public float orangeDropRatio = 0.6f;
    public bool canDrop = true;

    public float actualRatio;
    public float dropCD;
    public string dropCDLog;
    public float eluentRatio = 0.1f;

    public GameObject rodaminDrop;
    public GameObject methyleneBlueDrop;
    public GameObject methylOrangeDrop;
    public GameObject eluentDrop;
    private Vector3 dropStartPos = new Vector3(16.9605f, 1.79f, -0.4994f);

    public GameObject actualDrop;



    Vector3 valveCenter;
    // Start is called before the first frame update
    void Start()
    {
        valveCenter = valveCollider.bounds.center;


        _coloredSubstance = FindObjectsOfType<ColoredSubstance>();
        _bureteDeposit = FindObjectsOfType<BureteDeposit>();
        _gameManager = FindObjectOfType<GameManager>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _moveErlenmeyers= FindObjectOfType<MoveErlenmeyers>();

        mgState = 0;
        _coloredSubstance[1].SetDrop(eluentDrop, blueDropRatio);
    }

    // Update is called once per frame
    void Update()
    {

        dropCD = (dropRatio / actualRatio); // / (eluentSpeed * 200)) ;
    

        if (valve.transform.localRotation.x >= closedValveValue)
        {
            valveIsOpen = true;
        }
        else {
            valveIsOpen = false;
        }

        if (actualDrop == eluentDrop && canDrop && valveIsOpen)
        {
            SpawnDrop();
        }

        pressEnter = Input.GetKeyDown(KeyCode.Return);

        if (pressEnter)
        {
            mgState++;
            if (_playerMovement.onMG5Range)
            {
                _gameManager.StartMinigame(5);
            }
        }
       
       if (_gameManager.activatedMinigame == 5)
        {
            switch (mgState)
            {
                case 1:
                    _gameManager.activatedMinigame = 5;
                    _playerMovement.cromatographyNotif.SetActive(false);
                    info1.SetActive(true);
                    break;
                case 2:
                    info2.SetActive(true);
                    info1.SetActive(false);
                    break;
                case 3:
                    info2.SetActive(false);
                    info3.SetActive(true);

                    AnalitsActivation(AnalitsCuantity());

                        break;
                case 4:
                    info3.SetActive(false);
                    nextErlenArrow.SetActive(true);
                    mG5IsActive = true;
                    break;
            }
        }
        
        
       
        

        if (mG5IsActive)
        {
            pressSpace = Input.GetKey(KeyCode.Space);

            eluentSpeedCorrected = valve.transform.localRotation.x - eluentSpeedStandarization;

            eluentSpeed = eluentSpeedCorrected *SpeedCorrection;

            //0,6870875=0
            //0,9999192=100
            if (valve.transform.localRotation.x < fullyOpenedValve && pressSpace)
            {
                valve.transform.RotateAround(valveCenter, new Vector3(1, 0, 0), 160 * Time.deltaTime);
            }
            else if (!pressSpace && valve.transform.localRotation.x >= closedValveValue)
            {
                valve.transform.RotateAround(valveCenter, new Vector3(-1, 0, 0), 200 * Time.deltaTime);
            }

            _coloredSubstance[1].Elution();
        }
    }



    /// <summary>
    /// Stablishes the amount of analits
    /// </summary>
    /// <returns>Returns the amount of analits on the essay</returns>
    float AnalitsCuantity()
    {
        analits = Random.Range(2, 4);
        return analits;
    }

    /// <summary>
    /// Activates the analits' game objects' depending of the indicated amount
    /// </summary>
    /// <param name="analitsAmount"> Pre-stablished amount of analits </param>
    void AnalitsActivation(float analitsAmount)
    {
        if (analits == 2)
        {
            int unactiveAnalit = Random.Range(1, 3);
            switch (unactiveAnalit)
            {
                case 1:
                    rodamin.SetActive(false);
                    break;
                case 2:
                    methyleneBlue.SetActive(false);
                    break;
                case 3:
                    methylOrange.SetActive(false);
                    break;
            }
        }
        else
        {
            methylOrange.SetActive(true);
            rodamin.SetActive(true);
            methyleneBlue.SetActive(true);
        }
    }

    /// <summary>
    /// Starts the instantiation of Drops and its cooldown
    /// </summary>
    public void SpawnDrop()
    {
        canDrop = false;
        Instantiate(actualDrop, dropStartPos, transform.rotation);
        StartCoroutine(DropCD());

    }

    /// <summary>
    /// Cooldown for the drop spawning
    /// </summary>
    /// <returns>Cooldown between spawned drops</returns>
    IEnumerator DropCD()
    {
        yield return new WaitForSeconds(dropCD);
        canDrop = true;
        Debug.Log("aaasdasdsa");
    }
    /// <summary>
    /// Sets the essay on its default state 
    /// </summary>
    public void RestartExperiment()
    {

        for(int i=0; i<3; i++)
        {
            _coloredSubstance[i].DefaultState();
            _bureteDeposit[i].DefaultState();
        }
       
        rodamin.SetActive(false);
        methylOrange.SetActive(false);
        methyleneBlue.SetActive(false);
        mgState = 0;
        nextErlenArrow.SetActive(false);
        mG5IsActive = false;
        _moveErlenmeyers.DefaultPositions();
        info1.SetActive(false);
        info2.SetActive(false);
        info3.SetActive(false);
     
    }
    /// <summary>
    /// Closes minigame and return player to the freeroam
    /// </summary>
    public void ExitMinigame()
    {
        _camera.gameObject.SetActive(false);
        _gameManager.MG5Camera.gameObject.SetActive(false);
        RestartExperiment();
        _gameManager.player.SetActive(true);

    }
}
