using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SingleSubstation : MonoBehaviour
{
    [SerializeField] private GameObject cone;
    [SerializeField] private GameObject cube;
    [SerializeField] private Transform tossSpawnLocation;
    [SerializeField] private Transform noseSpawnLocation;
    [SerializeField] private float spawnForce;
    [SerializeField] private float spawnDelay;


    public List<SingleSubRequest> requests = new List<SingleSubRequest>();

    private bool _canSpawn = true;
    
    private InputAction _spawnGamePiece;

    
    private void Start()
    {

        _spawnGamePiece = InputSystem.actions.FindAction("SpawnSingleSubstation");
    }
    
    private void Update()
    {
        if (requests.Count > 0 && _canSpawn)
        {
            _canSpawn = false;

            SingleSubRequest req = requests[0];
            GameObject spawnedGameObject;
            switch(req.deliverType)
            {
                case DeliverType.Toss:

                    spawnedGameObject = Instantiate(req.pieceType == GamePieceType.Cube ? cube : cone, tossSpawnLocation.position, Quaternion.identity);
                    spawnedGameObject.GetComponent<Rigidbody>().AddForce(tossSpawnLocation.forward * spawnForce, ForceMode.VelocityChange);
                    spawnedGameObject.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere * spawnForce, ForceMode.VelocityChange);
                    break;
                case DeliverType.NoseFirst:
                    spawnedGameObject = Instantiate(req.pieceType == GamePieceType.Cube ? cube : cone, noseSpawnLocation.position, noseSpawnLocation.rotation);
                    spawnedGameObject.GetComponent<Rigidbody>().AddForce(noseSpawnLocation.forward * spawnForce, ForceMode.VelocityChange);
                    break;

            }

            requests.Remove(req);

            StartCoroutine(ResetSpawn());
        }
    }
    
    private IEnumerator ResetSpawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        _canSpawn = true;
    }

    
}

public class SingleSubRequest
{
    public GamePieceType pieceType;
    public DeliverType deliverType;


    public SingleSubRequest(GamePieceType type, DeliverType deliverType)
    {
        this.pieceType = type;
        this.deliverType = deliverType;
    }
}

public enum DeliverType
{
    Toss,
    NoseFirst
}