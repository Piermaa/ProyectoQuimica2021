using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        TurnoffCameras();
        _erlenmeyer = FindObjectOfType<Erlenmeyer>();
        _miniGame1Controller = FindObjectOfType<MiniGame1Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            FinishMinigame();
        }
    }

    public void StartMinigame(Camera minigameCamera,int activeMinigame)
    {
        minigameCamera.gameObject.SetActive(true);
        if (activatedMinigame != 2)
        {
            player.SetActive(false);
        }
        activatedMinigame=activeMinigame;
        
       
    }
    public void FinishMinigame()
    {
        player.SetActive(true);
        TurnoffCameras();
        activatedMinigame = 0;
        _miniGame1Controller.ExitMinigame();

        //state = GameState.Moving;
    }
    void TurnoffCameras()
    {
        MG1Camera.gameObject.SetActive(false);
        //MG2Camera.gameObject.SetActive(false);
        /*MG3Camera.gameObject.SetActive(false);
        MG4Camera.gameObject.SetActive(false);
        MG5Camera.gameObject.SetActive(false);*/
    }
}
