using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mechanisms
{
    public class ConfigurableJoint3641 : MonoBehaviour
    {
        [SerializeField] private ConfigurableJoint armPivot;
        [SerializeField] private ConfigurableJoint stage1;
        [SerializeField] private ConfigurableJoint stage2;
        [SerializeField] private ConfigurableJoint wrist;

        private Transform pivotRB;
        private Rigidbody extendRB;
        private Rigidbody wristRB;

        private Vector3 starting;

        [SerializeField] private float wristRetractedAngle;
        [SerializeField] private float wristExtendedAngle;

        [SerializeField] private float armHighAngle;
        [SerializeField] private float elevatorHighDistance;
        [SerializeField] private bool wristHigh;
        [SerializeField] private float armMiddleAngle;
        [SerializeField] private float elevatorMiddleDistance;
        [SerializeField] private bool wristMiddle;
        [SerializeField] private float armLowAngle;
        [SerializeField] private float elevatorLowDistance;
        [SerializeField] private bool wristLow;

        [SerializeField] private float armSubstationIntakeAngle;
        [SerializeField] private float elevatorSubstationIntakeDistance;
        [SerializeField] private bool wristSubstation;

        [SerializeField] private float armGroundIntakeAngle;
        [SerializeField] private float elevatorGroundIntakeDistance;
        [SerializeField] private bool wristGroundIntake;


        [SerializeField] private float armHighAngleCube;
        [SerializeField] private float elevatorHighDistanceCube;
        [SerializeField] private bool wristHighCube;

        [SerializeField] private float armMiddleAngleCube;
        [SerializeField] private float elevatorMiddleDistanceCube;
        [SerializeField] private bool wristMiddleCube;

        [SerializeField] private float armLowAngleCube;
        [SerializeField] private float elevatorLowDistanceCube;
        [SerializeField] private bool wristLowCube;

        [SerializeField] private float armSubstationIntakeAngleCube;
        [SerializeField] private float elevatorSubstationIntakeDistanceCube;
        [SerializeField] private bool wristSubstationCube;

        [SerializeField] private float armGroundIntakeAngleCube;
        [SerializeField] private float elevatorGroundIntakeDistanceCube;
        [SerializeField] private bool wristGroundCube;


        [SerializeField] private float armTargetAngle;
        [SerializeField] private float elevatorTargetDistance;
        [SerializeField] private bool wristExtended;

        private RobotState _previousRobotState;
        private RobotState _currentRobotState;

        private GamePieceManager _gamePieceManager;

        private InputAction _stowAction;
        private InputAction _highAction;
        private InputAction _middleAction;
        private InputAction _lowAction;
        private InputAction _intakeDoubleSubstationAction;
        private InputAction _intakeGroundAction;

        private void Start()
        {
            _gamePieceManager = GetComponent<GamePieceManager>();

            _stowAction = InputSystem.actions.FindAction("Stow");
            _highAction = InputSystem.actions.FindAction("High");
            _middleAction = InputSystem.actions.FindAction("Middle");
            _lowAction = InputSystem.actions.FindAction("Low");
            _intakeDoubleSubstationAction = InputSystem.actions.FindAction("IntakeDoubleSubstation");
            _intakeGroundAction = InputSystem.actions.FindAction("IntakeGround");

            pivotRB = armPivot.GetComponent<Transform>();
            extendRB = stage1.GetComponent<Rigidbody>();
            wristRB = wrist.GetComponent<Rigidbody>();

            starting = pivotRB.localRotation.eulerAngles;

        }

        private void Update()
        {
            if (GameManager.canRobotMove)
            {
                if (_stowAction.triggered || _intakeGroundAction.WasReleasedThisFrame())
                {
                    StopAllCoroutines();

                    StartCoroutine(retractFrom(0, 0, false));

                    _currentRobotState = RobotState.Stow;
                }
                else if (_highAction.WasPressedThisFrame())
                {
                    if (_gamePieceManager.currentGamePieceMode == GamePieceType.Cube)
                    {
                        StopAllCoroutines();

                        StartCoroutine(extendTo(armHighAngleCube, elevatorHighDistanceCube, wristHighCube));

                    }
                    else
                    {
                        StopAllCoroutines();

                        StartCoroutine(extendTo(armHighAngle, elevatorHighDistance, wristHigh));
                    }

                    _currentRobotState = RobotState.High;
                }
                else if (_middleAction.WasPressedThisFrame())
                {
                    if (_gamePieceManager.currentGamePieceMode == GamePieceType.Cube)
                    {
                        StopAllCoroutines();
                        StartCoroutine(extendTo(armMiddleAngleCube, elevatorMiddleDistanceCube, wristMiddleCube));

                    }
                    else
                    {
                        StopAllCoroutines();

                        StartCoroutine(extendTo(armMiddleAngle, elevatorMiddleDistance, wristMiddle));

                    }

                    _currentRobotState = RobotState.Middle;
                }
                else if (_lowAction.WasPressedThisFrame())
                {
                    if (_gamePieceManager.currentGamePieceMode == GamePieceType.Cube)
                    {
                        armTargetAngle = armLowAngleCube;
                        elevatorTargetDistance = elevatorLowDistanceCube;
                    }
                    else
                    {
                        armTargetAngle = armLowAngle;
                        elevatorTargetDistance = elevatorLowDistance;

                    }

                    _currentRobotState = RobotState.Low;
                }
                else if (_intakeDoubleSubstationAction.WasPressedThisFrame())
                {

                    _gamePieceManager.shouldSpawnCone = true;

                    _currentRobotState = RobotState.IntakeDoubleSubstation;
                }
                else if ((_intakeGroundAction.WasPressedThisFrame() && !_previousRobotState.Equals(RobotState.Low)) || 
                            (_intakeGroundAction.WasPressedThisFrame() && !_previousRobotState.Equals(RobotState.IntakeDoubleSubstation)))
                {


                    if (_gamePieceManager.currentGamePieceMode == GamePieceType.Cube)
                    {


                        if (_currentRobotState == RobotState.Stow)
                        {
                            StopAllCoroutines();

                            StartCoroutine(extendTo(armGroundIntakeAngleCube, elevatorGroundIntakeDistanceCube, wristGroundCube));
                        }
                        else
                        {
                            StopAllCoroutines();

                            StartCoroutine(retractFrom(armGroundIntakeAngleCube, elevatorGroundIntakeDistanceCube, wristGroundIntake));

                        }
                    }
                    else
                    {

                        if (_currentRobotState == RobotState.Stow)
                        {
                            StopAllCoroutines();

                            StartCoroutine(extendTo(armGroundIntakeAngle, elevatorGroundIntakeDistance, wristGroundIntake));
                        }
                        else
                        {
                            StopAllCoroutines();

                            StartCoroutine(retractFrom(armGroundIntakeAngle, elevatorGroundIntakeDistance, wristGroundIntake));

                        }
                    }

                    _currentRobotState = RobotState.IntakeGround;
                }
            }
            
            armPivot.targetRotation = Quaternion.Euler(armTargetAngle, 0, 0);
            stage1.targetPosition = new Vector3(0, 0, -elevatorTargetDistance / 2);
            stage2.targetPosition = new Vector3(0, 0, -elevatorTargetDistance);
            wrist.targetRotation = Quaternion.Euler(wristExtended ? wristExtendedAngle : wristRetractedAngle, 0, 0);



            _previousRobotState = _currentRobotState;
        }

        private float getActualAngle(float eulerAngleReading)
        {
            //in testing, should be about 55, progressively decreases to zero and then up
            if(eulerAngleReading <= starting.z)
            {
                return starting.z - eulerAngleReading;
            } else
            {
                return starting.z + (360 - eulerAngleReading);
            }
        }

        private IEnumerator extendTo(float armAngle, float elevatorDistance, bool wrist)
        {

            if(armAngle > 160)
            {
                armTargetAngle = 160;
                yield return new WaitForSeconds(0.2f);
                armTargetAngle = armAngle;
            } else
            {
                armTargetAngle = armAngle;
            }

            //while (Mathf.Abs(getActualAngle(pivotRB.localEulerAngles.z) - armTargetAngle) > 10f)
            //{
                
            //    yield return null;
            //}

            elevatorTargetDistance = elevatorDistance;
            wristExtended = wrist;
        }

        private IEnumerator retractFrom(float armAngle, float elevatorDistance, bool wrist)
        {

            elevatorTargetDistance = elevatorDistance;
            wristExtended = wrist;

            yield return new WaitForSeconds(0.2f);

            if (armAngle < 90 && pivotRB.localRotation.eulerAngles.z > 90)
            {
                armTargetAngle = 80;
                yield return new WaitForSeconds(0.2f);
                armTargetAngle = armAngle;
            }
            else
            {
                armTargetAngle = armAngle;
            }
        }
    }
}