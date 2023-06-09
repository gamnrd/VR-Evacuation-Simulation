/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			MainMenuScript.cs                                                                                               *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains the MainMenuScript class. Manages main menu behaviour.                                                 *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    [Header("Tutorial")]
    [SerializeField] private bool tutorialStarted = false;
    [SerializeField] private Button tutorialButton;


    [Header("Time Limit")]
    [SerializeField] private Toggle timeLimitToggle;
    [SerializeField] private TextMeshProUGUI timeLimitSliderTxt;
    [SerializeField] private Slider timeLimitSlider;


    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         StartProgram                                                                                                     *
    * DESCRIPTION:    This function will start the program.                                                                            *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void StartProgram()
    {
        DefaultPlayerPrefs();
        PlayerPrefs.Save();
        SceneManager.LoadScene("Final");
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         StartTutorial                                                                                                    *
    * DESCRIPTION:    This function will load in the tutorial scene.                                                                   *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void StartTutorial()
    {
        //If the tutorial has not already been loaded
        if (!tutorialStarted)
        {
            //Load the tutorial scene additivley
            SceneManager.LoadSceneAsync("Tutorial", LoadSceneMode.Additive);
            tutorialStarted = true;
            //Disable the tutorial button
            tutorialButton.interactable = false;
        }
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         DefaultPlayerPrefs                                                                                               *
    * DESCRIPTION:    Apply default settings at start if no settings exist.                                                            *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    private void DefaultPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("HasTimeLimit")) PlayerPrefs.SetInt("HasTimeLimit", 0);
        if (!PlayerPrefs.HasKey("TimeLimit")) PlayerPrefs.SetFloat("TimeLimit", 5.0f);
    }


    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         TimeLimitToggle                                                                                                  *
    * DESCRIPTION:    Toggle Time limit for the simulation.                                                                            *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void TimeLimitToggle()
    {
        if (timeLimitToggle.isOn)
        {
            PlayerPrefs.SetInt("HasTimeLimit", 1);
            timeLimitSlider.interactable = true;
            PlayerPrefs.SetFloat("TimeLimit", timeLimitSlider.value);
            timeLimitSliderTxt.text = "Set Time Limit(" + timeLimitSlider.value + " min)";
        }
        else if (!timeLimitToggle.isOn)
        {
            PlayerPrefs.SetInt("HasTimeLimit", 0);
            timeLimitSlider.interactable = false;
            PlayerPrefs.SetFloat("TimeLimit", 5.0f);
            timeLimitSliderTxt.text = "Set Time Limit(5 min)";
        }
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         TimeLimitSlider                                                                                                  *
    * DESCRIPTION:    Set time limit from slider.                                                                                      *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void TimeLimitSlider()
    {
        if (timeLimitSlider.interactable)
        {
            PlayerPrefs.SetFloat("TimeLimit", timeLimitSlider.value);
            timeLimitSliderTxt.text = "Set Time Limit(" + timeLimitSlider.value + " min)";
        }
    }

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         QuitGame                                                                                                         *
    * DESCRIPTION:    Quit the application.                                                                                            *
    * PARAMETERS:     ~ none ~                                                                                                         *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void QuitGame()
    {
        Application.Quit();
    }
}
