using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UIUpdater : MonoBehaviour
{
    public GameObject Longitude;
    public GameObject Latitude;
    public GameObject GPSStatus;

    private TextMeshProUGUI LongitudeText;
    private TextMeshProUGUI LatitudeText;
    private TextMeshProUGUI GPSStatusText;

    //public GameObject GPSLocation;
    // Start is called before the first frame update

    private void OnEnable()
    {
        LongitudeText = Longitude.GetComponent<TextMeshProUGUI>();
        LatitudeText = Latitude.GetComponent<TextMeshProUGUI>();
        GPSStatusText = GPSStatus.GetComponent<TextMeshProUGUI>();
        //GPSLocation.NewLocation += UpdateLocation;
    } 

    
    void UpdateLocation()
    {
        Debug.Log("we in updatelocation");
        //LongitudeText.text = GPSLocation.longitude;
        //LatitudeText.text = GPSLocation.latitude;
       // GPSStatusText.text = GPSLocation.GPSStatus;
    }
}
