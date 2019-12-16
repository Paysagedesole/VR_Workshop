using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class travelToNextPoint_Script : MonoBehaviour {
    [HideInInspector]
    public List<Vector3> listOfPoints; //list of all the points to travel to
    Vector3 nextPointToGo; // where the particle has to go next
    public int actualPointIndex; //where the particle is before deciding where to go after
    float distanceToStop; //distance min to stop going forward
    float speed = 0.1f;

	// Use this for initialization
	void Start () {
        goAhead();
        distanceToStop = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {

		if( Vector3.Distance(transform.position , nextPointToGo) < distanceToStop)
        {
            //goAhead();
        }
	}

    void goAhead()
    {
        /*actualPointIndex++; // increment
        nextPointToGo = listOfPoints[actualPointIndex + 1];
        transform.LookAt(nextPointToGo);
        transform.Translate(0, 0, speed);*/
        Debug.Log(actualPointIndex);
        
        
    }
}
