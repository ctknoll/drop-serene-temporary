using System;
using System.Collections;
using UnityEngine;

    [Serializable]
    public class FOVKick : MonoBehaviour
    {
        public Camera Camera;                           // optional camera setup, if null the main camera will be used
        public PlayerMovement player;
        [HideInInspector] public float originalFov;     // the original fov
        public float FOVDecreaseMax = 45f;                  // the amount the field of view increases when going into a run
        public float TimeToIncrease = .1f;               // the amount of time the field of view will increase over
        public float TimeToDecrease = .1f;               // the amount of time the field of view will take to return to its original size
        public AnimationCurve IncreaseCurve;


        public void Start ()
        {
            Camera = gameObject.GetComponent<Camera>();
            player = gameObject.GetComponentInParent<PlayerMovement>();
            originalFov = Camera.fieldOfView;
        }

        public void Update ()
        {
            float FOVChange = (1 - player.stamina) * FOVDecreaseMax;
            Camera.fieldOfView = originalFov - FOVChange;

        }

        public IEnumerator FOVKickUp()
        {
 //           float t = Mathf.Abs((Camera.fieldOfView - originalFov)/FOVIncrease);
//            while (t < TimeToIncrease)
            {
//                Camera.fieldOfView = originalFov + (IncreaseCurve.Evaluate(t/TimeToIncrease)*FOVIncrease);
//                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }


        public IEnumerator FOVKickDown()
        {
 //           float t = Mathf.Abs((Camera.fieldOfView - originalFov)/FOVIncrease);
//            while (t > 0)
            {
  //              Camera.fieldOfView = originalFov + (IncreaseCurve.Evaluate(t/TimeToDecrease)*FOVIncrease);
//                t -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            //make sure that fov returns to the original size
            Camera.fieldOfView = originalFov;
        }
    }
