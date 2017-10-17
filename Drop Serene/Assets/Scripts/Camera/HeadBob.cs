using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.Utility;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class HeadBob : MonoBehaviour
    {        
        public Camera Camera;
        public GameObject playerController;
        public CurveControlledBob motionBob = new CurveControlledBob();
        public LerpControlledBob jumpAndLandingBob = new LerpControlledBob();
        public float StrideInterval;
        public AudioSource audio;
        public AudioClip walkStep;
        public AudioClip sprintStep;
        private bool m_PreviouslyGrounded;
        private bool midstep = false;
        private Vector3 m_OriginalCameraPosition;
        public Vector2 footstepVolume;


        private void Start()
        {
			playerController = GameObject.Find("Player");
            Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            m_OriginalCameraPosition = Camera.transform.localPosition;
            motionBob.Setup(Camera, StrideInterval);
            audio = GetComponentInParent<AudioSource>();            
        }


        private void Update()
        {
            Vector3 newCameraPosition;

			AnimationCurve currentCurve = playerController.GetComponent<PlayerMovement>().isSprinting? motionBob.runningCurve : motionBob.walkingCurve;
            //Walking/Sprinting - step at lowest point in DoHeadBob cycle
            if (playerController.GetComponent<CharacterController>().velocity.magnitude > 0 && playerController.GetComponent<PlayerMovement>().isGrounded())
            {
                Camera.transform.localPosition = motionBob.DoHeadBob(playerController.GetComponent<CharacterController>().velocity.magnitude / (playerController.GetComponent<PlayerMovement>().isSprinting ? playerController.GetComponent<PlayerMovement>().sprintMultiplier : 1f));
                //Debug.Log(motionBob.Bobcurve.Evaluate(motionBob.m_CyclePositionY));
				if ((currentCurve.Evaluate(motionBob.m_CyclePositionY) - motionBob.m_Time) <= .02F && !midstep)
                {
                    midstep = true;
                    StartCoroutine("playFootsteps");
                    float sprintMod = playerController.GetComponent<PlayerMovement>().isSprinting ? playerController.GetComponent<PlayerMovement>().sprintMultiplier : 1;
                    ScriptableObject.CreateInstance<Noise>().makeNoise(UnityEngine.Random.Range(footstepVolume.x, footstepVolume.y) * sprintMod, transform.position);
                }
                newCameraPosition = Camera.transform.localPosition;
                newCameraPosition.y = Camera.transform.localPosition.y - jumpAndLandingBob.Offset();
            }
            else
            {
                //Standing still
                newCameraPosition = Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - jumpAndLandingBob.Offset();

                midstep = false;
                StopCoroutine("playFootsteps");
            }
            Camera.transform.localPosition = newCameraPosition;

            if(Input.GetKeyDown(KeyCode.B))
            {
                audio.PlayOneShot(walkStep);
            }

            //Landing from jump
            if (!m_PreviouslyGrounded && playerController.GetComponent<PlayerMovement>().isGrounded())
            {
                StartCoroutine(jumpAndLandingBob.DoBobCycle());
            }

            m_PreviouslyGrounded = playerController.GetComponent<PlayerMovement>().isGrounded();
        }

        IEnumerator playFootsteps()
        {
            audio.pitch = UnityEngine.Random.Range(.9F, 1F);

            if (!playerController.GetComponent<PlayerMovement>().isSprinting)
            {
                audio.volume = UnityEngine.Random.Range(.5F, .75F);
            }                
            else
            {
                audio.volume = UnityEngine.Random.Range(.9F, 1F);     
            }            

            audio.PlayOneShot(walkStep);

            yield return new WaitForSeconds(StrideInterval/2F);
            midstep = false;
        }
    }
}