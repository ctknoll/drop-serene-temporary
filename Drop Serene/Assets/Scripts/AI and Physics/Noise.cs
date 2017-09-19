using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : ScriptableObject
{
    public AnimationCurve noiseDropoff = new AnimationCurve(new Keyframe(0, 1), new Keyframe(.25f, .5f),
                                                            new Keyframe(.5f, .15f), new Keyframe(.75f, .05f),
                                                            new Keyframe(1, .01f));

    public void makeNoise(float loudness, Vector3 location)
    {
        Collider[]  colliders = Physics.OverlapSphere(location, loudness);
        foreach(Collider collider in colliders)
        {
            NoiseListener noiseListener;
            if (noiseListener = collider.gameObject.GetComponent<NoiseListener>())
            {
                float distanceModifier = (location - collider.transform.position).magnitude / loudness;
                float volume = loudness * noiseDropoff.Evaluate(distanceModifier);
                noiseListener.onHearingNoise(volume, location);
            }
        }
    }
}
