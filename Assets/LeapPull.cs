using UnityEngine;
using System.Collections;



public class LeapPull : MonoBehaviour
{
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;
    [SerializeField] GameObject handle;
    public static bool LeapGrabActive = false;
    bool grabbed = false;

    [SerializeField] ViveExpand viveExpand;

    void Update()
    {
        leftHand.SetActive(LeapGrabActive);
        rightHand.SetActive(LeapGrabActive);
        
    }

    public void Pinch(string s)
    {
        if (grabbed || !LeapGrabActive)
            return;

        grabbed = true;
        if(s == "Left")
        {
            handle.transform.parent = leftHand.transform;
            handle.transform.localPosition = Vector3.zero;
            viveExpand.origin = handle.transform.position;
        }
        if (s == "Right")
        {
            handle.transform.parent = rightHand.transform;
            handle.transform.localPosition = Vector3.zero;
            viveExpand.origin = handle.transform.position;
        }
    }

    public void Release()
    {
        grabbed = false;
        handle.transform.parent = null;
        StartCoroutine(SnapBack());
    }

    IEnumerator SnapBack()
    {
        while (Vector3.Distance(handle.transform.position, viveExpand.origin) > .005f)
        {
            handle.transform.position = Vector3.Lerp(handle.transform.position, viveExpand.origin, .01f);
            yield return null;
        }
        handle.transform.position = viveExpand.origin;
    }

}
