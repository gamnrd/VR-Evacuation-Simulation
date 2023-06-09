/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			WheelchirPatiend.cs                                                                                             *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains wheelchair paitent controll.                                                                           *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

using UnityEngine;
using TMPro;
using Autohand;

public class WheelchairPatient : MonoBehaviour
{
    [SerializeField] public bool isEvacuated = false;
    [SerializeField] public GameObject o2Tank;
    [SerializeField] private GameObject o2UI;
    [SerializeField] private int rand;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Grabbable grab;
    [SerializeField] private bool isTutorial = false;

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Awake                                                                                                            *
    * DESCRIPTION:    This function will called before Start(). It will gets the object set up and random numeber variables set up.    *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        o2UI = GetComponentInChildren<TextMeshPro>().gameObject;
        grab = GetComponent<Grabbable>();

        //If not tutorial randomize patients needs (needing oxygen tank)
        if (!isTutorial)
        {
            rand = Random.Range(0, 2);
        }
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Start                                                                                                            *
    * DESCRIPTION:    This function will called before the first frame update. It will set the wheel chair patient with or with out    *
    *                 oxygen                                                                                                           *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    private void Start()
    {
        //does not require o2
        if (rand == 0)
        {
            rb.isKinematic = false;
            grab.isGrabbable = true;
            o2UI.SetActive(false);
            gameObject.transform.Find("PlacePoint").gameObject.SetActive(false);
            if (!isTutorial)
            {
                o2Tank.SetActive(false);
            }
        }

        //Does require o2
        if (rand == 1)
        {
            //Until given oxygen cannot be moved
            rb.isKinematic = true;
            grab.isGrabbable = false;
            o2UI.SetActive(true);

            if (!isTutorial)
            {
                o2Tank.SetActive(true);
            }
        }
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         GetOxygen                                                                                                        *
    * DESCRIPTION:    When the patient is provided an oxygen tank                                                                      *
    *                 oxygen                                                                                                           *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void GetOxygen()
    {
        //Enable user moving patient
        rb.isKinematic = false;
        grab.isGrabbable = true;
        o2UI.SetActive(false);

        //Make O2 tank, no longer grabbable, set to default so it no longer get highlighted, disable the task marker
        o2Tank.transform.Find("O2Tank").GetComponent<Grabbable>().enabled = false;
        o2Tank.transform.Find("O2Tank").gameObject.layer = LayerMask.NameToLayer("Default");
        o2Tank.transform.Find("TaskMarker").gameObject.SetActive(false);
    }
}
