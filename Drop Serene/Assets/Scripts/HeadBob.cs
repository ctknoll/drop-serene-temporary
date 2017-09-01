using System;
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
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;


        private void Start()
        {
            playerController = GameObject.Find("Player");
            Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            m_OriginalCameraPosition = Camera.transform.localPosition;
            motionBob.Setup(Camera, StrideInterval);            
        }


        private void Update()
        {
            Vector3 newCameraPosition;
            if (playerController.GetComponent<CharacterController>().velocity.magnitude > 0 && playerController.GetComponent<PlayerMovement>().isGrounded())
            {
                Camera.transform.localPosition = motionBob.DoHeadBob(playerController.GetComponent<CharacterController>().velocity.magnitude / (playerController.GetComponent<PlayerMovement>().isSprinting ? playerController.GetComponent<PlayerMovement>().sprintMultiplier : 1f));
                newCameraPosition = Camera.transform.localPosition;
                newCameraPosition.y = Camera.transform.localPosition.y - jumpAndLandingBob.Offset();
            }
            else
            {
                newCameraPosition = Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - jumpAndLandingBob.Offset();
            }
            Camera.transform.localPosition = newCameraPosition;

            if (!m_PreviouslyGrounded && playerController.GetComponent<PlayerMovement>().isGrounded())
            {
                StartCoroutine(jumpAndLandingBob.DoBobCycle());
            }

            m_PreviouslyGrounded = playerController.GetComponent<PlayerMovement>().isGrounded();
        }
    }
}
