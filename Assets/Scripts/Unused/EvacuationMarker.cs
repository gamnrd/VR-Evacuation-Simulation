/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			EvacuationMarker.cs                                                                                             *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains the EvacuationMarker class. Handles evacuation marker behaviour.                                       *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

using UnityEngine;

public class EvacuationMarker : MonoBehaviour
{
    [SerializeField] private GameObject marker;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material evacuatedMaterial;
    [SerializeField] private bool isGreen = false;

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         isGrabbed                                                                                                        *
    * DESCRIPTION:    When grabed, it will set the evacuation marker's material.                                                       *
    * PARAMETERS:     ~none~                                                                                                           *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void isGrabbed()
    {
        if(!isGreen)
        {
            isGreen = true;
            marker.GetComponent<MeshRenderer>().material = evacuatedMaterial;
        }
        else
        {
            isGreen = false;
            marker.GetComponent<MeshRenderer>().material = defaultMaterial;
        }
    }
}
