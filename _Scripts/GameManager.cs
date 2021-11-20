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
    public GameObject pauseScreen;

    public Scene start;
    public Scene game;

    public bool onGame;
    public int activatedMinigame;
    public enum GameState
    {
        Loading,
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
       
        if (Input.GetKey(KeyCode.Escape))
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _erlenmeyer.DefaultState();
            _mG2Controller.DefaultState();
            _minigame3Controller.DefaultState();
        }
    }
    public void StartGame()
    {
        startScreen.SetActive(false);
        activatedMinigame = 0;

        do { 
        livingProves[Random.Range(1, livingProves.Length)].SetActive(true);
            probeCount++;
        } 
        while (probeCount<10);

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
}
