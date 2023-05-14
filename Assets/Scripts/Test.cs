using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Test : MonoBehaviour
{
    //[SerializeField] private UnityEvent updateLocation;

    public static event Action NewLocation;

    // Start is called before the first frame update
    void Start()
    {
        NewLocation?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        NewLocation?.Invoke();
    }
}
