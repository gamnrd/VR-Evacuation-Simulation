/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			Timer.cs                                                                                                        *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains the timer class.                                                                                       *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

using UnityEngine;

public class Timer : MonoBehaviour
{
    /*
    [Header("Timer Text")]
    [SerializeField] private TextMeshPro timerTxt;*/

    public static Timer Instance;

    [Header("Timer")]
    [SerializeField] public float currentTimer;
    [SerializeField] private float finishTime;
    [SerializeField] private bool isTimerActive = true;

    [Header("Countdown")]
    [SerializeField] private float timeLimit;
    [SerializeField] public float currentCountdown;
    [SerializeField] public bool hasTimeLimit;

    [SerializeField] private GameObject resultScreen;

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Start                                                                                                            *
    * DESCRIPTION:    This function will called before the first frame update. Initialize the time limit.                              *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    void Start()
    {
        Instance = this;
        //If a time limit has been set
        if (PlayerPrefs.HasKey("HasTimeLimit"))
        {
            hasTimeLimit = PlayerPrefs.GetInt("HasTimeLimit") == 1 ? true : false;
        }        
        else
        {
            hasTimeLimit = false;
        }
        timeLimit = PlayerPrefs.HasKey("TimeLimit") ? PlayerPrefs.GetFloat("TimeLimit") : 5;

        currentCountdown = timeLimit * 60;
        currentTimer = 0;
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Update                                                                                                           *
    * DESCRIPTION:    On update, it will check the time limit state.                                                                   *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    void Update()
    {
        //If timing is active
        if (isTimerActive)
        {
            //time the user
            currentTimer += Time.deltaTime;

            //if there is a time limit
            if (hasTimeLimit)
            {
                //countdown the timelimt
                currentCountdown -= Time.deltaTime;

                //if the time limit has hit zero
                if (currentCountdown <= 0f)
                {
                    TimeLimitReached();
                }
            }
        }
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         SetTimerActive                                                                                                   *
    * DESCRIPTION:    It will pause the timers.                                                                                        *
    * PARAMETERS:     activeTimer - boolean state value for timer.                                                                     *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void SetTimerActive(bool activeTimer)
    {
        isTimerActive = activeTimer;
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         FinishTimer                                                                                                      *
    * DESCRIPTION:    When tasks finished, stop timers and display results screen                                                      *
    * PARAMETERS:     ~none~                                                                                                           *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void FinishTimer()
    {
        SetTimerActive(false);
        resultScreen.SetActive(true);
        ResultScreenController.Instance.TasksCompleted(currentTimer);
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         TimeLimitReached                                                                                                 *
    * DESCRIPTION:    When time limit is reached, stop timers and display results screen.                                              *
    * PARAMETERS:     ~none~                                                                                                           *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void TimeLimitReached()
    {
        SetTimerActive(false);
        resultScreen.SetActive(true);
        ResultScreenController.Instance.TimeLimitReached(timeLimit * 60);
    }

}
