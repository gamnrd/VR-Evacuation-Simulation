/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			Watch.cs                                                                                                        *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains timer functionality.                                                                                   *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

using System;
using UnityEngine;
using TMPro;

public class Watch : MonoBehaviour
{
    [SerializeField] private TextMeshPro time;
    [SerializeField] private bool hasTimeLimit;

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Start                                                                                                            *
    * DESCRIPTION:    This function will called before the first frame update. Initialize the timer.                                   *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    void Start()
    {
        hasTimeLimit = Timer.Instance.hasTimeLimit;
    }


    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         Update                                                                                                           *
    * DESCRIPTION:    On update, it will check the time limit state and update the text.                                               *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    void Update()
    {
        //If theres a time limit, countdown
        if (hasTimeLimit)
        {
            //Format seconds into mm:ss format
            var ts = TimeSpan.FromSeconds(Timer.Instance.currentCountdown);
            time.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        }
        else
        {
            //Format seconds into mm:ss format
            var ts = TimeSpan.FromSeconds(Timer.Instance.currentTimer);
            time.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        }
    }
}
