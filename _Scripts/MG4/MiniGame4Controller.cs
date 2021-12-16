using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MiniGame4Controller : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private GameManager _gameManager;

    public GameObject[] tubos;
    public GameObject drop;
    public GameObject orangeDrop;

    //tubo 1
    public GameObject problemSolution;
    public GameObject addedHClliquidphase;
    public GameObject addedHClgroup1;
    public GameObject liquidPhase;

    public GameObject hcl3t2Flask;
    public GameObject hcl3t2FlaskServing;
    public GameObject hclt2Text;
    public GameObject hclt2TextServing;

    public Transform tubo1;
    //tubo 2
    public GameObject group1;
    public GameObject water;
    public GameObject group1WithoutPb;
    public GameObject nH4OH;
    public GameObject hgNH2Clblackpp;

    public AudioSource lighterSound;

    public GameObject agNH32ClT2;
    public GameObject hNO3T2;
    public GameObject AgClT2;

    public GameObject tubo2Calor;

    public GameObject h2oFlask;
    public GameObject h2oFlaskServing;
    public GameObject h2oText;
    public GameObject h20TextServing;

    public GameObject hno3t2Flask;
    public GameObject hno3t2FlaskServing;
    public GameObject hno3t2Text;
    public GameObject hno3t2TextServing;

    public GameObject nh4ohFlask;
    public GameObject nh4ohFlaskServing;
    public GameObject nh4ohText;
    public GameObject nh4ohTextServing;

    public Transform tubo2;
    //tubo 3
    public GameObject h20withPb;
    public GameObject K2Cr04;
    public GameObject pbCrO4;

    public GameObject K2Cr04Flask;
    public GameObject K2Cr04FlaskServing;
    public GameObject K2Cr04Text;
    public GameObject K2Cr04TextServing;

    public Transform tubo3;
    //tubo 4
    public GameObject agNH32Cl;
    public GameObject hNO3;
    public GameObject AgCl;

    public GameObject hno3Flask;
    public GameObject hno3FlaskServing;
    public GameObject hno3Text;
    public GameObject hno3TextServing;

    public Transform tubo4;

    public bool hasPb;
    public bool hasAg;
    public bool hasHg;
    public bool centrifugueOn;
    public bool notOnUi;

    int pb;
    int ag;
    int hg;
    public int mgState;
    public int marchStep;

    public ParticleSystem fire;

    //Interfaz
    public GameObject info1;
    public GameObject info2;
    public GameObject map;
    public GameObject buttonsPanel;
    public GameObject mapButton;
    public GameObject closeMap;

    //Interfaz de fin de test
    public GameObject endTestButton;
    public GameObject endButtons;
    public GameObject endPanel;
    public bool saidHasAg;
    public bool saidHasPb;
    public bool saidHasHg;
    public GameObject verify;
    public GameObject restart;
    // Ag
    public GameObject correctAg;
    public GameObject incorrectAg;
    //Pb
    public GameObject correctPb;
    public GameObject incorrectPb;
    //Hg
    public GameObject correctHg;
    public GameObject incorrectHg;



    private void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (_gameManager.activatedMinigame == 4)
        {
            _playerMovement.analiticMarchNotif.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                mgState++;
            }
            switch (mgState)
            {
                case 0:
                    info1.SetActive(true);
                    notOnUi = false;
                    break;
                case 1:
                    info1.SetActive(false);
                    info2.SetActive(true);
                    break;
                case 2:
                    info2.SetActive(false);
                    map.SetActive(true);
                    break;
                case 3:
                    map.SetActive(false);
                    buttonsPanel.SetActive(true);
                    mapButton.SetActive(true);
                    endTestButton.SetActive(true);
                    mgState++;
                    notOnUi = true;
                    break;

            }
        }
    }
   /// <summary>
   /// Resets all game objects and UI in the minigame, also re-randomizes problem solution s cations
   /// </summary>
    public void RestartExperiment()
    {

        problemSolution.SetActive(true);
        addedHClliquidphase.SetActive(false);
        addedHClgroup1.SetActive(false);
        liquidPhase.SetActive(false);
        group1.SetActive(false);
        water.SetActive(false);
        group1WithoutPb.SetActive(false);
        nH4OH.SetActive(false);
        hgNH2Clblackpp.SetActive(false);
        h20withPb.SetActive(false);
        K2Cr04.SetActive(false);
        pbCrO4.SetActive(false);
        agNH32Cl.SetActive(false);
        hNO3.SetActive(false);
        AgCl.SetActive(false);
        agNH32ClT2.SetActive(false);
        hNO3T2.SetActive(false);
        AgClT2.SetActive(false);
        endButtons.SetActive(false);
        endPanel.SetActive(false);
        verify.SetActive(false);
        restart.SetActive(false);
        correctAg.SetActive(false);
        correctPb.SetActive(false);
        correctHg.SetActive(false);
        incorrectAg.SetActive(false);
        incorrectPb.SetActive(false);
        incorrectHg.SetActive(false);





        pb = Random.Range(0, 10);
        ag = Random.Range(0, 10);
        hg = Random.Range(0, 10);
        if (pb < 8)
        {
            hasPb = true;
        }
        else
        {
            hasPb = false;
        }
        if (ag < 7)
        {
            hasAg = true;
        }
        else
        {
            hasAg = false;
        }
        if (hg < 7)
        {
            hasHg = true;
        }
        else
        {
            hasHg = false;
        }
        marchStep = 0;
    }

    /// <summary>
    /// Deactivates solid phase from a tube and activates it in another, separation depends of march step
    /// </summary>
    public void Separate()
    {
        if (hasAg || hasHg || hasPb)
        {
            if (marchStep == 1)
            {
                SeparateGroup1ofProblem();
            }
        }

        if (marchStep==3)
        {
            SeparateH20withPbofGroup1();
        }
        SepararNitratodePlataAmoniacal();
            
        
    }
    /// <summary>
    /// Activates HCl on tube 1 and solid phase if the tube contains group 1 cations
    /// </summary>
    public void AddHCl()
    {
        if (marchStep == 0) {
            if (hasAg || hasHg || hasPb)
            {
                StartCoroutine(AddingHCl());
                Debug.Log("Se agrego HCl y precipito un solido blanco (grupo 1)");
            }
            marchStep=1;
        }
    }
    IEnumerator AddingHCl()
    {
        hcl3t2Flask.SetActive(false);
        hcl3t2FlaskServing.SetActive(true);
        hclt2Text.SetActive(false);
        hclt2TextServing.SetActive(true);
        yield return new WaitForSeconds(1.7f);
        hcl3t2Flask.SetActive(true);
        hcl3t2FlaskServing.SetActive(false);
        hclt2Text.SetActive(true);
        hclt2TextServing.SetActive(false);


        problemSolution.SetActive(false);

        addedHClliquidphase.SetActive(true);
        addedHClgroup1.SetActive(true);
    }
    public void SeparateGroup1ofProblem()
    {
        if (marchStep == 1)
        {
            StartCoroutine(CentrifugateGroup1());
            liquidPhase.SetActive(true);
            //tubo 1 a centri
            addedHClliquidphase.SetActive(false);
            addedHClgroup1.SetActive(false);

            Debug.Log("Se separo el pp blanco del resto del problema con una decantacion");

            marchStep = 2;
        }
    }
    IEnumerator CentrifugateGroup1()
    {
        tubos[1].SetActive(false);
        centrifugueOn = true;
        yield return new WaitForSeconds(3);
        centrifugueOn = false;
        tubos[1].SetActive(true);
        group1.SetActive(true);
    }
    /// <summary>
    /// Activates water, then toggles heat system, fire particles and tube 2
    /// </summary>
    public void AddH20andHeat()
    {
        if (marchStep == 2)
        {
            StartCoroutine(AddH20());
          
           
            Debug.Log("Se agrego agua y calor, luego se debe centrifugar");
            
        }
    }
    IEnumerator AddH20()
    {
        h2oFlask.SetActive(false);
        h2oFlaskServing.SetActive(true);
        h2oText.SetActive(false);
        h20TextServing.SetActive(true);
        InvokeRepeating("SpawnDrop()", 0, 0.3f);
        yield return new WaitForSeconds(1.7f);
        h2oFlask.SetActive(true);
        h2oFlaskServing.SetActive(false);
        h2oText.SetActive(true);
        h20TextServing.SetActive(false);

        water.SetActive(true);

        StartCoroutine(AddHeat());
    }
    IEnumerator AddHeat()
    {
        yield return new WaitForSeconds(1);
        tubos[2].SetActive(false);
        tubo2Calor.SetActive(true);
        fire.Play();
        lighterSound.Play();
        yield return new WaitForSeconds(4);
        fire.Stop();
        lighterSound.Stop();
        tubo2Calor.SetActive(false);
        tubos[2].SetActive(true);
        group1WithoutPb.SetActive(true);
        marchStep = 3;
    }
    public void SeparateH20withPbofGroup1()
    {
        if (marchStep==3) {
            StartCoroutine(CentrifugateH20withPb());
            water.SetActive(false);
            Debug.Log("Se separo el agua con Pb del resto del grupo 1");
            marchStep = 4;
        }
    }
    IEnumerator CentrifugateH20withPb()
    {
        tubos[2].SetActive(false);
        centrifugueOn = true;
        yield return new WaitForSeconds(3);
        centrifugueOn = false;
        tubos[2].SetActive(true);
        h20withPb.SetActive(true);
    }
    /// <summary>
    /// Toggles K2CrO4 and PbCrO4 if there is Pb++
    /// </summary>
    public void AddK2CrO4()
    {
        if (marchStep == 4)
        {

            StartCoroutine(AddingK2CrO4());
            Debug.Log("Al tubo 3 se agregó K2CrO4");
            marchStep = 5;
        }
    }
    IEnumerator AddingK2CrO4()
    {
        K2Cr04Flask.SetActive(false);
        K2Cr04FlaskServing.SetActive(true);
        K2Cr04Text.SetActive(false);

        yield return new WaitForSeconds(1.7f);
        K2Cr04Flask.SetActive(true);
        K2Cr04FlaskServing.SetActive(false);
        K2Cr04Text.SetActive(true);
        

        K2Cr04.SetActive(true);
        if (hasPb)
        {
            pbCrO4.SetActive(true);
        }

        h20withPb.SetActive(false);

    }
    /// <summary>
    /// Toggles NH4OH and black pp if there is Hg
    /// </summary>
    public void AddNH4OH()
    {
        StartCoroutine(AddingNH4OH());
    }

    IEnumerator AddingNH4OH()
    {
        nh4ohFlask.SetActive(false);
        nh4ohFlaskServing.SetActive(true);
        nh4ohTextServing.SetActive(false);

        yield return new WaitForSeconds(1.7f);
        nh4ohFlask.SetActive(true);
        nh4ohFlaskServing.SetActive(false);
        nh4ohText.SetActive(true);
        nh4ohTextServing.SetActive(false);


        if (marchStep == 5)
        {
            group1.SetActive(false);
            nH4OH.SetActive(true);
            if (hasHg)
            {
                hgNH2Clblackpp.SetActive(true);
            }

            group1WithoutPb.SetActive(false);
            Debug.Log("Se agrego NH4OH al tubo 2");
            marchStep = 6;
        }
    }
    /// <summary>
    /// Another separation
    /// </summary>
    public void SepararNitratodePlataAmoniacal()
    {
        if (marchStep == 6)
        {
            StartCoroutine(LastCentrifugate());
            marchStep = 7;
        }
    }
    IEnumerator LastCentrifugate()
    {
        tubos[2].SetActive(false);
        centrifugueOn = true;
        yield return new WaitForSeconds(3);
        centrifugueOn = false;
        tubos[2].SetActive(true);
        agNH32Cl.SetActive(true);
        nH4OH.SetActive(false);

    }
    /// <summary>
    /// Toggles HNO3 and AgNO3, flask affected changes by existance of Hg++
    /// </summary>
    public void AddHNO3()
    {
        StartCoroutine(AddingHNO3());
    }

    IEnumerator AddingHNO3()
    {

        if (marchStep == 7 || (marchStep == 6 && !hasHg))
        {
            if (hasHg)
            {
                hno3Flask.SetActive(false);
                hno3FlaskServing.SetActive(true);
                hno3Text.SetActive(false);
                hno3TextServing.SetActive(true);

                yield return new WaitForSeconds(1.7f);
                hno3Flask.SetActive(true);
                hno3FlaskServing.SetActive(false);
                hno3Text.SetActive(true);
                hno3TextServing.SetActive(false);
                agNH32Cl.SetActive(false);
                if (hasAg)
                {
                    AgCl.SetActive(true);
                }
                hNO3.SetActive(true);
            }

            if (!hasHg)
            {
                hno3t2Flask.SetActive(false);
                hno3t2FlaskServing.SetActive(true);
                hno3t2Text.SetActive(false);
                hno3t2TextServing.SetActive(true);

                yield return new WaitForSeconds(1.7f);
                hno3t2Flask.SetActive(true);
                hno3t2FlaskServing.SetActive(false);
                hno3t2Text.SetActive(true);
                hno3t2TextServing.SetActive(false);

                agNH32Cl.SetActive(false);
                agNH32ClT2.SetActive(false);
                if (hasAg)
                {
                    AgClT2.SetActive(true);
                }
                hNO3T2.SetActive(true);
            }

            Debug.Log("Se agrego HNO3 al tubo 4 y se forma AgCl");

            marchStep = 8;
        }
      
    }
    public void ActivateButtons()
    {
        buttonsPanel.SetActive(true);
    }
   
    /// <summary>
    /// Resets experiment to base state and deactivates its UI
    /// </summary>
    public void ExitMinigame()
    {
        RestartExperiment();
        
        mgState = 0;
        marchStep = 0;
     
        
        mapButton.SetActive(false);
        map.SetActive(false);
        closeMap.SetActive(false);
        buttonsPanel.SetActive(false);
        endTestButton.SetActive(false);
    }
    public void ShowMap()
    {
        map.SetActive(true);
        closeMap.SetActive(true);
    }
    public void CloseMap()
    {
        map.SetActive(false);
        closeMap.SetActive(false);
        
    }
    /// <summary>
    /// Shows UI for a test results verification
    /// </summary>
    public void EndTest()
    {
        map.SetActive(false);
        closeMap.SetActive(false);
        buttonsPanel.SetActive(false);

        endButtons.SetActive(true);
        endPanel.SetActive(true);
        verify.SetActive(true);
        restart.SetActive(true);
    }
    /// <summary>
    /// Says that the mentioned cation is present in the sample
    /// </summary>
    /// <param name="ion">int that identifies cation (Hg++,Ag+ or/and Pb++)</param>
    public void VotateYes(int ion)
    {
        switch (ion)
        {
            case 1:
                saidHasAg = true;
                break;
            case 2:
                saidHasPb = true;
                break;
            case 3:
                saidHasHg = true;
                break;
        }

    }
    /// <summary>
    /// Says that the mentioned cation is not present in the sample
    /// </summary>
    /// <param name="ion">int that identifies cation (Hg++,Ag+ or/and Pb++)</param>
    public void VotateNo(int ion)
    {
      switch (ion)
      {
        case 1:
            saidHasAg = false;
            break;
        case 2:
            saidHasPb = false;
            break;
        case 3:
            saidHasHg = false;
            break;
      }
    }
    /// <summary>
    /// Fetches coincidences between what is on the sample and the answers given
    /// </summary>
    public void DeterminateResults()
    {
        verify.SetActive(false);
        if (hasAg == saidHasAg)
        {
            correctAg.SetActive(true);
        }
        else {
            incorrectAg.SetActive(true);
        }
        if (hasPb == saidHasPb)
        {
            correctPb.SetActive(true);
        }
        else {
            incorrectPb.SetActive(true);
        }
        if (hasHg == saidHasHg)
        {
            correctHg.SetActive(true);
        }
        else {
            incorrectHg.SetActive(true);
        }
    

    }
}
