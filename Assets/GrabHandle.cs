using UnityEngine;
using System.Collections;

public class GrabHandle : MonoBehaviour
{
    public GameObject handle;
    SteamVR_TrackedController hand;
    bool pressed = false;
    [SerializeField] ViveExpand vh;
    bool menuTrigger = false;

	void Start ()
    {
        vh.origin = handle.transform.position;
        hand = GetComponent<SteamVR_TrackedController>();
	}

    void Update()
    {
        if (hand.triggerPressed && !pressed)
            Pressed();

        if (!hand.triggerPressed && pressed)
            Release();

        if(hand.menuPressed && !menuTrigger)
        {
            menuTrigger = true;
            LeapPull.LeapGrabActive = !LeapPull.LeapGrabActive;
        }
        if(!hand.menuPressed)
        {
            menuTrigger = false;
        }
    }

    void Pressed()
    {
        pressed = true;
        handle.transform.parent = gameObject.transform;
        handle.transform.localPosition = Vector3.zero;
        vh.origin = hand.transform.position;
    }

    void Release()
    {
        pressed = false;
        handle.transform.parent = null;
        StartCoroutine(SnapBack());
    }

    //void OnTriggerStay(Collider c)
    //{
    //    if (c.gameObject != handle)
    //        return;

    //    if (hand.triggerPressed)
    //    {
    //        StopCoroutine(SnapBack());
    //        handle.transform.parent = gameObject.transform;
    //        handle.transform.localPosition = Vector3.zero;
    //    }
    //    else
    //    {
    //        handle.transform.parent = null;
    //        StartCoroutine(SnapBack());
    //    }
    //}

    IEnumerator SnapBack() //To Reality, Opp..
    {
        while (Vector3.Distance(handle.transform.position, vh.origin) > .005f)
        {
            handle.transform.position = Vector3.Lerp(handle.transform.position, vh.origin, .01f);
            yield return null;
        }
        handle.transform.position = vh.origin;
    }
}
