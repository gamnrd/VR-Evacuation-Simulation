/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			HandUiInteractorController.cs                                                                                   *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains the HandUiInteractorController class                                                                   *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


/// <summary>
/// Behaviour for an XR Ui ray-interactor of a hand
/// </summary>
[RequireComponent(typeof(XRRayInteractor))]
[RequireComponent(typeof(XRInteractorLineVisual))]
[RequireComponent(typeof(LineRenderer))]
public class HandUiInteractorController : MonoBehaviour
{
    private XRRayInteractor _rayInteractor;
    private XRInteractorLineVisual _lineVisual;
    private LineRenderer _lineRenderer;

    /// <summary>
    /// Sets color of UI interaction ray when pointing at a valid target
    /// </summary>
    public void SetRayValidColorGradient(Gradient gradient)
    {
        _lineVisual.validColorGradient = gradient;
    }

    /// <param name="gradient">Color of ui interaction ray</param>
    /// <summary>
    /// Sets color of UI interaction ray when pointing at an invalid target
    /// </summary>
    /// <param name="gradient">Color of ui interaction ray</param>
    public void SetRayInvalidColorGradient(Gradient gradient)
    {
        _lineVisual.invalidColorGradient = gradient;
    }

    /// <summary>
    /// Sets the width of the UI interaction ray
    /// </summary>
    /// <param name="width">UI interaction ray width</param>
    public void SetRayWidth(float width)
    {
        _lineVisual.lineWidth = width;
    }

    /// <summary>
    /// Sets whether ui interactor is enabled
    /// </summary>
    public void SetEnabled(bool enabled)
    {
        _rayInteractor.enabled = enabled;
        _lineVisual.enabled = enabled;
    }

    /// <summary>
    /// Caches references to required components
    /// </summary>
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _rayInteractor = GetComponent<XRRayInteractor>();
        _lineVisual = GetComponent<XRInteractorLineVisual>();
    }
}
