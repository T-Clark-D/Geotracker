using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateLogic : MonoBehaviour
{
    public static float generatedLongitude;
    public static float generatedLatitude;
    public static float distance;
    public static float angle = 0;
    // Start is called before the first frame update

    public static void GeneratePointOfInterestInrange(int range)
    {
        float direction_degree = Random.Range(1, 90);
        float longitudeMeters = range * Mathf.Sin(direction_degree);
        float latitudeMeters = range * Mathf.Cos(direction_degree);
        float latcoef = latitudeMeters / 111320.0f;
        float longcoef = longitudeMeters / (111320.0f * Mathf.Cos(GPSLocation.latitude * (Mathf.PI / 180)));
        //Debug.Log(coef);
        generatedLatitude = GPSLocation.latitude + latcoef;
        generatedLongitude = GPSLocation.longitude + longcoef;
        Debug.Log(generatedLatitude + " : " + generatedLongitude);
    }

    public static void CalculateDistance()
    {
        float latdegrees = Mathf.Abs(GPSLocation.latitude - generatedLatitude);
        float longdegrees = Mathf.Abs(GPSLocation.longitude - generatedLongitude);
        float latmeters = latdegrees * 111320.0f;
        float longmeters = longdegrees * (111320.0f * Mathf.Cos(GPSLocation.latitude * (Mathf.PI / 180)));
        distance = Mathf.Sqrt(Mathf.Pow(latmeters, 2) + Mathf.Pow(longmeters, 2));
        
        if (generatedLongitude - GPSLocation.longitude >= 0 && generatedLatitude - GPSLocation.latitude > 0) //First Quadrant
        {
            //soh cah toa
            angle = Mathf.Atan(longmeters / latmeters) * Mathf.Rad2Deg;
        }
        else if(generatedLongitude - GPSLocation.longitude > 0 && generatedLatitude - GPSLocation.latitude  <= 0) //Second Quadrant
        {
            angle = Mathf.Atan(latmeters / longmeters) * Mathf.Rad2Deg + 90;
        }
        else if(generatedLongitude - GPSLocation.longitude <= 0 && generatedLatitude - GPSLocation.latitude < 0) //Third Quadrant
        {
            angle = Mathf.Atan(longmeters / latmeters) * Mathf.Rad2Deg + 180;
        }
        else if(generatedLongitude - GPSLocation.longitude < 0 && generatedLatitude - GPSLocation.latitude >= 0) //Fourth Quadrant
        {
            angle = Mathf.Atan(latmeters / longmeters) * Mathf.Rad2Deg + 270;
        }
    }
}
