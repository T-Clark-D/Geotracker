using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using TMPro;

public class GPSLocation : MonoBehaviour
{
    public static float longitude;
    public static float latitude;
    private bool isInitialised = false;
    static int count = 0;

    public bool isUpdating;

    private void Update()
    {
        if (!isUpdating)
        {
            StartCoroutine(GetLocation());
            isUpdating = !isUpdating;
        }
    }
    IEnumerator GetLocation()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield return new WaitForSeconds(10);

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
         //   gpsOut.text = "Timed out";
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
         //   gpsOut.text = "Unable to determine device location";
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            longitude = Input.location.lastData.longitude;
            latitude = Input.location.lastData.latitude;
            count++;
            //UIUpdater.ErrorTextOut.text = count.ToString();
            if (!isInitialised)
            {
                GameManager.Instance.UpdateGameStates(GameManager.GameState.InitilisatingMap);
                isInitialised = true;
            }
        }

        // Stop service if there is no need to query location updates continuously
        isUpdating = !isUpdating;
        //Input.location.Stop();
    }
}