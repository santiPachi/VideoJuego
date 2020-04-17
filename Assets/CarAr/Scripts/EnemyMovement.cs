using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;










public class EnemyMovement : MonoBehaviour {
    public Transform[] waypoints;
    private int currentTargetIndex;
    private NavMeshAgent nav;
	void Start () {
        nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
       if (Landed()) {
           NextPoint();
       }
       UpdateDestinationAgent(waypoints[currentTargetIndex]);
       if(GameManager.instance.StopMovement){
           nav.Stop();
       }
	}
    public void NextPoint(){
        currentTargetIndex = (currentTargetIndex < waypoints.Length-1)? currentTargetIndex + 1 : 0;
    }
    public void UpdateDestinationAgent(Transform target){
        nav.destination = (target.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, 0.05f);
    }

    public bool Landed(){
        return nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending;
    }

    

}
