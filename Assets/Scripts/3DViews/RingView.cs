using Model;
using RingInWater.Utility;
using System.Collections;
using UnityEngine;

namespace RingInWater.View
{
    /// <summary>
    /// Представление кольца.
    /// </summary>
    public class RingView : MonoBehaviourLogger, IWaveMovable, IViewWithId
    {
        [SerializeField] private RingViewId viewId = RingViewId.unknown;
        /// <summary>
        /// Визуальная часть центра кольца.
        /// Нужна для определения надевания кольца на шпиль.
        /// </summary>
        [SerializeField] private RingCenterView ringCenterView = null;
        [SerializeField] private float ringOnSpireDisablePhisicsTime = 1f;
        [SerializeField] private MeshRenderer ringMeshRenderer = null;
        [SerializeField] private float durationColorChanged = 1f;
        [SerializeField] private Vector3 ringSizePrivate = Vector3.one;

        public int idInt
        {
            get => (int)viewId;
        }
        /// <summary>
        /// Разрмер view кольца в пространстве.
        /// </summary>
        public Vector3 ringSize
        {
            get => this.ringSizePrivate;
        }
        private Material ringMaterial
        {
            get => this.ringMeshRenderer.material;
        }
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
            this.isSetKinematick = false;
            this.ringCenterView.ResetIsRingOnSpire();
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
                StartCoroutine(SetWhite());
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
        private float leftTime = 0;
        private Color originalColor;
        private bool isSetKinematick = false;
        private IEnumerator SetWhite()
        {
            if (!this.isSetKinematick)
            {
                this.isSetKinematick = true;
                Color result = this.originalColor;
                while (result != Color.white)
                {
                    this.leftTime += Time.deltaTime;
                    result = Color.Lerp(this.originalColor, Color.white, this.leftTime / this.durationColorChanged);
                    this.ringMaterial.SetColor("_BaseColor", result);
                    yield return null;
                }
                this.leftTime = 0;
                while (result != this.originalColor)
                {
                    this.leftTime += Time.deltaTime;
                    result = Color.Lerp(Color.white, this.originalColor, this.leftTime / this.durationColorChanged);
                    this.ringMaterial.SetColor("_BaseColor", result);
                    yield return null;
                }
            }
        }
        private void Awake()
        {
            this.selfRigidbody = GetComponentInChildren<Rigidbody>();
            this.selfCollider = GetComponentInChildren<Collider>();
            //Ограничить угловую скорость, чтобы кольца не вращались слишком сильно.
            this.selfRigidbody.maxAngularVelocity = 3f;
            this.ringCenterView.ringEnteredSpireChanged += OnRingEnteredSpireChanged;
            this.originalColor = this.ringMaterial.GetColor("_BaseColor");
            ResetView();
        }
        private void OnDestroy()
        {
            this.ringCenterView.ringEnteredSpireChanged -= OnRingEnteredSpireChanged;
        }
    }
}