using UnityEngine;
using UnityEngine.InputSystem;

public class Blooper : MonoBehaviour
{
    [SerializeField] private GamePieceManager manager;

    [SerializeField] private ConfigurableJoint joint;
    [SerializeField] private Transform spawnLocation;


    [SerializeField] private float extendTarget;

    [SerializeField] private float jointTarget;

    private InputAction _gamePiecePlace;

    private bool hasCube = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gamePiecePlace = InputSystem.actions.FindAction("Place");


        //Remove the usual starting piece and place a cube up on the blooper.
        manager.deletePiece();
        Instantiate(manager.getCube(), spawnLocation.position, spawnLocation.rotation);
    }

    // Update is called once per frame
    void Update()
    {

        if(_gamePiecePlace.WasPressedThisFrame() && hasCube)
        {
            jointTarget = extendTarget;
        } else if(!hasCube)
        {
            jointTarget = 0;
        }

        joint.targetRotation = Quaternion.Euler(jointTarget, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cube"))
        {
            hasCube = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Cube"))
        {
            hasCube = false;
        }
    }
}
