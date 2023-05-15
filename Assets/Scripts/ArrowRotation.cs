using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation : MonoBehaviour
{
    public GameObject MapBox;
    void FixedUpdate()
    {
        UIUpdater.ErrorTextOut.text = CoordinateLogic.angle + " <-angle " ;
        transform.eulerAngles = new Vector3(0, 0, (MapBox.transform.eulerAngles.z - CoordinateLogic.angle));
       //transform.rotation = Quaternion.Euler(0, 0, (-CoordinateLogic.angle * 100));
    }
}
