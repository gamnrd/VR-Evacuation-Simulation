/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			RoomMarker.cs                                                                                                   *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains the RoomMarker class.                                                                                  *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

using UnityEngine;

public class RoomMarker : MonoBehaviour
{
    [SerializeField] private GameObject emptyMarker;        //Empty room - white
    [SerializeField] private GameObject occupiedMarker;     //Occupied room - red
    [SerializeField] private GameObject taskMarker;
    [SerializeField] private bool isRoomEmpty = false;
    [SerializeField] private bool taskCompleated = false;

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Awake                                                                                                            *
    * DESCRIPTION:    This function will called before Start(). It will find the objects postion.                                      *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    private void Awake()
    {
        emptyMarker = transform.Find("RoomOccupied/Empty").gameObject;
        occupiedMarker = transform.Find("RoomOccupied/Occupied").gameObject;
        emptyMarker.SetActive(false);
        occupiedMarker.SetActive(true);
        taskMarker = transform.Find("TaskMarker").gameObject;

    }


    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         ToggleRoomMarker                                                                                                 *
    * DESCRIPTION:    This function will toggle the room makers and sets the taks completion state.                                    *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void ToggleRoomMarker()
    {
        if (isRoomEmpty)
        {
            emptyMarker.SetActive(false);
            occupiedMarker.SetActive(true);
            taskMarker.SetActive(true);
            isRoomEmpty = false;

            //If the task had been previously completed, undo completion
            if (taskCompleated)
            {
                taskCompleated = false;
                GameObject.FindGameObjectWithTag("TaskList").GetComponent<TaskManager>().RoomMarkerTaskMinus();
            }
            return;
        }

        else
        {
            emptyMarker.SetActive(true);
            occupiedMarker.SetActive(false);
            taskMarker.SetActive(false);
            isRoomEmpty = true;

            //If the task has not been completed, complete it
            if (!taskCompleated)
            {
                taskCompleated = true;
                GameObject.FindGameObjectWithTag("TaskList").GetComponent<TaskManager>().RoomMarkerTaskPlus();
            }
            return;
        }
    }
}
