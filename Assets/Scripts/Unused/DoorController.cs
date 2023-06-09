/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			DoorController.cs                                                                                               *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains the DoorController class. Handles door behaviours.                                                     *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] public bool isOpen = false;
    private Animator anim;
    private BoxCollider bc;
    private bool playerNear = false;

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Start                                                                                                            *
    * DESCRIPTION:    This function will called before the first frame update.                                                         *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    private void Start()
    {
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider>();
    }

    /*private void Update()
    {
        anim.SetBool("isOpen", isOpen);
    }*/

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         ChangeDoorState                                                                                                  *
    * DESCRIPTION:    This function will update the door state depending on the player position is near to the door.                   *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void ChangeDoorState()
    {
        if (playerNear)
        {
            if (isOpen)
            {
                isOpen = false;
                anim.SetBool("isOpen", isOpen);
            }
            else if (!isOpen)
            {
                isOpen = true;
                anim.SetBool("isOpen", isOpen);
            }
        }

    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         OnTriggerEnter                                                                                                   *
    * DESCRIPTION:    This function will update boolean value true, which represents the player is near to the door.                   *
    * PARAMETERS:     Collider other - player object                                                                                   *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.tag == "Player")
        {
            bc.enabled = false;
            isOpen = true;
            anim.SetBool("isOpen", isOpen);
        }*/

        if (other.gameObject.tag == "Player" && playerNear == false)
        {
            playerNear = true;
        }
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         OnTriggerEnter                                                                                                   *
    * DESCRIPTION:    This function will update boolean value true, which represents the player is near to the door.                   *
    * PARAMETERS:     Collider other - player object                                                                                   *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    private void OnTriggerStay(Collider other)
    {
        /*if (other.gameObject.tag == "Player")
        {
            bc.enabled = false;
            isOpen = true;
            anim.SetBool("isOpen", isOpen);
        }*/

        if (other.gameObject.tag == "Player" && playerNear == false)
        {
            playerNear = true;
        }

    }


    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         OnTriggerExit                                                                                                    *
    * DESCRIPTION:    This function will update boolean value false, which represents the player is no more near to the door.          *
    * PARAMETERS:     Collider other - player object                                                                                   *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    private void OnTriggerExit(Collider other)
    {
        /*if (other.gameObject.tag == "Player")
        {
            isOpen = false;
            anim.SetBool("isOpen", isOpen);
            StartCoroutine(CloseDoor());
            
        }*/

        if (other.gameObject.tag == "Player" && playerNear == true)
        {
            playerNear = false;
        }
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(5);
        bc.enabled = true;
    }
}
