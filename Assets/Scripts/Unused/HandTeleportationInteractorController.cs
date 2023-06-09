/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			HandTeleportationInteractorController.cs                                                                        *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains the HandTeleportationInteractorController class                                                        *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

using DG.Tweening;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.InputSystem.InputAction;

/// <summary>
/// Behaviour for a teleportation interactor. Allows setting visual properties.
/// Animates the teleportation ray when projected.
/// </summary>
public class HandTeleportationInteractorController : MonoBehaviour
{
    [SerializeField]
    private XRRayInteractor _rayInteractor;
    [SerializeField]
    private XRInteractorLineVisual _rayVisual;

    private float _curveStartVelocity = 12;
    private float _curveEndVelocity = 16;
    private float _curveVelocityIncreaseTimeSecs = .5f;
    private Tween _tween;

    /// <summary>
    /// SEts the prefab object to use as the teleportation target reticle
    /// </summary>
    /// <param name="reticlePrefab">Reticle for teleportation targets</param>
    public void SetReticlePrefab(GameObject reticlePrefab)
    {
        _rayVisual.reticle?.SetActive(false);
        _rayVisual.reticle = reticlePrefab;
    }


    /// <summary>
    /// Sets color of teleportation interaction ray when pointing at a valid target
    /// </summary>
    /// <param name="gradient">Color of teleportation interaction ray</param>
    public void SetRayValidColorGradient(Gradient gradient)
    {
        _rayVisual.validColorGradient = gradient;
    }

    /// <summary>
    /// Sets color of teleportation interaction ray when pointing at an invalid target
    /// </summary>
    /// <param name="gradient">Color of teleportation interaction ray</param>
    public void SetRayInvalidColorGradient(Gradient gradient)
    {
        _rayVisual.invalidColorGradient = gradient;
    }

    /// <summary>
    /// Sets the width of the teleportation interaction ray
    /// </summary>
    /// <param name="width">teleportation interaction ray width</param>
    public void SetRayWidth(float width) 
    { 
        _rayVisual.lineWidth = width;
    }

    /// <summary>
    /// Sets the velocity for the teleportation ray when it first shoots out of a hand
    /// </summary>
    /// <param name="startVelocity">Starting velocity of teleportation ray</param>
    public void SetCurveStartVelocity(float startVelocity)
    {
        _curveStartVelocity = startVelocity;
        UpdateTweenParameters();
    }

    /// <summary>
    /// Sets the target velocity of the teleportation ray 
    /// </summary>
    /// <param name="targetVelocity">Target velocity of teleportation ray</param>
    public void SetCurveEndVelocity(float targetVelocity)
    {
        _curveEndVelocity = targetVelocity;
        UpdateTweenParameters();
    }

    /// <summary>
    /// Sets amount of time that the teleportation ray spends projecting
    /// </summary>
    /// <param name="time">Amount of time to project</param>

    public void SetCurveVelocityIncreaseTimeSecs(float value)
    {
        _curveVelocityIncreaseTimeSecs = value;
        UpdateTweenParameters();
    }

    /// <summary>
    /// Shows the teleportation ray
    /// </summary>

    public void ActivateRay()
    {
        SetRayEnabled(true);
    }

    /// <summary>
    /// Hides the teleportation ray
    /// </summary>
    public void DeactivateRay()
    {
        SetRayEnabled(false);
    }

    /// <summary>
    /// Sets whether the teleportation ray is showing or hidden
    /// </summary>
    /// <param name="enabled">True to show the teleportation ray, false to hide it</param>
    public void SetRayEnabled(bool enabled)
    {
        if (_rayInteractor.enabled)
        {
            _rayInteractor.allowSelect = enabled;
            _rayVisual.enabled = enabled;
            _rayVisual.reticle.SetActive(enabled);

            if (enabled)
            {
                _tween.Restart();
            }
        }
    }

    /// <summary>
    /// Sets whether teleportation is enabled
    /// </summary>
    /// <param name="enabled">Whether teleportation is enabled</param>
    public void SetEnabled(bool enabled)
    {
        _rayInteractor.enabled = enabled;
        _rayInteractor.allowSelect = false;
        if (!enabled)
        {
            _rayVisual.enabled = false;
            _rayVisual.reticle.SetActive(false);
        }
    }

    /// <summary>
    /// Input action callback. controlls enabling or disabling the
    /// teleportation ray.
    /// </summary>
    /// <param name="cc">Contains Vector2 input</param>
    public void OnTeleportActivate(CallbackContext cc)
    {
        SetRayEnabled(cc.ReadValue<Vector2>().y > 0);
    }


    /// <summary>
    /// Sets initial animation properties for teleportation ray
    /// </summary>

    void Start()
    {
        SetEnabled(false);
        UpdateTweenParameters();
    }


    /// <summary>
    /// Cleans up resources
    /// </summary>
    private void OnDestroy()
    {
        _tween.Kill();
    }

    /// <summary>
    /// Resets animation with latest visual settings
    /// </summary>
    private void UpdateTweenParameters()
    {
        _tween?.Kill();
        _tween = DOTween
            .To(() => _rayInteractor.velocity, x => _rayInteractor.velocity = x, _curveEndVelocity, _curveVelocityIncreaseTimeSecs)
            .From(_curveStartVelocity)
            .SetAutoKill(false)
            .SetEase(Ease.OutSine);
    }
}
