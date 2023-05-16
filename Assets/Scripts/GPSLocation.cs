using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using TMPro;
using System;

public class GPSLocation : MonoBehaviour
{
    private float longitude;
    private float latitude;
    private float lastLatitude;
    private float lastLongitude;
    private bool isInitialised = false;

    public static event Action<float,float> CoordinatesUpdated;

    public bool isUpdating;

    private void Update()
    {
        if (!isUpdating)
        {
            StartCoroutine(GetLocation());
            isUpdating = !isUpdating;
        }
    }

    //checks for permission then retreives location
    IEnumerator GetLocation()
    {
 
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield return new WaitForSeconds(0.5f);

        // Start service before querying location
        if (!isInitialised)
        {
            Input.location.Start();
        }
       

        // Wait until service initializes
        int maxWait = 10;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait <1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            longitude = Input.location.lastData.longitude;
            latitude = Input.location.lastData.latitude;
            //invokes coordinate update if the new coordinates are different
            if(latitude!=lastLatitude || longitude != lastLongitude)
            {
                CoordinatesUpdated?.Invoke(longitude, latitude);
                lastLatitude = latitude;
                lastLongitude = longitude;
            }
            //changes the games state after the first run of the coroutine
            if (!isInitialised)
            {
                //GameManager.Instance.UpdateGameStates(GameManager.GameState.InitilisatingMap);
                isInitialised = true;
            }
            
        }
        isUpdating = !isUpdating;
    }
}