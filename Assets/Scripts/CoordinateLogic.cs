using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoordinateLogic : MonoBehaviour
{
    public static float angle = 0;

    private float generatedLongitude;
    private float generatedLatitude;
    private float distance;

    private bool isGenerated = false;

    public static event Action<float, float> PointOfInterestGenerated;
    public static event Action<float, float> DistanceAndAngleCalculated;

    private void Awake()  
    {
        GPSLocation.CoordinatesUpdated += CalculateDistanceAndAngle;
    }

    private void OnDestroy()
    {
        GPSLocation.CoordinatesUpdated -= CalculateDistanceAndAngle;
    }

    public void GeneratePointOfInterestInrange(float longitude, float latitude, int range)
    {
        float direction_degree = UnityEngine.Random.Range(1, 90);
        float longitudeMeters = range * Mathf.Sin(direction_degree);
        float latitudeMeters = range * Mathf.Cos(direction_degree);
        float latcoef = latitudeMeters / 111320.0f;
        float longcoef = longitudeMeters / (111320.0f * Mathf.Cos(latitude * (Mathf.PI / 180)));

        generatedLatitude = latitude + latcoef;
        generatedLongitude = longitude + longcoef;

        PointOfInterestGenerated?.Invoke(generatedLongitude,generatedLatitude);
    }

    public void CalculateDistanceAndAngle(float longitude, float latitude)
    {
        //if no point is generated generate it
        if (!isGenerated)
        {
            GeneratePointOfInterestInrange(longitude, latitude, 200);
            isGenerated = true;
        }

        //Distance calculations
        float latdegrees = Mathf.Abs(latitude - generatedLatitude);
        float longdegrees = Mathf.Abs(longitude - generatedLongitude);
        float latmeters = latdegrees * 111320.0f;
        float longmeters = longdegrees * (111320.0f * Mathf.Cos(latitude * (Mathf.PI / 180)));

        distance = Mathf.Sqrt(Mathf.Pow(latmeters, 2) + Mathf.Pow(longmeters, 2));
       
        //Angle calculations
        if (generatedLongitude - longitude >= 0 && generatedLatitude - latitude > 0) //First Quadrant
        {
            angle = Mathf.Atan(longmeters / latmeters) * Mathf.Rad2Deg;
        }
        else if(generatedLongitude - longitude > 0 && generatedLatitude - latitude  <= 0) //Second Quadrant
        {
            angle = Mathf.Atan(latmeters / longmeters) * Mathf.Rad2Deg + 90;
        }
        else if(generatedLongitude - longitude <= 0 && generatedLatitude - latitude < 0) //Third Quadrant
        {
            angle = Mathf.Atan(longmeters / latmeters) * Mathf.Rad2Deg + 180;
        }
        else if(generatedLongitude - longitude < 0 && generatedLatitude - latitude >= 0) //Fourth Quadrant
        {
            angle = Mathf.Atan(latmeters / longmeters) * Mathf.Rad2Deg + 270;
        }
        DistanceAndAngleCalculated?.Invoke(distance, angle);
    }
}
