using UnityEngine;
using System.Collections;

public class ViveExpand : MonoBehaviour
{
    [SerializeField] GameObject knob;
    public Vector3 origin;
    [SerializeField] float maxDist = 1f;
    Animation anim;
    float animLength;

	void Start()
    {
        origin = knob.transform.position;
        anim = GetComponent<Animation>();

        anim.Play();
        anim["Open"].speed = 0;
        animLength = anim.clip.length;
    }
	
	void Update ()
    {
        float dist = Vector3.Distance(origin, knob.transform.position);
        if (dist > maxDist)
            dist = maxDist;
        if (dist < 0)
            dist = 0;

        float seekTime = animLength * (dist / maxDist);

        anim["Open"].time = seekTime;
    }
}
