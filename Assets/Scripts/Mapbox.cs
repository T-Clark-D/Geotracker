using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Mapbox : MonoBehaviour
{

    public string accessToken;
    public static float centerLatitude = 45.49224f;
    public static float centerLongitude = -73.56439f;
    public float zoom = 17.0f;
    public int bearing = 0;
    public int pitch = 0;

    public enum style { Light, Dark, Streets, Outdoors, Satellite, SatelliteStreets };
    public style mapStyle = style.Streets;

    public enum resolution { low = 1, high = 2 };
    public resolution mapResolution = resolution.low;

    private int mapWidth = 800;
    private int mapHeight = 600;
    private string[] styleStr = new string[] { "light-v10", "dark-v10", "streets-v11", "outdoors-v11", "satellite-v9", "satellite-streets-v11" }; private string url = "";
    private Rect rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = gameObject.GetComponent<RawImage>().rectTransform.rect; 
        mapWidth = (int)Math.Round(rect.width);
        mapHeight = (int) Math.Round(rect.height);
        InvokeRepeating("UpdateGPSData", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void UpdateGPSData()
    {
        rect = gameObject.GetComponent<RawImage>().rectTransform.rect;
        mapWidth = (int)Math.Round(rect.width);
        mapHeight = (int)Math.Round(rect.height);
        StartCoroutine(GetMapbox());
        //generates point if not generated allready
        if(CoordinateLogic.isGenerated == false)
        {
            CoordinateLogic.GeneratePointOfInterestInrange(200);
        }
    }

    IEnumerator GetMapbox()
    {
        if(GPSLocation.longitude != 0)
        { 
            centerLatitude = GPSLocation.latitude;
            centerLongitude = GPSLocation.longitude;
        }

        url = "https://api.mapbox.com/styles/v1/mapbox/" + styleStr[(int)mapStyle] + "/static/"+ "pin-l+ff0000("+CoordinateLogic.generatedLongitude+","+CoordinateLogic.generatedLatitude+")/"+ centerLongitude + "," +centerLatitude + "," + zoom + "," + 0 + ","+ pitch + "/" +mapWidth + "x" + mapHeight + "?" + "access_token=" + accessToken;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url); 
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("WWW ERROR:" + www.error);
        }
        else
        {
            gameObject.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }
}
