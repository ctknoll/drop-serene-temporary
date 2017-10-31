using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStateController : MonoBehaviour 
{
    public State currentState;
    State expectedState;
    public AudioClip footstepClip;
    public GameObject player;
	public float distance = 25f;
	public float proximity = 4f;
    

    [Header("Roam State")]
    public Transform[] roamGoalNodes;

    [Header("Chase State")]
	public AudioClip moanSound;
	public AudioClip growlSound;
    public float chaseSpeed = 4.5f;

    [Header("Investigate State")]
    public Vector3 alertLocation;

    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public RoamState roamState;
    [HideInInspector]
    public InvestigateState investigateState;
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public readonly Vector3 vec3Null = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
    [HideInInspector]
    public AudioSource audioSource; 
	[HideInInspector]
	public List<Vector3> history = new List<Vector3>();
	AudioSource stateAudio;
	[HideInInspector]
	public float distanceToPlayer;

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

		// Audio shenanigans
		audioSource = GetComponent<AudioSource> ();
		stateAudio = GameObject.Find ("MonsterVoice").GetComponent<AudioSource>();

		// Start monster's footsteps (Always looping - since the monster should always be moving)
		StartCoroutine (MonsterStep ());
	}
	
	// Update is called once per frame
	void Update () 
	{	
		// Play moaning sound when the player gets to a certain distance from the monster
		distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
		if (distanceToPlayer <= 4.0f) {
			if(!stateAudio.isPlaying) {
				stateAudio.PlayOneShot (moanSound, 0.2f);
			}
		}
			
		currentState.OnStateUpdate();
        currentState.EvaluateTransition();
        if(!expectedState.Equals(currentState))
        {
            expectedState.OnStateExit();
            currentState.OnStateEnter();

			// Leaving chase state audio cue
			if (currentState != chaseState) {
				if(!stateAudio.isPlaying) {
					stateAudio.PlayOneShot (moanSound, 0.2f);
				}
			}
			// Entering chase state audio cue
			if (currentState == chaseState) {
				if(!stateAudio.isPlaying) {
					stateAudio.PlayOneShot (growlSound, 1);
				}
			}

            expectedState = currentState;
            alertLocation = vec3Null;
        }

        while (history.Count > 100)
            history.RemoveAt(0);
        if (!GamestateUtilities.isPaused)
            history.Add(gameObject.transform.position);

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
