using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSLocation : MonoBehaviour
{
    public static string GPSStatus; 
    public static string longitude;
    public static string latitude;
    //  public Text altitudeValue; 
    // public Text horizontalAccuracyValue;
    // public Text timestampValue;
    // Start is called before the first frame update

    public static event Action NewLocation;

    void Start()
    {
        GPSStatus = "...";
        longitude = "...";
        latitude = "...";
        Debug.Log("we before invoke");
        NewLocation?.Invoke();
        Debug.Log("we after invoke");
        StartCoroutine(GPSLoc());
    }

    // Update is called once per frame
    void Update()
    {
        NewLocation?.Invoke();
    }

    IEnumerator GPSLoc()
    {


        // check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // start service before querying location
        Input.location.Start();

        //wait until service initailze
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        
        // service didn't init in 20 sec
        if (maxWait < 1)
        {
            GPSStatus = "Time out";
            yield break;
        }
        // connection failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GPSStatus = "Unable to determine device location";
            yield break;
        }
        else
        {
            //Access granted
            GPSStatus = "Running";
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        }
    }
    private void UpdateGPSData()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            //access granted to GPS values
            GPSStatus = "Running";
            latitude = Input.location.lastData.latitude.ToString();
            longitude = Input.location.lastData.longitude.ToString();
            NewLocation?.Invoke();
            // altitudeValue.text = Input.location.lastData.altitude.ToString();
            // horizontalAccuracyValue.text = Input.location.lastData.horizontalAccuracy.ToString(); 
            // timestampValue.text = Input.location.lastData.timestamp.ToString();

            //Mapbox.Instance.centerLatitude = Input.location.lastData.latitude;
            //Mapbox.Instance.centerLongitude = Input.location.lastData.longitude;
        }
        else
        {
            //service is stopped
        }
    }
}
