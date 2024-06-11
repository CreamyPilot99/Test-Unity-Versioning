using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
        Rigidbody rigidBody = new Rigidbody();
        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                Move();
            }
        }

        public void Move()
        {
            SubmitPositionRequestRpc();
        }

        //[Rpc(SendTo.Server)]
        void SubmitPositionRequestRpc(ClientRpcParams rpcParams = default)
        {
            var randomPosition = GetRandomPositionOnPlane();
            transform.position = randomPosition;
            Position.Value = randomPosition;
        }

        static Vector3 GetRandomPositionOnPlane()
        {
            return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
        }
        void Start()
        {
            Rigidbody rigidBody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.W))
                rigidBody.velocity += Vector3.forward * 1.0f;
            if (Input.GetKey(KeyCode.S))
                rigidBody.velocity += Vector3.back * 1.0f;
            if (Input.GetKey(KeyCode.A))
                rigidBody.velocity += Vector3.left * 1.0f;
            if (Input.GetKey(KeyCode.D))
                rigidBody.velocity += Vector3.right * 1.0f;
        }
    }
}