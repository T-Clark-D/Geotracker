using System;
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

    private void Awake()
    {
        GPSLocation.CoordinatesUpdated += UpdateGPSText;
        CoordinateLogic.DistanceAndAngleCalculated += UpdateDistanceText;
    }

    private void OnDestroy()
    {
        GPSLocation.CoordinatesUpdated -= UpdateGPSText;
        CoordinateLogic.DistanceAndAngleCalculated -= UpdateDistanceText;
    }
    private void OnEnable()
    {
        CoordinatesOut = Coordinates.GetComponent<TextMeshProUGUI>();
        CompassOut = Compass.GetComponent<TextMeshProUGUI>();
        DistanceOut = Distance.GetComponent<TextMeshProUGUI>();
        ErrorTextOut = ErrorText.GetComponent<TextMeshProUGUI>();
    }

    private void UpdateGPSText(float longitude, float latitude)
    {
        CoordinatesOut.text = "Latitude: " + latitude + "\nLongitude: " + longitude;
    }

    private void UpdateDistanceText(float distance, float angle)
    {
        DistanceOut.text = (Mathf.Round(distance * 100)) / 100.0 + "m";
    }

    private void FixedUpdate()
    {   
        //this is gonna happen pretty often so its better to access it directly instead of sending events
        CompassOut.text = Mathf.RoundToInt(CompassRotation.NewAngle) + "°";
    }
}
