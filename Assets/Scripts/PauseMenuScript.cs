/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			PauseMenuScript.cs                                                                                              *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains the PauseMenuScript class. Manages pause menu control.                                                 *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Autohand;


public class PauseMenuScript : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private AutoHandPlayer player;
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private float playerSpeed;

    [Header("Options")]
    [SerializeField] private Toggle minimapToggle;
    [SerializeField] private Toggle snapToggle;
    [SerializeField] private GameObject minimap;

    [Header("Menu")]
    [SerializeField] private TextMeshProUGUI tasks;
    [SerializeField] private bool pauseMenuActive = false;
    
    

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Awake                                                                                                            *
    * DESCRIPTION:    Initilization of the gameobjects and camera                                                                      *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    private void Awake()
    {
        cam = Camera.main.transform;
        player = GameObject.Find("Auto Hand Player").GetComponent<AutoHandPlayer>();
        playerSpeed = player.maxMoveSpeed; //backup player speed
        gameObject.SetActive(false);
    }


    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Unpause                                                                                                          *
    * DESCRIPTION:    Resume and go back to the application.                                                                           *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void Resume()
    {
        //Enable player movement
        player.maxMoveSpeed = playerSpeed;

        //Disable menu
        MiniMapToggle();
        gameObject.SetActive(false);
        pauseMenuActive = false;

        //UI cam overlays UI
        Camera UICam = GameObject.Find("UICamera").GetComponent<Camera>();
        UICam.cullingMask = LayerMask.GetMask("UI");

        //Resume Timer
        Timer.Instance.SetTimerActive(true);
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Pause                                                                                                            *
    * DESCRIPTION:    Display the pause menu.                                                                                          *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void Pause()
    {
        //Move menu infront of players current view
        transform.position = (cam.position + cam.forward * 3.0f) + new Vector3(0.0f, -0.4f, 0.0f);
        Vector3 vRot = cam.eulerAngles;
        vRot.z = 0;
        transform.eulerAngles = vRot;

        //Disable player movement
        player.maxMoveSpeed = 0;

        //Enable menu and prep contents
        minimap.SetActive(false);
        gameObject.SetActive(true);
        pauseMenuActive = true;
        GetTasks();

        //UI cam overlays UI and Hands
        Camera UICam = GameObject.Find("UICamera").GetComponent<Camera>();
        UICam.cullingMask = LayerMask.GetMask("UI", "Hand");

        //Pause timer
        Timer.Instance.SetTimerActive(false);
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         QuitToTitleScreenGame                                                                                            *
    * DESCRIPTION:    Return to main menu.                                                                                             *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void QuitToTitleScreenGame()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         SanpToggle                                                                                                       *
    * DESCRIPTION:    Toggle snap turn or smooth turn.                                                                                 *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void SanpToggle()
    {
        if (snapToggle.isOn)
        {
            player.rotationType = RotationType.snap;
            return;
        }
        else
        {
            player.rotationType = RotationType.smooth;
            return;
        }
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         MiniMapToggle                                                                                                    *
    * DESCRIPTION:    Toggle Minimap.                                                                                                  *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void MiniMapToggle()
    {
        if (minimapToggle.isOn)
        {
            minimap.SetActive(true);
            return;
        }
        else
        {
            minimap.SetActive(false);
            return;
        }
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         DisplayPauseMenu                                                                                                 *
    * DESCRIPTION:    Enable / disable pause screen.                                                                                   *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void DisplayPauseMenu()
    {
        //If unpausing
        if (pauseMenuActive == true)
        {
            Resume();
            return;
        }
        
        //if Pausing
        if (pauseMenuActive == false)
        {
            Pause();
            return;
        }
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         GetTasks                                                                                                         *
    * DESCRIPTION:    Gather the task list for the pausescreen.                                                                        *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    private void GetTasks()
    {
        tasks.text = GameObject.FindGameObjectWithTag("TaskList").GetComponent<TaskManager>().GetTaskList();
    }
}
