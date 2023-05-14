using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using TMPro;

public class GPSLocation : MonoBehaviour
{
    public static float longitude;
    public static float latitude;
    public static int bearing;
    public GameObject gpsText;
    private TextMeshProUGUI gpsOut;
    public bool isUpdating;

    private void Start()
    {
        gpsOut = gpsText.GetComponent<TextMeshProUGUI>();
        gpsOut.text = "default";
    }
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
        Input.location.Start();

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
            gpsOut.text = "Timed out";
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            gpsOut.text = "Unable to determine device location";
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            longitude = Input.location.lastData.longitude;
            latitude = Input.location.lastData.latitude;

            gpsOut.text = "Location: " + latitude + " " + longitude + " " + Input.location.lastData.altitude + 100f + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp + " " + bearing;
            // Access granted and location value could be retrieved
            print("Location: " + latitude + " " + longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            longitude = Input.location.lastData.longitude;
            latitude = Input.location.lastData.latitude;
        }

        // Stop service if there is no need to query location updates continuously
        
        isUpdating = !isUpdating;
        Input.location.Stop();
    }
}