using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : ScriptableObject
{
	public Noise(float loudness, Vector3 location)
    {
        Collider[]  colliders = Physics.OverlapSphere(location, loudness);
        foreach(Collider collider in colliders)
        {
            NoiseListener noiseListener;
            if (noiseListener = collider.gameObject.GetComponent<NoiseListener>())
            {
                //determine loudness somehow
                //noiseListener.onHearingNoise(location, loudness);
            }
        }
    }
}
