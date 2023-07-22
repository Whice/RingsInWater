using RingInWater.Utility;
using System;
using System.Collections;
using UnityEngine;

namespace RingInWater.View
{
    public class BubbleView : MonoBehaviourLogger, IWaveMovable
    {
        public event Action<bool, BubbleView> activeChanged;
        private Vector3 startPoint;
        public float maxBubblesVisibleHeight;

        /// <summary>
        /// <see cref="Rigidbody"/> представления.
        /// </summary>
        public Rigidbody selfRigidbody { get; private set; }
        /// <summary>
        /// Положение по x оси.
        /// </summary>
        public Vector3 position
        {
            get => this.transform.position;
        }

        public void SetActive(bool isActive)
        {
            this.gameObject.SetActive(isActive);
            this.startPoint = this.transform.position;
            this.activeChanged?.Invoke(isActive, this);
            if (isActive)
            {
                StartCoroutine(CheckHeight());
            }
        }
        private IEnumerator CheckHeight()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.3f);
                if (this.transform.position.y - this.startPoint.y > maxBubblesVisibleHeight)
                {
                    SetActive(false);
                    break;
                }
            }
        }
        public void SetSize(float size)
        {
            this.transform.localScale = new Vector3(size, size, size);
        }
        private static System.Random random = new System.Random(0);
        /// <summary>
        /// Установить нглубину по оси z.
        /// </summary>
        public void SetZAxisDeep()
        {
            this.transform.localPosition = new Vector3
                (
                 this.transform.localPosition.x,
                 this.transform.localPosition.y,
                -10f
                );
            this.startPoint = this.transform.position;
        }
        public void SetRandomPosition()
        {

            this.transform.localPosition = new Vector3
                (
                random.Next(1, 200) / 100f,
                random.Next(1, 200) / 100f,
                -10f
                );
            SetZAxisDeep();
        }

        private void Awake()
        {
            this.selfRigidbody = this.GetComponent<Rigidbody>();
        }
    }
}