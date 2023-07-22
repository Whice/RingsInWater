using RingInWater.Utility;
using System.Collections;
using UnityEngine;

namespace RingInWater.View
{
    /// <summary>
    /// Представление кольца.
    /// </summary>
    public class RingView : MonoBehaviourLogger
    {
        [SerializeField] private RingCenterView ringCenterView = null;
        [SerializeField] private float ringOnSpireDisablePhisicsTime = 1f;
        /// <summary>
        /// Коллайде представления кольца.
        /// </summary>
        public Collider selfCollider { get; private set; }
        /// <summary>
        /// <see cref="Rigidbody"/> представления кольца.
        /// </summary>
        public Rigidbody ringBody { get; private set; }
        /// <summary>
        /// Положение кольца по x оси.
        /// </summary>
        public float xPosition
        {
            get => this.transform.position.x;
        }

        private Coroutine delayCoroutine;
        /// <summary>
        /// Сбросить состояние представления для переиспользования.
        /// </summary>
        public void ResetView()
        {
            this.ringBody.isKinematic = false;
        }
        /// <summary>
        /// Отключить физику с задержкой.
        /// </summary>
        /// <returns></returns>
        private IEnumerator DelayForPhisicsDisable()
        {
            yield return new WaitForSeconds(this.ringOnSpireDisablePhisicsTime);
            if (this.ringCenterView.isRingOnSpire)
            {
                this.ringBody.isKinematic = true;
            }
        }
        /// <summary>
        /// Обработать выход/попадание кольца на шпиль.
        /// </summary>
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
            }
        }
        /// <summary>
        /// Обработать событие воздействия силы на кольцо.
        /// </summary>
        public void OnForceAdded()
        {
            if (ringCenterView.isRingOnSpire)
            {
                if (delayCoroutine != null)
                    StopCoroutine(this.delayCoroutine);
                this.delayCoroutine = StartCoroutine(DelayForPhisicsDisable());
            }
        }

        public void SetActive(bool isActive)
        {
            this.gameObject.SetActive(isActive);
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