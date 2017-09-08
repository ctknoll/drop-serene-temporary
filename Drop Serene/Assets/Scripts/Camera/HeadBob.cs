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
        public AudioSource stepAudio;
        private bool m_PreviouslyGrounded;
        private bool midstep = false;
        private Vector3 m_OriginalCameraPosition;


        private void Start()
        {
            playerController = GameObject.Find("Player");
            Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            m_OriginalCameraPosition = Camera.transform.localPosition;
            motionBob.Setup(Camera, StrideInterval);
            stepAudio = GetComponentInParent<AudioSource>();            
        }


        private void Update()
        {
            Vector3 newCameraPosition;
            //Walking/Sprinting - step at lowest point in DoHeadBob cycle
            if (playerController.GetComponent<CharacterController>().velocity.magnitude > 0 && playerController.GetComponent<PlayerMovement>().isGrounded())
            {
                Camera.transform.localPosition = motionBob.DoHeadBob(playerController.GetComponent<CharacterController>().velocity.magnitude / (playerController.GetComponent<PlayerMovement>().isSprinting ? playerController.GetComponent<PlayerMovement>().sprintMultiplier : 1f));
                newCameraPosition = Camera.transform.localPosition;
                newCameraPosition.y = Camera.transform.localPosition.y - jumpAndLandingBob.Offset();
                if(!midstep)
                {
                    midstep = true;
                    StartCoroutine("playFootsteps");
                }
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
                stepAudio.PlayOneShot(stepAudio.clip);
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
            stepAudio.PlayOneShot(stepAudio.clip);            
            yield return new WaitForSeconds(2.5F);
            midstep = false;
        }
    }
}
