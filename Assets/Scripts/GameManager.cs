using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject GreenSquare;


    void Awake()
    {
        Instance = this;
        CoordinateLogic.DistanceAndAngleCalculated += GameLoop;
    }

    private void GameLoop(float distance, float angle)
    {
        if(distance < 10)
        {
            GreenSquare.SetActive(true);
            Handheld.Vibrate();
        }
        else
        {
            GreenSquare.SetActive(false);
        }
    }
  
}
