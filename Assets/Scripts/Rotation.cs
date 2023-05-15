using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour
{
    public RectTransform transform;
    public float currentAngle;
    public float NewAngle;
    public int StabalizationAngle = 10;
    // Start is called before the first frame update
    void Start()
    {
        Input.compass.enabled = true;
        transform = gameObject.GetComponent<RawImage>().rectTransform;
        currentAngle = Input.compass.magneticHeading;
        //InvokeRepeating("UpdateRotation", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        NewAngle = Input.compass.magneticHeading;
        if(Mathf.Abs(Mathf.DeltaAngle(currentAngle, NewAngle)) > StabalizationAngle)
        {
            if (currentAngle < 90 && NewAngle > 270) NewAngle -= 360;
            if (NewAngle < 90 && currentAngle > 270) NewAngle += 360;
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, NewAngle),0.1f);
            currentAngle = transform.eulerAngles.z;
        }
       
    }

    /*void UpdateRotation()
    {
        NewAngle = Input.compass.magneticHeading;
    }*/
   
}
