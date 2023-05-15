using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static float distance;

    public GameObject GEOLocationObj;
    public GameObject MapBoxObj;
    public GameObject GreenSquare;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameStates(GameState.InitilisationLocationServices);
    }


    public void UpdateGameStates(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.InitilisationLocationServices:
                HandleLocationInitialization();
                break;
            case GameState.InitilisatingMap:
                HandleMapInitialisation();
                break;
            case GameState.RunningGameLoop:
                InvokeRepeating("GameLoop", 0.5f, 1f);
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }
    private void GameLoop()
    {
        CoordinateLogic.CalculateDistance();
        if(CoordinateLogic.distance < 10)
        {
            GreenSquare.SetActive(true);
        }
        else
        {
            GreenSquare.SetActive(false);
        }
    }
    private void HandleMapInitialisation()
    {
        CoordinateLogic.GeneratePointOfInterestInrange(200);
        CoordinateLogic.CalculateDistance();
        MapBoxObj.SetActive(true);
        UpdateGameStates(GameState.RunningGameLoop);
    }

    private void HandleLocationInitialization()
    {
        GEOLocationObj.SetActive(true);
        //Update to state change is Done when in intialsation is complete from within GeoLocation
    }

    public enum GameState
    {
        InitilisationLocationServices,
        InitilisatingMap,
        RunningGameLoop
    }
}
