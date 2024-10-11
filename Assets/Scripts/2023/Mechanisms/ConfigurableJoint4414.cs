using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mechanisms
{
    public class ConfigurableJoint4414 : MonoBehaviour
    {
        [SerializeField] private ConfigurableJoint stage1;
        [SerializeField] private ConfigurableJoint stage2;
        [SerializeField] private ConfigurableJoint stage3;
        [SerializeField] private ConfigurableJoint pivot;
        [SerializeField] private ConfigurableJoint wrist;
        [SerializeField] private ConfigurableJoint intake;

        [SerializeField] private float elevatorStowDistance;
        [SerializeField] private float pivotStowAngle;
        [SerializeField] private float wristStowAngle;

        [SerializeField] private float elevatorStowDistanceCube;
        [SerializeField] private float pivotStowAngleCube;
        [SerializeField] private float wristStowAngleCube;

        [SerializeField] private float elevatorHighDistance;
        [SerializeField] private float pivotHighAngle;
        [SerializeField] private float wristHighAngle;
        [SerializeField] private float elevatorMiddleDistance;
        [SerializeField] private float pivotMiddleAngle;
        [SerializeField] private float wristMiddleAngle;
        [SerializeField] private float elevatorLowDistance;
        [SerializeField] private float pivotLowAngle;
        [SerializeField] private float wristLowAngle;
        [SerializeField] private float elevatorSubstationIntakeDistance;
        [SerializeField] private float pivotSubstationIntakeAngle;
        [SerializeField] private float wristSubstationIntakeAngle;
        [SerializeField] private float elevatorGroundIntakeDistance;
        [SerializeField] private float pivotGroundIntakeAngle;
        [SerializeField] private float wristGroundIntakeAngle;
        [SerializeField] private float intakeGroundIntakeAngle;

        [SerializeField] private float elevatorHighDistanceCube;
        [SerializeField] private float pivotHighAngleCube;
        [SerializeField] private float wristHighAngleCube;
        [SerializeField] private float elevatorMiddleDistanceCube;
        [SerializeField] private float pivotMiddleAngleCube;
        [SerializeField] private float wristMiddleAngleCube;
        [SerializeField] private float elevatorLowDistanceCube;
        [SerializeField] private float pivotLowAngleCube;
        [SerializeField] private float wristLowAngleCube;
        [SerializeField] private float elevatorSubstationIntakeDistanceCube;
        [SerializeField] private float pivotSubstationIntakeAngleCube;
        [SerializeField] private float wristSubstationIntakeAngleCube;
        [SerializeField] private float elevatorGroundIntakeDistanceCube;
        [SerializeField] private float pivotGroundIntakeAngleCube;
        [SerializeField] private float wristGroundIntakeAngleCube;
        [SerializeField] private float intakeGroundIntakeAngleCube;

        [SerializeField] private float elevatorTargetDistance;
        [SerializeField] private float pivotTargetAngle;
        [SerializeField] private float wristTargetAngle;
        [SerializeField] private float intakeTargetAngle;

        private RobotState _previousRobotState;
        private RobotState _currentRobotState;

        private GamePieceManager _gamePieceManager;

        private bool _canMove = true;

        private InputAction _stowAction;
        private InputAction _highAction;
        private InputAction _middleAction;
        private InputAction _lowAction;
        private InputAction _intakeDoubleSubstationAction;
        private InputAction _intakeGroundAction;
        private InputAction _placeGamePieceAction;

        private void Start()
        {
            _gamePieceManager = GetComponent<GamePieceManager>();

            _stowAction = InputSystem.actions.FindAction("Stow");
            _highAction = InputSystem.actions.FindAction("High");
            _middleAction = InputSystem.actions.FindAction("Middle");
            _lowAction = InputSystem.actions.FindAction("Low");
            _intakeDoubleSubstationAction = InputSystem.actions.FindAction("IntakeDoubleSubstation");
            _intakeGroundAction = InputSystem.actions.FindAction("IntakeGround");
            _placeGamePieceAction = InputSystem.actions.FindAction("Place");

            _previousRobotState = RobotState.Stow;
            _currentRobotState = RobotState.Stow;
        }

        private void Update()
        {
            if (GameManager.canRobotMove && _canMove)
            {
                if (_stowAction.triggered || _intakeGroundAction.WasReleasedThisFrame() ||
                    _intakeDoubleSubstationAction.WasReleasedThisFrame())
                {

                    if (_gamePieceManager.currentGamePieceMode == GamePieceType.Cube)
                    {
                        elevatorTargetDistance = elevatorStowDistanceCube;
                        pivotTargetAngle = pivotStowAngleCube;
                        wristTargetAngle = wristStowAngleCube;
                        intakeTargetAngle = 0;
                    }
                     else
                    {
                        elevatorTargetDistance = elevatorStowDistance;
                        pivotTargetAngle = pivotStowAngle;
                        wristTargetAngle = wristStowAngle;
                        intakeTargetAngle = 0;
                    }
                   

                    _currentRobotState = RobotState.Stow;

                }
                else if (_highAction.WasPressedThisFrame())
                {
                    if (_gamePieceManager.currentGamePieceMode == GamePieceType.Cube)
                    {
                        elevatorTargetDistance = elevatorHighDistanceCube;
                        pivotTargetAngle = pivotHighAngleCube;
                        wristTargetAngle = wristHighAngleCube;
                        intakeTargetAngle = 0;
                    }
                    else
                    {
                        elevatorTargetDistance = elevatorHighDistance;
                        pivotTargetAngle = pivotHighAngle;
                        wristTargetAngle = wristHighAngle;
                        intakeTargetAngle = 0;
                    }

                    _currentRobotState = RobotState.High;
                }
                else if (_middleAction.triggered)
                {
                    if (_gamePieceManager.currentGamePieceMode == GamePieceType.Cube)
                    {
                        elevatorTargetDistance = elevatorMiddleDistanceCube;
                        pivotTargetAngle = pivotMiddleAngleCube;
                        wristTargetAngle = wristMiddleAngleCube;
                        intakeTargetAngle = 0;
                    }
                    else
                    {
                        elevatorTargetDistance = elevatorMiddleDistance;
                        pivotTargetAngle = pivotMiddleAngle;
                        wristTargetAngle = wristMiddleAngle;
                        intakeTargetAngle = 0;
                    }

                    _currentRobotState = RobotState.Middle;
                }
                else if (_lowAction.triggered)
                {
                    if (_gamePieceManager.currentGamePieceMode == GamePieceType.Cube)
                    {
                        elevatorTargetDistance = elevatorLowDistanceCube;
                        pivotTargetAngle = pivotLowAngleCube;
                        wristTargetAngle = wristLowAngleCube;
                        intakeTargetAngle = intakeGroundIntakeAngleCube;
                    }
                    else
                    {
                        elevatorTargetDistance = elevatorLowDistance;
                        pivotTargetAngle = pivotLowAngle;
                        wristTargetAngle = wristLowAngle;
                        intakeTargetAngle = 0;
                    }

                    _currentRobotState = RobotState.Low;
                }
                else if (_intakeDoubleSubstationAction.triggered)
                {
                    if (_gamePieceManager.currentGamePieceMode == GamePieceType.Cube)
                    {
                        elevatorTargetDistance = elevatorSubstationIntakeDistanceCube;
                        pivotTargetAngle = pivotSubstationIntakeAngleCube;
                        wristTargetAngle = wristSubstationIntakeAngleCube;
                        intakeTargetAngle = 0;
                    }
                    else
                    {
                        elevatorTargetDistance = elevatorSubstationIntakeDistance;
                        pivotTargetAngle = pivotSubstationIntakeAngle;
                        wristTargetAngle = wristSubstationIntakeAngle;
                        intakeTargetAngle = 0;
                    }

                    _currentRobotState = RobotState.IntakeDoubleSubstation;
                }
                else if ((_intakeGroundAction.triggered && !_previousRobotState.Equals(RobotState.Low)) ||
                         (_intakeGroundAction.triggered &&
                          !_previousRobotState.Equals(RobotState.IntakeDoubleSubstation)))
                {

                    if(_gamePieceManager.currentGamePieceMode == GamePieceType.Cube)
                    {
                        elevatorTargetDistance = elevatorGroundIntakeDistanceCube;
                        pivotTargetAngle = pivotGroundIntakeAngleCube;
                        wristTargetAngle = wristGroundIntakeAngleCube;
                        intakeTargetAngle = intakeGroundIntakeAngleCube;
                    } else
                    {
                        elevatorTargetDistance = elevatorGroundIntakeDistance;
                        pivotTargetAngle = pivotGroundIntakeAngle;
                        wristTargetAngle = wristGroundIntakeAngle;
                        intakeTargetAngle = intakeGroundIntakeAngle;
                    }

                    _currentRobotState = RobotState.IntakeGround;
                }

                if (_placeGamePieceAction.WasPressedThisFrame() && _gamePieceManager.hasGamePiece && !_gamePieceManager.isPlacing &&
                    _gamePieceManager.canPlace && _currentRobotState == RobotState.Low && _gamePieceManager.currentGamePieceMode == GamePieceType.Cube)
                {
                    StartCoroutine(ReverseIntakeEject());
                }
            }


            stage1.targetPosition = new Vector3(0, elevatorTargetDistance/3, 0);
            stage2.targetPosition = new Vector3(0, elevatorTargetDistance/3, 0);
            stage3.targetPosition = new Vector3(0, elevatorTargetDistance/3, 0);
            pivot.targetRotation = Quaternion.Euler(0, -pivotTargetAngle, 0);
            wrist.targetRotation = Quaternion.Euler(0, wristTargetAngle, 0);
            intake.targetRotation = Quaternion.Euler(0, intakeTargetAngle, 0);


            _previousRobotState = _currentRobotState;
        }

        private IEnumerator MoveArmAndIntake(float stage1Distance, float stage2Distance, float wristAngle,
            float intakeAngle, bool moveWrist = true)
        {
            _canMove = false;
            stage1.targetPosition = new Vector3(0, -stage1Distance, 0);
            stage2.targetPosition = new Vector3(0, -stage2Distance, 0);
            intake.targetRotation = Quaternion.Euler(intakeAngle, 0, 0);
            if (moveWrist)
            {
                wrist.targetRotation = Quaternion.Euler(-wristAngle, 0, 0);
            }

            _canMove = true;
            yield return null;
        }

        private IEnumerator ReverseIntakeEject()
        {
            /*_gamePieceManager.isPlacing = true;
            if (_gamePieceManager.currentGamePieceMode == GamePieceType.Cube)
            {
                _gamePieceManager.StartCoroutine(_gamePieceManager.PlaceSequence(GamePieceType.Cube));
                yield return new WaitForSeconds(0.25f);
                _canMove = false;
                stage1.targetPosition = new Vector3(0, 0, 0);
                stage2.targetPosition = new Vector3(0, 0, 0);
                intake.targetRotation = Quaternion.Euler(0, 0, 0);
                wrist.targetRotation = Quaternion.Euler(0, 0, 0);
                yield return new WaitForSeconds(0.1f);
                _canMove = true;
                yield break;
            }

            _canMove = false;
            stage1.targetPosition = new Vector3(0, -elevatorTargetDistance, 0);
            //stage2.targetPosition = new Vector3(0, -stage2TargetDistance, 0);
            intake.targetRotation = Quaternion.Euler(intakeTargetAngle, 0, 0);
            wrist.targetRotation = Quaternion.Euler(-90, 0, 0);
            _canMove = true;
            yield return new WaitForSeconds(0.25f);
            _gamePieceManager.StartCoroutine(_gamePieceManager.PlaceSequence(GamePieceType.Cone));
            yield return new WaitForSeconds(0.1f);
            _canMove = false;
            stage1.targetPosition = new Vector3(0, 0, 0);
            stage2.targetPosition = new Vector3(0, 0, 0);
            intake.targetRotation = Quaternion.Euler(0, 0, 0);
            wrist.targetRotation = Quaternion.Euler(0, 0, 0);
            yield return new WaitForSeconds(0.1f);
            _canMove = true;*/

            intakeTargetAngle = intakeGroundIntakeAngle;

            yield return null;
        }
    }
}