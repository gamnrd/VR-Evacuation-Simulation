/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			OxygenTask.cs                                                                                                   *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains the OxygenTask class. Manages oxgen tasks states.                                                      *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

using UnityEngine;

public class OxygenTask : MonoBehaviour
{
    private bool isGrabbed = false;
    [SerializeField] private bool taskComplete = false;
    [SerializeField] private AudioClip clip;
    private HingeJoint joint;

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Start                                                                                                            *
    * DESCRIPTION:    This function will called before the first frame update. Initialize the joint for valve.                         *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    void Start()
    {
        joint = GetComponent<HingeJoint>();
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Update                                                                                                           *
    * DESCRIPTION:    Will check if valve is grabbed and the rotation of valve and update the task completion state.                   *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    void Update()
    {
        //If  the valve is currently grabbed && rotation exceeds required amount && the task has not already been completed, complete task
        if (isGrabbed && joint.angle <= joint.limits.min + 5f && !taskComplete)
        {
            taskComplete = true;
            GetComponent<AudioSource>().PlayOneShot(clip);
            TaskCompleted();
        }
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         TaskCompleted                                                                                                    *
    * DESCRIPTION:    Will update the oxygen task state.                                                                               *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    private void TaskCompleted()
    {
        //call event for clipboard to update task
        GameObject.FindGameObjectWithTag("TaskList").GetComponent<TaskManager>().OxygenTask();
        //Remove the task marker
        Destroy(transform.parent.Find("TaskMarker").gameObject);
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Grabbed                                                                                                          *
    * DESCRIPTION:    Track when grabbed                                                                                               *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void Grabbed()
    {
        isGrabbed = true;
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Released                                                                                                         *
    * DESCRIPTION:    Track when grabbed                                                                                               *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void Released()
    {
        isGrabbed = false;
    }
}
