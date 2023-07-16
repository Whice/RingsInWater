using RingInWater.Utility;
using System.Collections;
using UnityEngine;

namespace RingInWater.View
{
    public class RingView : MonoBehaviourLogger
    {
        [SerializeField] private RingCenterView ringCenterView = null;
        [SerializeField] private float ringOnSpireDisablePhisicsTime = 1f;
        public Collider selfCollider { get; private set; }
        public Rigidbody ringBody { get; private set; }
        public float xPosition
        {
            get => this.transform.position.x;
        }

        private Coroutine delayCoroutine;
        private IEnumerator DelayForPhisicsDisable()
        {
            yield return new WaitForSeconds(this.ringOnSpireDisablePhisicsTime);
            if (this.ringCenterView.isRingOnSpire)
            {
                this.ringBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                LogInfo("Phis is disable!");
            }
        }
        private void OnRingEnteredSpireChanged()
        {
            if (ringCenterView.isRingOnSpire)
            {
                this.delayCoroutine = StartCoroutine(DelayForPhisicsDisable());
            }
            else
            {
                if (delayCoroutine != null)
                {
                    StopCoroutine(this.delayCoroutine);
                    this.delayCoroutine = null;
                }
                this.ringBody.constraints = RigidbodyConstraints.FreezePositionZ;
            }
        }

        public void CheckLeftRightBorder()
        {
            float xPosition = this.transform.localPosition.x;
            if (xPosition > 10 || xPosition < -10)
            {
                this.ringBody.velocity = new Vector3(-5, this.ringBody.velocity.y, 0);

            }
        }
        private void Awake()
        {
            this.ringBody = GetComponentInChildren<Rigidbody>();
            this.selfCollider = GetComponentInChildren<Collider>();
            //Ограничить угловую скорость, чтобы кольца не вращались слишком сильно.
            this.ringBody.maxAngularVelocity = 3f;
            this.ringCenterView.ringEnteredSpireChanged += OnRingEnteredSpireChanged;
        }
        private void OnDestroy()
        {
            this.ringCenterView.ringEnteredSpireChanged -= OnRingEnteredSpireChanged;
        }

    }
}