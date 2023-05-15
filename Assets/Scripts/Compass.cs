using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public GameObject MapBox;
    void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(0, 0, (MapBox.transform.eulerAngles.z));
    }
}
