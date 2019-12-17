using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Smoke_Script : MonoBehaviour {

    public Transform grabTransform;
    public bool isSmoking;
    public ParticleSystem smokeParticleRef;
    public Transform transformmRef;
    [Range(0, 500)] int smokeQuantity; //How much smoke u have in mouth
    // Use this for initialization




    void Start ()
    {
        smokeQuantity = 0;
        isSmoking = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isSmoking == true)
        {
            smokeQuantity += 1;
        }
    }

    public void UseSmoke()
    {
        VRTK_DeviceFinder.GetControllerHand(this.gameObject);
        float distance = Vector3.Distance(grabTransform.position , VRTK_DeviceFinder.HeadsetTransform().position);
        if ( distance <= 0.2F  ) //True si le controlleur droit est assez proche de la tête
        {

            isSmoking = true;
        }
        
        
    }

    public void UnUseSmoke()
    {
        isSmoking = false;
        ParticleSystem particlesSystemInstantiateRef = Instantiate(smokeParticleRef, VRTK_DeviceFinder.HeadsetTransform().position - new Vector3(0, 0.5F, 0), VRTK_DeviceFinder.HeadsetTransform().rotation );
        particlesSystemInstantiateRef.emissionRate = smokeQuantity; //it works
        Debug.Log(particlesSystemInstantiateRef);
        smokeQuantity = 0;
        
    }


}
