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


    private void OnEnable()
    {
        CoordinatesOut = Coordinates.GetComponent<TextMeshProUGUI>();
        CompassOut = Compass.GetComponent<TextMeshProUGUI>();
        DistanceOut = Distance.GetComponent<TextMeshProUGUI>();
        ErrorTextOut = ErrorText.GetComponent<TextMeshProUGUI>();
    } 
    
    void FixedUpdate()
    {
        CoordinatesOut.text = "Latitude: " + GPSLocation.latitude + "\nLongitude: " + GPSLocation.longitude;
        CompassOut.text = Mathf.RoundToInt(CompassRotation.NewAngle) + "°";
        DistanceOut.text = (Mathf.Round(CoordinateLogic.distance * 100)) / 100.0 + "m";
    }
}
