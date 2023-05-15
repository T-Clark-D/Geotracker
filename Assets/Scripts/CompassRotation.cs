using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompassRotation : MonoBehaviour
{
    public RectTransform t;
    public static float currentAngle;
    public static float NewAngle;
    //private TextMeshProUGUI Compass;
    public int StabalizationAngle = 10;
    // Start is called before the first frame update
    void Start()
    {
        Input.compass.enabled = true;
        t = gameObject.GetComponent<RawImage>().rectTransform;
        currentAngle = Input.compass.magneticHeading;
        //InvokeRepeating("UpdateRotation", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        NewAngle = Input.compass.magneticHeading;
        if(Mathf.Abs(Mathf.DeltaAngle(currentAngle, NewAngle)) > StabalizationAngle)
        {
            if (currentAngle < 90 && NewAngle > 270)
            {
                //UIUpdater.UpdateError("CU:" + currentAngle + " NA:" + NewAngle + "-360");
                NewAngle -= 360;
            }
            else if (NewAngle < 90 && currentAngle > 270) {
                //UIUpdater.UpdateError("CU:" + currentAngle + " NA:" + NewAngle + "+360");
                NewAngle += 360;
            }
            transform.eulerAngles = Vector3.Lerp(new Vector3(0, 0, currentAngle), new Vector3(0, 0, NewAngle),0.05f);
            currentAngle = transform.eulerAngles.z;
        }
       
    }

    /*void UpdateRotation()
    {
        NewAngle = Input.compass.magneticHeading;
    }*/
   
}
