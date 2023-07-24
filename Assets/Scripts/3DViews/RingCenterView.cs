using RingInWater.Utility;
using System;
using UnityEngine;

namespace RingInWater.View
{
    public class RingCenterView : MonoBehaviourLogger
    {
        public bool isRingOnSpire { get; private set; } = false;
        public float ringEnteredSpireTime = 0;
        public event Action ringEnteredSpireChanged;
        public void ResetIsRingOnSpire()
        {
            isRingOnSpire = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!this.isRingOnSpire)
            {
                SpireStickView view = other.GetComponent<SpireStickView>();
                if (view != null)
                {
                    this.ringEnteredSpireTime = Time.time;
                    this.isRingOnSpire = true;
                    ringEnteredSpireChanged?.Invoke();
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            SpireStickView view = other.GetComponent<SpireStickView>();
            if (view != null)
            {
                this.isRingOnSpire = false;
                ringEnteredSpireChanged?.Invoke();
            }
        }
    }
}