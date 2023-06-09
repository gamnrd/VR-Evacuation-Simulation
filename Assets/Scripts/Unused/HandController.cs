/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			HandController.cs                                                                                               *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains the HandController class                                                                               *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
using UnityEngine;


/// <summary>
/// XR Hand controller that allows for enabling and disabling multiple types of interactions
/// </summary>
public class HandController : MonoBehaviour
{
    /// <summary>
    /// Provides teleportation ability
    /// </summary>
    [SerializeField]
    private HandTeleportationInteractorController _teleportationInteractor;
    /// <summary>
    /// Provides UI raycast interaction ability
    /// </summary>
    [SerializeField]
    private HandUiInteractorController _uiInteractor;

    /// <summary>
    /// Sets whether the hand has teleportation interaction enabled
    /// </summary>
    /// <param name="isEnabled">Whether teleportation interaction should be enabled</param>
    public void SetTeleportationEnabled(bool isEnabled)
    {
        _teleportationInteractor.SetEnabled(isEnabled);
    }

    /// <summary>
    /// Sets whether the hand has UI interaction rays enabled
    /// </summary>
    /// <param name="isEnabled">Whether UI interaction is enabled</param>
    public void SetUIiInteractionEnabled(bool isEnabled)
    {
        _uiInteractor.SetEnabled(isEnabled);
    }

    /// <summary>
    /// Sets the amount of time for the hand's teleportation ray to take to go
    /// from its min to its max velocity as it projects out of the hand
    /// </summary>
    /// <param name="timeSecs">
    /// Amount of time for the hand's teleportation ray to take to go from its
    /// min to its max velocity as it shoots out
    /// </param>
    public void SetTeleportationCurveVelocityIncreaseTimeSecs(float timeSecs)
    {
        _teleportationInteractor.SetCurveVelocityIncreaseTimeSecs(timeSecs);
    }

    /// <summary>
    /// Sets the starting velocity of the hand's teleportation ray as it begins
    /// projecting out
    /// </summary>
    /// <param name="startVelocity">Starting speed of the hand's teleportation ray</param>
    public void SetTeleportationCurveStartVelocity(float startVelocity)
    {
        _teleportationInteractor.SetCurveStartVelocity(startVelocity);
    }

    /// <summary>
    /// Sets the target velocity for the hand's teleportation ray to ramp up to
    /// as it shoots out of the hand (before it stops moving)
    /// </summary>
    /// <param name="endVelocity">Target velocity for the hand's teleportation ray</param>
    public void SetTeleportationCurveEndVelocity(float endVelocity)
    {
        _teleportationInteractor.SetCurveEndVelocity(endVelocity);
    }

    /// <summary>
    /// Sets the color gradient of the hand's teleportation interaction ray
    /// </summary>
    /// <param name="gradient">Color gradient for the hand's teleportation ray</param>
    public void SetTeleportationRayValidColorGradient(Gradient gradient)
    {
        _teleportationInteractor.SetRayValidColorGradient(gradient);
    }

    /// <summary>
    /// Sets a color gradient for the hand's teleportation ray, when its
    /// pointing at an invalid target
    /// </summary>
    /// <param name="gradient">Color gradient of teleportation ray for invalid targets</param>
    public void SetTeleportationRayInvalidColorGradient(Gradient gradient)
    {
        _teleportationInteractor.SetRayInvalidColorGradient(gradient);
    }

    /// <summary>
    /// Sets the width of the hand's teleportation ray
    /// </summary>
    /// <param name="width">Width of teleportation ray</param>
    public void SetTeleportationRayWidth(float width)
    {
        _teleportationInteractor.SetRayWidth(width);
    }

    /// <summary>
    /// Sets the color gradient of the hand's UI interaction ray
    /// </summary>
    /// <param name="gradient">Color gradient for the hand' UI teleportation ray</param>
    public void SetUiRayValidColorGradient(Gradient gradient)
    {
        _uiInteractor.SetRayValidColorGradient(gradient);
    }

    /// <summary>
    /// Sets a color gradient for the hand's UI ray, when its pointing at an
    /// invalid target
    /// </summary>
    /// <param name="gradient">Color gradient of UI ray for invalid targets</param>
    public void SetUiRayInvalidColorGradient(Gradient gradient)
    {
        _uiInteractor.SetRayInvalidColorGradient(gradient);
    }


    /// <summary>
    /// Sets the width of the hand's UI ray
    /// </summary>
    /// <param name="width">Width of UI ray</param>
    public void SetUiRayWidth(float width)
    {
        _uiInteractor.SetRayWidth(width);
    }

    /// <summary>
    /// Sets prefab object for hand's teleportation ray reticle
    /// </summary>
    /// <param name="reticlePrefab">Reticle object for teleportation rays</param>
    public void SetTeleportationReticlePrefab(GameObject reticlePrefab)
    {
        _teleportationInteractor.SetReticlePrefab(reticlePrefab);
    }
}
