using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoiseListener : MonoBehaviour
{
    public float threshold;
    public NoiseEvent noiseEvent;

    public void onHearingNoise(float volume, Vector3 location)
    {
        if (volume < threshold) return;
        noiseEvent.Invoke(location);
    }
}

[System.Serializable]
public class NoiseEvent : UnityEvent<Vector3> {}
