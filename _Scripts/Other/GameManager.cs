using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Camera MG1Camera;
    public Camera MG2Camera;
    public Camera MG3Camera;
    public Camera MG4Camera;
    public Camera MG5Camera;
    private Erlenmeyer _erlenmeyer;
    private MiniGame1Controller _miniGame1Controller;
    private MG2Controller _mG2Controller;
    private Minigame3Controller _minigame3Controller;
    private MiniGame4Controller _minigame4Controller;

    public GameObject[] livingProves;
    public int probeCount;

    public GameObject startScreen;
    public GameObject tutorialScreen;
    public GameObject pauseScreen;
    public bool onPause;

    public Scene start;
    public Scene game;

    public bool onGame;
    public int activatedMinigame;
    public int preactivatedMinigame;
    //creditos
    public GameObject creditsScreen;
    public GameObject credits;
    public bool moveCredits;


    public enum GameState
    {
        Loading,
        Pause,
        Moving,
        OnMinigame
    }
    public GameState state;
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene("Start");
        onGame = false;
        activatedMinigame = 5;
      
        TurnoffCameras();
        _erlenmeyer = FindObjectOfType<Erlenmeyer>();
        _miniGame1Controller = FindObjectOfType<MiniGame1Controller>();
        _mG2Controller = FindObjectOfType<MG2Controller>();
        _minigame3Controller = FindObjectOfType<Minigame3Controller>();
        _minigame4Controller = FindObjectOfType<MiniGame4Controller>();

    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!onPause)
            {
                PauseGame();
            }else{
                Resume();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _erlenmeyer.DefaultState();
            _mG2Controller.DefaultState();
            _minigame3Controller.DefaultState();
        }
        if(moveCredits)
        {
            credits.transform.Translate(Vector3.up * Time.deltaTime * 60);
        }
    }
    public void StartGame()
    {
        StartCoroutine(ShowTutorial());
        activatedMinigame = 0;

        do { 
        livingProves[Random.Range(1, livingProves.Length)].SetActive(true);
            probeCount++;
        } 
        while (probeCount<10);

    }

    IEnumerator ShowTutorial()
    {
        startScreen.SetActive(false);
        tutorialScreen.SetActive(true);
        yield return new WaitForSeconds(5);
        tutorialScreen.SetActive(false);
    }

    public void StartMinigame(int activeMinigame)
    {
      

        if (activatedMinigame ==1)
        {
            player.SetActive(false);
        }
        activatedMinigame=activeMinigame;

        if (activatedMinigame == 2)
        {
            //player.transform.position = new Vector3(3.686f,2, 7.123f);
        }
        if (activatedMinigame == 4)
        {
            player.SetActive(false);
            MG4Camera.gameObject.SetActive(true);

            if(!_minigame4Controller.notOnUi)
            {
                _minigame4Controller.RestartExperiment();
            }
          
        }
               
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        onPause = false;
      
    }
    public void FinishMinigame()
    {
        player.SetActive(true);
        TurnoffCameras();
        activatedMinigame = 0;
        _miniGame1Controller.ExitMinigame();
        _minigame4Controller.ExitMinigame();
        pauseScreen.SetActive(false);
        Time.timeScale = 1;

    }
    public void EndGame()
    {
        Application.Quit();
    }
    void TurnoffCameras()
    {
        MG1Camera.gameObject.SetActive(false);
        MG4Camera.gameObject.SetActive(false);
      
    }

    public void Credits()
    {
        Vector3 arriba = new Vector3(0, 1, 0);
        credits.transform.position=new Vector3(957.3256f, 0, 32);
        creditsScreen.SetActive(true);
        moveCredits = true;
        StartCoroutine(CreditsDuration());
    }
    IEnumerator CreditsDuration()
    {
        yield return new WaitForSeconds(12);
        moveCredits = false;
        creditsScreen.SetActive(false);
    }

    public void CloseCredits()
    {
        moveCredits = false;
        creditsScreen.SetActive(false);
    }

    public void PauseGame()
    {
        preactivatedMinigame = activatedMinigame;
        pauseScreen.SetActive(true);
        onPause = true;
  
    }
}
