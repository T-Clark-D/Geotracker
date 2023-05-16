using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompassRotation : MonoBehaviour
{
    private float currentAngle;
    public static float NewAngle;
    public int StabalizationAngle = 10;

    void Start()
    {
        Input.compass.enabled = true;
        currentAngle = Input.compass.magneticHeading;
    }

    void FixedUpdate()
    {
        NewAngle = Input.compass.magneticHeading;
        if(Mathf.Abs(Mathf.DeltaAngle(currentAngle, NewAngle)) > StabalizationAngle)
        {
            if (currentAngle < 90 && NewAngle > 270)
            {
                NewAngle -= 360;
            }
            else if (NewAngle < 90 && currentAngle > 270) {
                NewAngle += 360;
            }
            transform.eulerAngles = Vector3.Lerp(new Vector3(0, 0, currentAngle), new Vector3(0, 0, NewAngle),0.05f);
            currentAngle = transform.eulerAngles.z;
        }
    }
   
}
