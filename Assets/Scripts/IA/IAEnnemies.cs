using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
//IF the following errors happens it is because there is too much colliders around the ennemi just put the collider number in Detection() to 200
//IndexOutOfRangeException: Array index is out of range.
//IAEnnemies.Detection (UnityEngine.Transform checkingobject, UnityEngine.Transform target, Single maxAngle, Single maxRadius) (at Assets/Scripts/IA/IAEnnemies.cs:325)
public class IAEnnemies : MonoBehaviour {
    //public GameManager manager;
    public NavMeshAgent agent;
    public string states;
    //Walk Points
    public Transform originPos;
    public Transform secondPos;

    //Distance 
    private float closeDistance = 1;

    //Destination
    public Transform destination;

    //Sleep booléan
    private bool isSleeping = false;
    //Wait Booléean
    bool isWaitEnded; //Ennemie wait ended
    bool isWaiting; // Ennemie is waiting
    bool isWaitChangeSceneEnded;

    //Stats
        public float speed;
        //Field of vue Angle
        public float maxAngle;
        //Field of vue Distance
        public float maxRadius;
        public float damage;
        //Time Wait Watching
        public int waitAtPosMin;
        public int waitAtPosMax;
        //Range of Ennemis Hears
        public float ennemiesHearRange;

    //IADetection
    private Transform playerLastTransform;
    public bool isPlayerDetected;
    bool isOldPlayerDetected = false;

    //Player
    public GameObject PlayerObject;
    // Use this for initialization
    void Start ()
    {
        states = "Walk";
        isWaitChangeSceneEnded = false;
        destination.position = secondPos.position;
    }
    // Update is called once per frame
    void Update () {
        PlayerDetector();
        IAStates(); 
    }
    void PlayerDetector()
    {
        //Detect Player
        if (PlayerObject != null)
        {
            
                    isPlayerDetected = true;
                }
        else
        {
            isPlayerDetected = false;
        }
    }
      
    //States Manager
    private void IAStates()
    {
        //Conditions
        switch (states)
        {
            case "Watch":
                if (isSleeping)
                {
                    states = "Sleep";
                }
                if (isPlayerDetected)
                {
                    states = "ChasePlayer";
                }
                else if (isWaitEnded)
                {
                    states = "Walk";
                }
                break;
            case "Walk":
                if (isSleeping)
                {
                    states = "Sleep";
                }
                if (isPlayerDetected)
                {
                    states = "ChasePlayer";
                }
                //Back and Forth between origin Pos and Second Pos
                if (destination.position == originPos.position)
                {
                    if (CheckDistance(destination, closeDistance))
                    {
                        states = "Watch";
                    }
                }
                //If Chase player stop when unfound
                else if (CheckDistance(destination, closeDistance))
                {
                    states = "Watch";
                }
                //if Detect PLayer CHase him
                break;
            case "ChasePlayer":
                if (isSleeping)
                {
                    states = "Sleep";
                }
                if (CheckDistance(PlayerObject.transform, closeDistance))
                {
                    if(isPlayerDetected)
                    {
                        states = "Watch";
                    }
                    else
                    {
                        states = "Attack";
                    }
                }
                else if (CheckDistance(destination, closeDistance))
                {
                    states = "Watch";
                }
                if (isPlayerDetected)
                {
                    states = "Walk";
                }
                break;
            case "Attack":
                if (isSleeping)
                {
                    states = "Sleep";
                }
                if (PlayerObject == null)
                {
                    states = "Watch";
                }
                break;
            case "Sleep":
                if (!isSleeping)
                {
                    states = "Watch";
                }
                break;
            default:
                break;
        }
        //States
        switch (states)
        {
            case "Watch":
                agent.speed = 0;
                // Si l'enemi cherche ou observe, il ne relance pas un attente
                if (!isWaiting)
                {
                    StartCoroutine(WaitAtPos());
                }
                // Si l'enemi a finis de chercher, l'attente passe à terminé
                if (isWaitEnded)
                {
                    isWaitEnded = false;
                }
                if (isWaitChangeSceneEnded)
                {
                    //manager.GameOver();
                }
                // Si le player est repéré et qu'il n'est pas invisible 
                if (isPlayerDetected)
                {
                    agent.SetDestination(PlayerObject.transform.position);
                }
                break;
            case "Walk":
                agent.speed = speed;
                agent.SetDestination(destination.position);
                if (destination.position == originPos.position)
                {
                    if (CheckDistance(destination, closeDistance))
                    {
                        destination.position = secondPos.position;
                        StartCoroutine(WaitAtPos());
                    }
                }
                else if (destination.position == secondPos.position)
                {
                    if (CheckDistance(destination, closeDistance))
                    {
                        destination.position = originPos.position;
                        StartCoroutine(WaitAtPos());
                    }
                }
                //If Chase player stop when unfound
                else if (CheckDistance(destination, closeDistance))
                {
                    destination.position = originPos.position;
                    StartCoroutine(WaitAtPos());
                }
                if (isWaitChangeSceneEnded)
                {
                    //manager.GameOver();
                }
                if (isPlayerDetected)
                {
                    agent.SetDestination(PlayerObject.transform.position);
                }
                break;
            case "ChasePlayer":
                agent.speed = speed;
                agent.SetDestination(destination.position);
                if (CheckDistance(destination, closeDistance))
                {
                    StartCoroutine(WaitAtPos());
                }
                if (isPlayerDetected)
                {
                    destination.position = PlayerObject.transform.position;
                }
                else
                {
                    if (playerLastTransform != null)
                    {
                        destination.position = playerLastTransform.position;
                    }
                }
                break;
            case "Attack":
                if (PlayerObject == null)
                {
                    StartCoroutine(WaitAtPos());
                }
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    isWaitChangeSceneEnded = false;
                    StartCoroutine(WaitChangeScene());
                }
                Destroy(PlayerObject);
                destination.position = originPos.position;
                agent.SetDestination(destination.position);
                break;
            case "Sleep":
                agent.speed = 0;
                agent.SetDestination(originPos.position);
                break;
            default:
                break;
        }
    }
    //Check if distance between this object and the "target" is less than "distance"
    private bool CheckDistance(Transform target, float distance)
    {
        if(target != null)
        {
            if (Vector3.Distance(target.position, transform.position) < distance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        
    }
    IEnumerator WaitAtPos()
    {
        isWaiting = true;
        isWaitEnded = false;
        yield return new WaitForSeconds(Random.Range(waitAtPosMin, waitAtPosMax));
        isWaitEnded = true;
        isWaiting = false;

    }
    IEnumerator WaitChangeScene()
    {
        yield return new WaitForSeconds(2);
        isWaitChangeSceneEnded = true;

    }
    public IEnumerator WaitSleep(float time)
    {
        isSleeping = true;
        states = "Sleep";
        yield return new WaitForSeconds(time);
        isSleeping = false;
    }
    //Draw differents Gizmos like hear range or field of vue
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, ennemiesHearRange);

        Vector3 ennemieLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        Vector3 ennemieLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, ennemieLine1);
        Gizmos.DrawRay(transform.position, ennemieLine2);
        if (isPlayerDetected)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        if(PlayerObject != null)
        {
            Gizmos.DrawRay(transform.position, (PlayerObject.transform.position - transform.position).normalized * maxRadius);
        }
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * maxRadius);
    }
    //Detect if player is inside the field of vue
    public static bool Detection(Transform checkingobject, Transform target, float maxAngle, float maxRadius)
    {
        Collider[] overlaps = new Collider[100];
        int count = Physics.OverlapSphereNonAlloc(checkingobject.position, maxRadius, overlaps);

        for (int i = 0; i < count + 1; i++)
        {
            if (overlaps[i] != null)
            {
                if (overlaps[i].transform == target)
                {
                    Vector3 directionBetween = (target.position - checkingobject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(checkingobject.forward, directionBetween);
                    if (angle <= maxAngle)
                    {
                        Ray ray = new Ray(checkingobject.position, target.position - checkingobject.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, maxRadius))
                        {
                            if (hit.transform == target)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
}
