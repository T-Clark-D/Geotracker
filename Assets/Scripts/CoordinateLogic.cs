using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateLogic : MonoBehaviour
{
    public static float generatedLongitude;
    public static float generatedLatitude;
    public static bool isGenerated = false;
    // Start is called before the first frame update

    public static void GeneratePointOfInterestInrange(int range)
    {
        float direction_degree = Random.Range(1, 90);
        float longitudeMeters = range * Mathf.Sin(direction_degree);
        float latitudeMeters = range * Mathf.Cos(direction_degree);
        float latcoef = latitudeMeters / 111320.0f;
        float longcoef = longitudeMeters / (111320.0f * Mathf.Cos(Mapbox.centerLatitude * (Mathf.PI / 180)));
        //Debug.Log(coef);
        generatedLatitude = Mapbox.centerLatitude + latcoef;
        generatedLongitude = Mapbox.centerLongitude + longcoef;
        Debug.Log(generatedLatitude + " : " + generatedLongitude);
        My_Calculate_Distance();
        isGenerated = true;
    }

    public static void My_Calculate_Distance()
    {
        float latdegrees = Mathf.Abs(Mapbox.centerLatitude - generatedLatitude);
        float longdegrees = Mathf.Abs(Mapbox.centerLongitude - generatedLongitude);
        float latmeters = latdegrees * 111320.0f;
        float longmeters = longdegrees * (111320.0f * Mathf.Cos(Mapbox.centerLatitude * (Mathf.PI / 180)));
        float distance = Mathf.Sqrt(Mathf.Pow(latmeters, 2) + Mathf.Pow(longmeters, 2));
        print(distance);
    }
}
