using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame_Candle : MonoBehaviour {
    float randomIntensity;
    float actualIntensity;
    // Use this for initialization
    void Start ()
    {
        actualIntensity = GetComponent<Light>().intensity;
	}
	
	// Update is called once per frame
	void Update ()
    {
        randomIntensity = Mathf.Clamp (Random.Range( actualIntensity -0.1f ,actualIntensity +0.1f), 0.5f, 1f);
        GetComponent<Light>().intensity = randomIntensity;
        actualIntensity = GetComponent<Light>().intensity;


    }
}
