/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			TaskManager.cs                                                                                                  *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains the TaskManager class.Contorls in-game task management.                                                *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public TextMeshProUGUI taskList;

    public static TaskManager Instance;
    [SerializeField] private bool isTutorial = false;
    

    [Header("Patient Task")]
    [SerializeField] private bool patientTaskTracker = false;
    [SerializeField] private string patientTaskTxt;
    [SerializeField] private GameObject[] allPatients;
    [SerializeField] private int patientsEvacuated = 0;

    [Header("Room Marker Task")]
    [SerializeField] private bool roomMarkerTaskTracker = false;
    [SerializeField] private string roomMarkerTaskTxt;
    [SerializeField] private GameObject[] allRoomMarkers;
    [SerializeField] private int roomMarkersToggled = 0;

    [Header("Oxygen Task")]
    [SerializeField] private bool oxygenTaskTracker = false;
    [SerializeField] private string O2TaskTxt;
    [SerializeField] private GameObject[] allO2Tanks;
    [SerializeField] private int tanksShutOff = 0;
    
    [Header("Document Task")]
    [SerializeField] private bool documentTaskTracker = false;
    [SerializeField] private string documentsTaskTxt;
    [SerializeField] private GameObject[] allDocuments;
    [SerializeField] private int documentsCollected = 0;

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Awake                                                                                                            *
    * DESCRIPTION:    This function will called before Start(). Initilization.                                                         *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    private void Awake()
    {
        Instance = this;
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Start                                                                                                            *
    * DESCRIPTION:    This function will called before the first frame update. Sets up Game Objects needed for tasks, clip board.      *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    void Start()
    {
        //Get Patients
        allPatients = GameObject.FindGameObjectsWithTag("PatientTask");
        //Get Room Markers
        allRoomMarkers = GameObject.FindGameObjectsWithTag("RoomMarker");
        //Get O2 Tanks
        allO2Tanks = GameObject.FindGameObjectsWithTag("O2Tank");
        //Get patient Documents
        allDocuments = GameObject.FindGameObjectsWithTag("Documents");        

        //Set starting text on clipboard
        taskList = GameObject.Find("ClipboardText").GetComponent<TextMeshProUGUI>();
        SetupTasks();
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         SetupTasks                                                                                                       *
    * DESCRIPTION:    This function will set up the first tasks                                                                        *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    private void SetupTasks()
    {
        //If task not complete set as "Task name (tasks done / task  count)"
        if (!patientTaskTracker) patientTaskTxt = "1. Evacuate patients(" + patientsEvacuated + "/" + allPatients.Length + ")";
        if (!roomMarkerTaskTracker) roomMarkerTaskTxt = "2. Toggle door markers (" + roomMarkersToggled + "/" + allRoomMarkers.Length + ")";
        if (!oxygenTaskTracker) O2TaskTxt = "3. Shut off oxygen valve (" + tanksShutOff + "/" + allO2Tanks.Length + ")";
        if (!documentTaskTracker) documentsTaskTxt = "4. Retrieve medical documents (" + documentsCollected + "/" + allDocuments.Length + ")";
        
        UpdateClipboard();
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         UpdateClipboard                                                                                                  *
    * DESCRIPTION:    Used to replace text displayed on the clipholder.                                                                *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void UpdateClipboard()
    {
        taskList.text = patientTaskTxt + "\n" + roomMarkerTaskTxt + "\n" + O2TaskTxt + "\n" + documentsTaskTxt;
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         GetTaskList                                                                                                      *
    * DESCRIPTION:    Get current task list.                                                                                           *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        current task list (for pause screen)                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public string GetTaskList()
    {
        return patientTaskTxt + "\n" + roomMarkerTaskTxt + "\n" + O2TaskTxt + "\n" + documentsTaskTxt;
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         PatientTask                                                                                                      *
    * DESCRIPTION:    It tracks the patient task and update the text.                                                                  *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void PatientTask()
    {
        patientsEvacuated++;
        if (patientsEvacuated >= allPatients.Length)
        {
            //complete task
            patientTaskTxt =  "1. Evacuate patients(Done)";
            patientTaskTracker = true;
            UpdateClipboard();
            CheckTaskCompletetion();
            return;
        }
        SetupTasks();
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         RoomMarkerTaskPlus                                                                                               *
    * DESCRIPTION:    It tracks the room marker task and update the text.                                                              *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void RoomMarkerTaskPlus()
    {
        roomMarkersToggled++;
        if (roomMarkersToggled >= allRoomMarkers.Length)
        {
            //complete task
            roomMarkerTaskTxt = "2. Toggle room markers (Done)";
            roomMarkerTaskTracker = true;
            UpdateClipboard();
            CheckTaskCompletetion();
            return;
        }
        SetupTasks();
    }
    
    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         RoomMarkerTaskMinus                                                                                              *
    * DESCRIPTION:    If door marker is toggled after task complete, undo task completion.                                             *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void RoomMarkerTaskMinus()
    {
        if (roomMarkersToggled >= allRoomMarkers.Length)
        {
            //complete task
            roomMarkerTaskTxt = "2. Toggle door markers (" + (roomMarkersToggled - 1) + "/" + allRoomMarkers.Length + ")";
            roomMarkerTaskTracker = false;
        }
        roomMarkersToggled--;
        SetupTasks();
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         OxygenTask                                                                                                       *
    * DESCRIPTION:    It tracks the oxygen task and update the text.                                                                  *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void OxygenTask()
    {
        tanksShutOff++;
        if (tanksShutOff >= allO2Tanks.Length)
        {
            //complete task
            O2TaskTxt = "3. Shut off oxygen valve (Done)";
            oxygenTaskTracker = true;
            UpdateClipboard();
            CheckTaskCompletetion();
            return;
        }
        SetupTasks();
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         DocumentsTask                                                                                                    *
    * DESCRIPTION:    It tracks the document task and update the text.                                                                 *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void DocumentsTask()
    {
        documentsCollected++;
        if (documentsCollected >= allDocuments.Length)
        {
            //complete task
            documentsTaskTxt = "4. Retrieve medical documents (Done)";
            documentTaskTracker = true;
            UpdateClipboard();
            CheckTaskCompletetion();
            return;
        }
        SetupTasks();
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         CheckTaskCompletetion                                                                                            *
    * DESCRIPTION:    It stops the timer when all the task is completed                                                                *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void CheckTaskCompletetion()
    {
        if (patientTaskTracker && roomMarkerTaskTracker && oxygenTaskTracker && documentTaskTracker && !isTutorial)
        {
            Timer.Instance.FinishTimer();
        }
    }
}
