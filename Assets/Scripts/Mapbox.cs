using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Mapbox : MonoBehaviour
{

    public string accessToken;
    public float zoom = 17.0f;
    public int bearing = 0;
    public int pitch = 0;

    public enum style { Light, Dark, Streets, Outdoors, Satellite, SatelliteStreets };
    public style mapStyle = style.Streets;

    public enum resolution { low = 1, high = 2 };
    public resolution mapResolution = resolution.high;

    private float pointLatitude;
    private float pointLongitude;
    
    private float newLatitude;
    private float newLongitude;

    private int mapWidth;
    private int mapHeight;
    private string[] styleStr = new string[] { "light-v10", "dark-v10", "streets-v11", "outdoors-v11", "satellite-v9", "satellite-streets-v11" }; private string url = "";
    private Rect rect;

    // Start is called before the first frame update
    private void Awake()
    {
        GPSLocation.CoordinatesUpdated += UpdateGPSData;
        CoordinateLogic.PointOfInterestGenerated += UpdatePointOfInterest;
    }

    private void OnDestroy()
    {
        GPSLocation.CoordinatesUpdated -= UpdateGPSData;
        CoordinateLogic.PointOfInterestGenerated -= UpdatePointOfInterest;
    }

    void Start()
    {
        rect = gameObject.GetComponent<RawImage>().rectTransform.rect; 
        mapWidth = (int)Math.Round(rect.width);
        mapHeight = (int) Math.Round(rect.height);
    }
    private void UpdatePointOfInterest(float genLongitude, float genLatitude)
    {
        pointLatitude = genLatitude;
        pointLongitude = genLongitude;
        StartCoroutine(GetMapbox());
    }

    void UpdateGPSData(float longitude, float latitude)
    {
        newLongitude = longitude;
        newLatitude = latitude;
        StartCoroutine(GetMapbox());
    }

    IEnumerator GetMapbox()
    {
        url = "https://api.mapbox.com/styles/v1" + "/chaotic-clark/clhpkdq5y01h301p68v7fadhf" + "/static/"+ "pin-l+ff0000("+ pointLongitude + ","+ pointLatitude + ")/"+ newLongitude + "," + newLatitude + "," + zoom + "," + 0 + ","+ pitch + "/" +mapWidth + "x" + mapHeight + "?" + "access_token=" + accessToken;
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
