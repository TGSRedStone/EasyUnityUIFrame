using UnityEngine;

namespace EasyUIFrame.GamePlay.GameScene
{
    public class PlayerControl : MonoBehaviour
    {
        private Rigidbody rigidBody;
        public float speed = 5;
        public static float PlayerSpeed;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            var horizontal = Input.GetAxis("Horizontal");//AD
            var vertical = Input.GetAxis("Vertical");    // WS
            rigidBody.AddForce(Vector3.forward * (vertical * speed));
            rigidBody.AddForce(Vector3.right * (horizontal * speed));
            PlayerSpeed = rigidBody.velocity.magnitude;
        }
    }
}
