/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			FadeOutCanvas.cs                                                                                                *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains the FadeOutCanvas class. It controll the fade out effect for UI.                                       *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeOutCanvas : MonoBehaviour
{
    public bool SkipFadeOut;

    [SerializeField]
    private float _fadeTimeSecs;
    [SerializeField]
    GameObject _camera;
    [SerializeField]
    private Image _fadeImage;

    /**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    * METHOD:         FadeOut                                                                                                          *
    * DESCRIPTION:    UI Fade out effect starts when called.                                                                           *
    * PARAMETERS:     Action onFadeEnd                                                                                                 *
    * RETURNS:        VOID                                                                                                             *
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
    public void FadeOut(Action onFadeEnd)
    {
        if (!SkipFadeOut && _fadeTimeSecs > 0)
        {
            _camera.SetActive(true);
            _fadeImage.DOFade(1, _fadeTimeSecs)
                .From(0)
                .OnComplete(() => {
                    onFadeEnd();
                })
                .SetAutoKill(true);
        }
        else
        {
            onFadeEnd();
        }
    }

}
