using RingInWater.Utility;
using System;
using System.Collections;
using UnityEngine;

namespace RingInWater.View
{
    public class BubbleView : MonoBehaviourLogger
    {
        public event Action<bool, BubbleView> activeChanged;
        private Vector3 startPoint;
        public void SetActive(bool isActive)
        {
            this.gameObject.SetActive(isActive);
            this.startPoint = this.transform.position;
            this.activeChanged?.Invoke(isActive, this);
            if (isActive)
            {
                StartCoroutine(CheckBorders());
            }
        }
        private IEnumerator CheckBorders()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.3f);
                if (this.transform.position.y - this.startPoint.y > 30)
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
        public void SetRandomPosition()
        {

            this.transform.localPosition = new Vector3
                (
                random.Next(1, 200) / 100f,
                random.Next(1, 200) / 100f,
                0
                );
            this.startPoint = this.transform.position;
        }

    }
}