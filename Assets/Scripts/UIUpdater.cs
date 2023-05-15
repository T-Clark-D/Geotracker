using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UIUpdater : MonoBehaviour
{
    public GameObject Coordinates;
    public GameObject Compass;
    public GameObject Distance;
    public GameObject ErrorText;

    private TextMeshProUGUI CoordinatesOut;
    private TextMeshProUGUI CompassOut;
    private TextMeshProUGUI DistanceOut;
    public static TextMeshProUGUI ErrorTextOut;

    //public GameObject GPSLocation;
    // Start is called before the first frame update

    private void OnEnable()
    {
        CoordinatesOut = Coordinates.GetComponent<TextMeshProUGUI>();
        CompassOut = Compass.GetComponent<TextMeshProUGUI>();
        DistanceOut = Distance.GetComponent<TextMeshProUGUI>();
        ErrorTextOut = ErrorText.GetComponent<TextMeshProUGUI>();
        //GPSLocation.NewLocation += UpdateLocation;
    } 
    
    void FixedUpdate()
    {
        CoordinatesOut.text = "Latitude: " + GPSLocation.latitude + "\nLongitude: " + GPSLocation.longitude;
        CompassOut.text = CompassRotation.NewAngle + "°";
        DistanceOut.text = CoordinateLogic.distance + "m";
        //ErrorTextOut.text = GPSLocation.ErrorText;
    }
}
