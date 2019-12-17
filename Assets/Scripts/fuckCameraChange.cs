using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuckCameraChange : MonoBehaviour {
    
    public Camera fuckCameraRef;
    public Camera playerCameraRef;
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CameraChange()
    {
        
        if (playerCameraRef.enabled == true)
        {
            playerCameraRef.enabled = false;
            fuckCameraRef.enabled = true;
        }
        else
        {
            fuckCameraRef.enabled = false;
            playerCameraRef.enabled = true;
        }
       

    }
}
