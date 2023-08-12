using DG.Tweening;
using ModestTree;
using RingInWater.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RingInWater.View
{
    public class SpireView : MonoBehaviourLogger
    {
        /// <summary>
        /// Минимальная высота, на которую опуститься кольцо,
        /// когда оно попадет на шпиль.
        /// </summary>
        [SerializeField] private Transform ringMinPosition = null;
        /// <summary>
        /// Длительность приведения кольца к егго конечному положению.
        /// </summary>
        [SerializeField] private float ringStopDuration = 2f;


        private List<RingView> ringViews = new List<RingView>();
        private RingView lastRingView
        {
            get => this.ringViews[this.ringViews.Count - 1];
        }

        public void ResetSpire()
        {
            ringViews.Clear();
        }
        /// <summary>
        /// Добавить кольцо на палку шпиля, чтобы оно осталось тут навсегда.
        /// </summary>
        /// <param name="ringView"></param>
        public void AddRing(RingView ringView)
        {
            ringView.transform.SetParent(this.transform, true);
            ringView.selfRigidbody.isKinematic = true;
            Vector3 zeroRotaion = Vector3.zero;
            //Посчитать поворот
            {
                Vector3 ringRotation = ringView.transform.rotation.eulerAngles;
                ringView.transform.rotation = Quaternion.Euler(ringRotation.x % 360, ringRotation.y % 360, ringRotation.z % 360);
                float ringRotationX = Mathf.Abs(ringRotation.x % 360);
                int mult = ringRotation.x > 0 ? 1 : -1;
                if (ringRotationX > 90 && ringRotationX < 270)
                {
                    zeroRotaion = new Vector3(180 * mult, 0, 0);
                }
                else if (ringRotationX > 270)
                {
                    zeroRotaion = new Vector3(360 * mult, 0, 0);
                }
            }
            ringView.transform.DOLocalRotate(zeroRotaion, this.ringStopDuration);

            //Посчитать положение.
            Vector3 endPosition = new Vector3
                        (
                        this.transform.position.x,
                        this.transform.position.y + ringView.ringSize.y * this.ringViews.Count,
                        this.transform.position.z
                        );
            ringView.transform.DOMove(endPosition, this.ringStopDuration);
            this.ringViews.Add(ringView);
        }
    }
}