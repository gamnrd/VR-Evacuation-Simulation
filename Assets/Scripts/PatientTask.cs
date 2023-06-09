/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			PatientTask.cs                                                                                                  *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains the PatientTask class. Manages patient's evacuation area                                               *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

using UnityEngine;

public class PatientTask : MonoBehaviour
{
    public AudioClip clip;

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         OnTriggerStay                                                                                                    *
    * DESCRIPTION:    Checks patient is evacuated and set evacuation state and patient task completion state when the patients is      *
    *                 placed in the drop off area.                                                                                     *
    * PARAMETERS:     Collider other                                                                                                   *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    private void OnTriggerStay(Collider other)
    {
        //If a wheelchair patient is placed in the drop off that has not already been previously evacuated
        if (other.gameObject.tag == "Patient" && other.gameObject.GetComponent<WheelchairPatient>().isEvacuated == false)
        {
            other.gameObject.GetComponent<WheelchairPatient>().isEvacuated = true;
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
            GameObject.FindGameObjectWithTag("TaskList").GetComponent<TaskManager>().PatientTask();
            Destroy(other.gameObject, 1);
            return;
        }

        //If a walking patient is placed in the drop off that has not already been previously evacuated
        if (other.gameObject.tag == "PatientWalk" && other.gameObject.GetComponent<WalkingPatient>().isEvacuated == false)
        {
            other.gameObject.GetComponent<WalkingPatient>().isEvacuated = true;
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
            GameObject.FindGameObjectWithTag("TaskList").GetComponent<TaskManager>().PatientTask();
            Destroy(other.gameObject, 1);
            return;
        }
    }
}
