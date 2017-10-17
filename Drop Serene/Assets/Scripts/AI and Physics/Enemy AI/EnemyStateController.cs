using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStateController : MonoBehaviour 
{
	
    public Transform[] roamGoalNodes;

    public State currentState;
    State expectedState;

    [HideInInspector]
    public NavMeshAgent agent;
    public RoamState roamState;
    public InvestigateState investigateState;
    public ChaseState chaseState;

    public readonly Vector3 vec3Null = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
    public Vector3 alertLocation;
	public GameObject foundPlayer;

	AudioSource audioSource;
	
	public GameObject player;

    // Use this for initialization
    void Start () 
	{
        agent = GetComponent<NavMeshAgent>();
        alertLocation = vec3Null;

        roamState = (RoamState)State.CreateState("RoamState", this);
        investigateState = (InvestigateState)State.CreateState("InvestigateState", this);
        chaseState = (ChaseState)State.CreateState("ChaseState", this);

        currentState = roamState;
        expectedState = roamState;
        currentState.OnStateEnter();

		player = GameObject.Find("Player");
		Debug.Log ("Found player? " + player);
		audioSource = GetComponent<AudioSource> ();

		// Start monster's footsteps (Always looping - since the monster should always be moving)
		StartCoroutine (MonsterStep ());
	}
	
	// Update is called once per frame
	void Update () 
	{	
		currentState.OnStateUpdate();
        currentState.EvaluateTransition();
        if(!expectedState.Equals(currentState))
        {
            expectedState.OnStateExit();
            currentState.OnStateEnter();
            expectedState = currentState;
            alertLocation = vec3Null;
			foundPlayer = null;
        }

		Debug.Log ("Monster Line of sight " + LightingUtils.inLineOfSight (gameObject, player.gameObject));
	}

    public void heardNoise(Vector3 location) {alertLocation = location;}

	// Play monster's footsteps, 2 speeds
	IEnumerator MonsterStep(){
		while (true) {
			audioSource.Play ();
			if (currentState == roamState || currentState == investigateState){
				yield return new WaitForSeconds (0.75f);
			} else {
				yield return new WaitForSeconds (0.25f);
			}
		}
	}
}
