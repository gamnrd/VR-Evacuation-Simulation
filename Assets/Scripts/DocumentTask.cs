/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			DocumentTask.cs                                                                                                 *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains the DocumentTask class.                                                                                *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

using UnityEngine;

public class DocumentTask : MonoBehaviour
{
    [SerializeField] private bool taskComplete = false;     //Ensure tasks cannot be repeated by marking as completed
    [SerializeField] private AudioClip clip;
    private GameObject parent;

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Start                                                                                                            *
    * DESCRIPTION:    This function will called before the first frame update.                                                         *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    private void Start()
    {
        parent = gameObject.transform.parent.gameObject;
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         OnGrab                                                                                                           *
    * DESCRIPTION:    This function will update the task completion state when the task board is grabbed.                              *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void OnGrab()
    {
        if (!taskComplete)
        {
            //Mark task as complete then send completion to the task manager
            taskComplete = true;
            GameObject.FindGameObjectWithTag("TaskList").GetComponent<TaskManager>().DocumentsTask();
            
            //Play task complete audio clip
            GetComponent<AudioSource>().PlayOneShot(clip);

            //Set invisible so audio clip can play before destroying document
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(parent, 2f);
        }
        
    }
}
