using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    [Serializable]
    public class CurveControlledBob
    {
        public float HorizontalBobRange = 0.12f;
        public float VerticalBobRange = 0.12f;
        public AnimationCurve walkingCurve; // sin curve for head bob for walking
		public AnimationCurve runningCurve; // sin curve for head bob for running
        public float VerticaltoHorizontalRatio = 1f;
		public PlayerMovement player;

        public float m_CyclePositionX = 0;
        public float m_CyclePositionY = 0;
        public float m_BobBaseInterval;
        private Vector3 m_OriginalCameraPosition;
        public float m_Time;


        public void Setup(Camera camera, float bobBaseInterval)
        {
			player = GameObject.Find("Player").GetComponent<PlayerMovement>();
			m_BobBaseInterval = bobBaseInterval;
            m_OriginalCameraPosition = camera.transform.localPosition;
        }


        public Vector3 DoHeadBob(float speed)
        {
            //Debug.Log("Horizontal: " + Bobcurve.Evaluate(m_CyclePositionX) * HorizontalBobRange);
            //Debug.Log("Vertical: " + Bobcurve.Evaluate(m_CyclePositionY) * VerticalBobRange);
			m_Time = player.isSprinting? runningCurve[runningCurve.length-1].time : walkingCurve[walkingCurve.length-1].time;
			float xPos = m_OriginalCameraPosition.x + ((player.isSprinting? runningCurve.Evaluate(m_CyclePositionX) : walkingCurve.Evaluate(m_CyclePositionX)) * HorizontalBobRange);
			float yPos = m_OriginalCameraPosition.y + ((player.isSprinting? runningCurve.Evaluate(m_CyclePositionY) : walkingCurve.Evaluate(m_CyclePositionY)) * VerticalBobRange);

            m_CyclePositionX += (speed*Time.deltaTime) / m_BobBaseInterval;
            m_CyclePositionY += ((speed*Time.deltaTime) / m_BobBaseInterval) * VerticaltoHorizontalRatio;

            if (m_CyclePositionX > m_Time)
            {
                m_CyclePositionX = m_CyclePositionX - m_Time;
            }
            if (m_CyclePositionY > m_Time)
            {
                m_CyclePositionY = m_CyclePositionY - m_Time;
            }

            return new Vector3(xPos, yPos, 0f);
        }
    }
}
