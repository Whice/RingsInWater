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
        public void AddForceToBody(Vector3 fromPoint, float force)
        {
            Vector3 delta = this.transform.position - fromPoint;
            Vector3 direction = delta.normalized;
            force  -= Vector3.Distance(this.transform.position, fromPoint);
            if (force < 0) force = 0;

            this.ringBody.AddForce(Mult(direction, this.forceAxisMultiplier) * force, ForceMode.Force);
        }
    }
}