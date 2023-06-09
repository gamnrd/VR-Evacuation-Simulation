/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			Timer.cs                                                                                                        *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains the TeleportReticle class.                                                                             *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

using DG.Tweening;
using UnityEngine;

/// <summary>
/// Behaviour for a teleportation interaction reticle that oscillates in size
/// </summary>
public class TeleportReticle : MonoBehaviour
{
    /// <summary>
    /// Max size for reticle in proportion to its starting size
    /// </summary>
    public float MaxScale = 1.3f;

    /// <summary>
    /// Amount of time to spend animating to max or min scale
    /// </summary>
    public float ScaleAnimationTimeSecs = 1f;

    /// <summary>
    /// Starts animation to scale the reticle as long as the reticle exists
    /// </summary>
    void Start()
    {
        transform.DOScale(MaxScale, ScaleAnimationTimeSecs)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(gameObject);
    }
}
