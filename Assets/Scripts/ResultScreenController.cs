/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			ResultScreenController.cs                                                                                       *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains the ResultScreenController class. Screne management of the result screen.                              *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Autohand;

public class ResultScreenController : MonoBehaviour
{
    public static ResultScreenController Instance;

    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform cam;

    [Header("Results Screen")]
    [SerializeField] private TextMeshProUGUI resultTitle;
    [SerializeField] private TextMeshProUGUI resultsText;
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private string time;
    
    

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Awake                                                                                                            *
    * DESCRIPTION:    This function will called before Start(). It initialize obejcts.                                                 *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    private void Awake()
    {
        Instance = this;
        cam = Camera.main.transform;
        taskManager = GameObject.FindGameObjectWithTag("TaskList").GetComponent<TaskManager>();
        player = GameObject.Find("Auto Hand Player");
        gameObject.SetActive(false);
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         ReturnToStart                                                                                                    *
    * DESCRIPTION:    This function will load first scene (main game menu)                                                             *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void ReturnToStart() 
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         TimeLimitReached                                                                                                 *
    * DESCRIPTION:    This function will set result text with the tasks completed within the timelimit.                                *
    * PARAMETERS:     timeLimit - float value time limit                                                                               *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void TimeLimitReached(float timeLimit)
    {
        MoveUIToUser();
        //Stop player moevemnt
        player.GetComponent<AutoHandPlayer>().maxMoveSpeed = 0;

        //Convert seconds to mm:ss time format
        var ts = TimeSpan.FromSeconds(timeLimit);
        time = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

        //Alter results screen for Time limit reached outcome
        resultTitle.text = "Time's Up";
        resultsText.text = "Tasks Compleated in " + time + ":\n\n" + taskManager.GetTaskList();
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         TasksCompleted                                                                                                   *
    * DESCRIPTION:    This function will have the result text with task completion + time                                              *
    * PARAMETERS:     finalTime - float value                                                                                          *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void TasksCompleted(float finalTime)
    {
        MoveUIToUser();
        //Stop player movement
        player.GetComponent<AutoHandPlayer>().maxMoveSpeed = 0;

        //Convert seconds to mm:ss time format
        var ts = TimeSpan.FromSeconds(finalTime);
        time = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

        //Alter results screen for Tasks completed outcome
        resultTitle.text = "Complete";
        resultsText.text = "All Tasks Compleated in:\n\n" + time;
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         MoveUIToUser                                                                                                     *
    * DESCRIPTION:    This function will set the UI to the user position.                                                              *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    private void MoveUIToUser()
    {
        //Move menu infront of players current view
        transform.position = (cam.position + cam.forward * 3.0f) + new Vector3(0.0f, -0.4f, 0.0f);
        Vector3 vRot = cam.eulerAngles;
        vRot.z = 0;
        transform.eulerAngles = vRot;

        //UI cam overlays UI and Hands
        Camera UICam = GameObject.Find("UICamera").GetComponent<Camera>();
        UICam.cullingMask = LayerMask.GetMask("UI", "Hand");
    }
}
