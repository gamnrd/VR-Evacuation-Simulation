/**~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 * TEAM:		    Virtuality                                                                                                      *
 * MEMBERS:		    Justin Fink, Mike Hammond, Luka Horiuchi, Deric Kruse, Josh Murphy, Leon Vong                                   *
 *                                                                                                                                  *
 * PROJECT:			Game Design - Cambridge Memorial Hosptial VR                                                                    *
 * FILE:			WalkingPatient.cs                                                                                               *
 * FINAL VERSION:	2023/04/13                                                                                                      *
 * DESCRIPTION:		Contains the UserController class.                                                                              *
 *                                                                                                                                  *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */
using System;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Provides configurable and toggleable XR movement and ui interaction
/// </summary>
[RequireComponent(typeof(ActionBasedContinuousMoveProvider))]
[RequireComponent(typeof(ActionBasedSnapTurnProvider))]
[RequireComponent(typeof(TeleportationProvider))]
[RequireComponent(typeof(LocomotionSystem))]
[RequireComponent(typeof(XROrigin))]
public class UserController : MonoBehaviour
{
    /// <summary>
    /// Enumeration of different types of movement modes
    /// </summary>
    [Serializable]
    public enum MovementMode
    {
        None = 0,
        Smooth,
        Teleport
    }

    [SerializeField]
    HandController _leftHandController;
    [SerializeField]
    HandController _rightHandController;

    [SerializeField]
    private bool _isRightHanded = true;
    /// <summary>
    /// Amount to snap turn
    /// </summary>
    [SerializeField]
    private int _rotationIncrementsDegrees = 45;
    /// <summary>
    /// Current movement mode
    /// </summary>
    [SerializeField]
    private MovementMode _movementMode = MovementMode.Smooth;
    [SerializeField]
    private bool _isUiInteractionEnabled = true;

    [SerializeField]
    private Gradient _teleportationRayValidColorGradient;
    [SerializeField]
    private Gradient _teleportationRayInvalidColorGradient;
    [SerializeField]
    private float _teleportationRayWidth = .02f;
    [SerializeField]
    private float _teleportationCurveStartVelocity = 12;
    [SerializeField]
    private float _teleportationCurveEndVelocity = 16;
    [SerializeField]
    private float _teleportationCurveVelocityIncreaseTimeSecs = .5f;
    [SerializeField]
    private GameObject _teleportationReticlePrefab;

    [SerializeField]
    private Gradient _uiRayValidColorGradient;
    [SerializeField]
    private Gradient _uiRayInvalidColorGradient;
    [SerializeField]
    private float _uiRayWidth = .02f;

    /// <summary>
    /// Provides smooth movement ability
    /// </summary>
    private ActionBasedContinuousMoveProvider _continuousMoveProvider;
    /// <summary>
    /// Provides turning in fixed increment ability
    /// </summary>
    private ActionBasedSnapTurnProvider _snapTurnProvider;
    private TeleportationProvider _teleportationProvider;

    /// <summary>
    /// Sets the current movement mode of the user controller
    /// </summary>
    /// <param name="movementMode">Movement mode for user to use</param>
    public void SetMovementMode(MovementMode movementMode)
    {
        SetHandDependentMovement(movementMode, _isRightHanded);
    }

    /// Sets whether the user controller is right handed or left handed
    /// </summary>
    /// <param name="isRightHanded">True for right handed, false for left handed</param>
    public void SetHandedness(bool isRightHanded) 
    {
        SetHandDependentMovement(_movementMode, isRightHanded);
    }

    /// <summary>
    /// Disallows the user controller from performing any movement (rotation included)
    /// </summary>
    public void DisableAllMovement()
    {
        SetHandDependentMovement(MovementMode.None, _isRightHanded);
    }

    /// <summary>
    /// Sets the user controller's movement mode to teleporting
    /// </summary>
    public void ActivateTeleporting()
    {
        SetHandDependentMovement(MovementMode.Teleport, _isRightHanded);
    }

    /// <summary>
    /// Sets the user controller's movement mode to smooth/continous moving
    /// </summary>
    public void ActivateSmoothMovement()
    {
        SetHandDependentMovement(MovementMode.Smooth, _isRightHanded);
    }

    /// <summary>
    /// Toggles whether the user controller is right handed or left handed. 
    /// </summary>
    public void ToggleHandedness()
    {
        SetHandedness(!_isRightHanded);
    }

    /// <summary>
    /// Sets whether the user can interact with UI elements
    /// </summary>
    /// <param name="isUiInteractionEnabled">True to allow user controller to
    /// interact with UI, otherwise false</param>
    public void SetUiInteractionEnabled(bool isUiInteractionEnabled)
    {
        _isUiInteractionEnabled = isUiInteractionEnabled;
        _leftHandController.SetUIiInteractionEnabled(isUiInteractionEnabled);
        _rightHandController.SetUIiInteractionEnabled(isUiInteractionEnabled);
    }

    /// <summary>
    /// Sets the amount that the user controller snap turns
    /// </summary>
    /// <param name="rotationIncrementDegrees">Amount for snap turns</param>
    public void SetRotationIncrementDegrees(int rotationIncrementDegrees)
    {
        _rotationIncrementsDegrees = rotationIncrementDegrees;
        _snapTurnProvider.turnAmount = _rotationIncrementsDegrees;
    }

    /// <summary>
    /// Caches references to components
    /// </summary>
    private void Awake()
    {
        _continuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
        _snapTurnProvider = GetComponent<ActionBasedSnapTurnProvider>();
        _teleportationProvider = GetComponent<TeleportationProvider>();
    }

    /// <summary>
    /// Sets starting properties for left and right hands then initializes movement and ui interaction
    /// </summary>
    void Start()
    {
        // apply visual settings to both left and right hands
        Action<HandController> ApplyGlobalHandSettings = (HandController hand) =>
        {
            hand.SetTeleportationCurveStartVelocity(_teleportationCurveStartVelocity);
            hand.SetTeleportationCurveEndVelocity(_teleportationCurveEndVelocity);
            hand.SetTeleportationCurveVelocityIncreaseTimeSecs(_teleportationCurveVelocityIncreaseTimeSecs);
            hand.SetTeleportationRayValidColorGradient(CopyGradient(_teleportationRayValidColorGradient));
            hand.SetTeleportationRayInvalidColorGradient(CopyGradient(_teleportationRayInvalidColorGradient));
            hand.SetTeleportationRayWidth(_teleportationRayWidth);
            hand.SetUiRayValidColorGradient(CopyGradient(_uiRayValidColorGradient));
            hand.SetUiRayInvalidColorGradient(CopyGradient(_uiRayInvalidColorGradient));
            hand.SetUiRayWidth(_uiRayWidth);
            hand.SetTeleportationReticlePrefab(_teleportationReticlePrefab);
        };

        ApplyGlobalHandSettings(_rightHandController);
        ApplyGlobalHandSettings(_leftHandController);

        // initialize movement and interaction
        SetHandDependentMovement(_movementMode, _isRightHanded);
        SetRotationIncrementDegrees(_rotationIncrementsDegrees);
        SetUiInteractionEnabled(_isUiInteractionEnabled);
    }

    /// <summary>
    /// Updates capabilities and properties of left and right hand based on current
    /// movement mode and UI interaction capability
    /// </summary>
    private void SetHandDependentMovement(MovementMode movementMode, bool isRightHanded)
    {
        // disable all previous movement before enabling new movement
        _snapTurnProvider.rightHandSnapTurnAction.action.Disable();
        _snapTurnProvider.leftHandSnapTurnAction.action.Disable();
        _snapTurnProvider.enabled = false;
        _continuousMoveProvider.rightHandMoveAction.action.Disable();
        _continuousMoveProvider.leftHandMoveAction.action.Disable();
        _continuousMoveProvider.enabled = false;
        _rightHandController.SetTeleportationEnabled(false);
        _leftHandController.SetTeleportationEnabled(false);
        _teleportationProvider.enabled = false;
    
        // set movement and handedness
        _isRightHanded = isRightHanded;
        _movementMode = movementMode;

        // selectively enable new movement
        switch (_movementMode)
        {
            case MovementMode.Smooth:
                _snapTurnProvider.enabled = true;
                _continuousMoveProvider.enabled = true;
                if (_isRightHanded) // right-hand move, left hand turn
                {
                    _continuousMoveProvider.rightHandMoveAction.action.Enable();
                    _snapTurnProvider.leftHandSnapTurnAction.action.Enable();
                }
                else // left-hand move, right-hand turn
                {
                    _continuousMoveProvider.leftHandMoveAction.action.Enable();
                    _snapTurnProvider.rightHandSnapTurnAction.action.Enable();
                }
                break;
            case MovementMode.Teleport:
                _snapTurnProvider.enabled = true;
                _teleportationProvider.enabled = true;
                if (_isRightHanded) // right-hand teleport, left hand turn
                {
                    _rightHandController.SetTeleportationEnabled(true);
                    _snapTurnProvider.leftHandSnapTurnAction.action.Enable();
                }
                else // left-hand teleport, right hand turn
                {
                    _leftHandController.SetTeleportationEnabled(true);
                    _snapTurnProvider.rightHandSnapTurnAction.action.Enable();
                }
                break;
        }
    }

    /// <summary>
    /// Copies a color gradient
    /// </summary>
    /// <param name="g">Gradient to copy</param>
    /// <returns>Copy of g</returns>
    private static Gradient CopyGradient(Gradient g)
    {
        Gradient copy = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[g.colorKeys.Length];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[g.alphaKeys.Length];
        Array.Copy(g.colorKeys,colorKeys,colorKeys.Length);
        Array.Copy(g.alphaKeys,alphaKeys,alphaKeys.Length);
        copy.SetKeys(colorKeys, alphaKeys);
        return g;
    }
}
