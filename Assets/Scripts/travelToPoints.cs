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
        for(int i = 0; i < path.corners.Length -1; i++)
        {
            ParticleSystem newParticle = Instantiate(particleRef, new Vector3 (path.corners[i].x, path.corners[i].y+1, path.corners[i].z) , new Quaternion(0,0,0,1 ));
            if (path.corners[i + 1] != null)
            {
                newParticle.transform.LookAt(path.corners[i + 1]);
                float hypotenuseLength = Vector2.Distance(new Vector2(path.corners[i].x, path.corners[i].z), new Vector2(path.corners[i + 1].x, path.corners[i + 1].z));
                float oppositeLength = path.corners[i + 1].z - path.corners[i].z;
                float sinHorizontal = oppositeLength / hypotenuseLength;
                Debug.Log(sinHorizontal);
                ///newParticle.startRotation3D = new Vector3 (0 , 0, 0);
            }
        }
    }

    void Update()
    {
   
        
    }
    
}
