using System;
using Unity.VisualScripting;
using UnityEngine;

namespace View
{
    public class RingView : MonoBehaviour
    {
        public Collider selfCollider { get; private set; }
        public Rigidbody ringBody { get; private set; }
        public float xPosition
        {
            get=>this.transform.position.x;
        }

        private void Awake()
        {
            this.ringBody = GetComponentInChildren<Rigidbody>();
            this.selfCollider = GetComponentInChildren<Collider>();
        }
        public void CheckLeftRightBorder()
        {
            float xPosition = this.transform.localPosition.x;
            if (xPosition > 10 || xPosition < -10)
            {
                this.ringBody.velocity = new Vector3(-5, this.ringBody.velocity.y, 0);   
            }
        }
    }
}