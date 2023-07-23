using RingInWater.Utility;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace RingInWater.View
{
    /// <summary>
    /// Представление кольца.
    /// </summary>
    public class RingView : MonoBehaviourLogger, IWaveMovable
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
        public Rigidbody selfRigidbody { get; private set; }
        /// <summary>
        /// Положение кольца по x оси.
        /// </summary>
        public Vector3 position
        {
            get => this.transform.position;
        }

        private Coroutine delayCoroutine;
        private void StopDelayCoroutine()
        {
            if (delayCoroutine != null)
            {
                StopCoroutine(this.delayCoroutine);
                this.delayCoroutine = null;
            }
        }
        /// <summary>
        /// Сбросить состояние представления для переиспользования.
        /// </summary>
        public void ResetView()
        {
            this.selfRigidbody.isKinematic = false;
            StopDelayCoroutine();
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
                this.selfRigidbody.isKinematic = true;
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
                StopDelayCoroutine();
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
            this.selfRigidbody = GetComponentInChildren<Rigidbody>();
            this.selfCollider = GetComponentInChildren<Collider>();
            //Ограничить угловую скорость, чтобы кольца не вращались слишком сильно.
            this.selfRigidbody.maxAngularVelocity = 3f;
            this.ringCenterView.ringEnteredSpireChanged += OnRingEnteredSpireChanged;
        }
        private void OnDestroy()
        {
            this.ringCenterView.ringEnteredSpireChanged -= OnRingEnteredSpireChanged;
        }
    }
}