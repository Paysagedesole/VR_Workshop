using UnityEngine;
using UnityEngine.AI;

public class travelToPoints : MonoBehaviour
{
    public Transform target;
    private NavMeshPath path;
    public ParticleSystem particleRef;
    
    void Start()
    {
        path = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
        for(int i = 0; i < path.corners.Length - 2; i++)
        {
            ParticleSystem newParticle = Instantiate(particleRef, new Vector3 (path.corners[i].x, path.corners[i].y+0.5f, path.corners[i].z) , new Quaternion(0,0,0,1 ));
            travelToNextPoint_Script newParticleScriptRef = newParticle.GetComponent<travelToNextPoint_Script>();
            newParticleScriptRef.listOfPoints.Add (path.corners[i]);
        }
    }

    void Update()
    {
   
        
    }
    
}
