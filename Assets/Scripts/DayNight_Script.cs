using UnityEngine;
using System.Collections;

public class DayNight_Script : MonoBehaviour
{

    public Light sun;
    public float secondsInFullDay = 120f;
    [Range(0, 1)]
    public float currentTimeOfDay = 0;
    Color sunColor;
    public bool isNight;



    float sunInitialIntensity;

    void Start()
    {
        

    }

    void Update()
    {
        
        //ROTATION--------------------------------------------------------------------------------------------------------
        currentTimeOfDay += (Time.deltaTime / secondsInFullDay);

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
        }
        transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f), 0,  0 );
        //COLOR----------------------------------------------------
        sunColor.r = 1;
        sunColor.g = currentTimeOfDay + 0.5f ;
        sunColor.b = currentTimeOfDay + 0.5f;
        sunColor.a = 1;
        sun.color = sunColor;
        if (currentTimeOfDay > 0.58f )
        {
            isNight = true;
        }
        else
        {
            isNight = false;
        }
    }

   
       
    
}