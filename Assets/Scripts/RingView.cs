using UnityEngine;

namespace View
{
    public class RingView : MonoBehaviour
    {
        [SerializeField] private Vector3 forceAxisMultiplier = Vector3.one;

        public Collider selfCollider { get; private set; }
        public Rigidbody ringBody { get; private set; }


        private void Awake()
        {
            this.ringBody = GetComponentInChildren<Rigidbody>();
            this.selfCollider = GetComponentInChildren<Collider>();
        }

        private Vector3 Mult(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }
        public void AddForceToBody(Vector3 fromPoint)
        {
            Vector3 direction = this.transform.position - fromPoint;

            ringBody.AddForce(Mult(direction, forceAxisMultiplier), ForceMode.Force);
        }
    }
}