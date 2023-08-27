using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPortal : MonoBehaviour
{
    private ARRaycastManager ARRaycastManagerScript;

    public GameObject Portal;
    private GameObject PortalAR;
    public GameObject Character;

    private Animation OpenPortalAnimation;
    private Animation ClosePortalAnimation;
    private Animation CharacterAppearanceAnimation;

    public bool ActivePortal = false;
    void Start()
    {
        ARRaycastManagerScript = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        SetPortal();
    }

    void SetPortal()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        Touch touch = Input.GetTouch(0);

        ARRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if (hits.Count > 0 && Input.touchCount > 0 && !ActivePortal)
        {
            PortalAR = Instantiate(Portal, hits[0].pose.position, Portal.transform.rotation);
            ActivePortal = true;
            OpenPortalAnimation = PortalAR.GetComponent<Animation>();
            OpenPortalAnimation.Play("Portal Appearance");

            StartCoroutine(routine: CharacterAppearance());

        }
    }

    private IEnumerator CharacterAppearance()
    {
        yield return new WaitForSeconds(2);
        Instantiate(Character, PortalAR.transform.position, Character.transform.rotation);
        //CharacterAppearanceAnimation = Character.GetComponent<Animation>();
        //CharacterAppearanceAnimation.Play("Character Animation");

        yield return new WaitForSeconds(2);

        ClosePortalAnimation = PortalAR.GetComponent<Animation>();
        ClosePortalAnimation.Play("Close Portal");
    }
}
