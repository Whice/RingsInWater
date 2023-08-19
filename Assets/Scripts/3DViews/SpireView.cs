using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Model;
using RingInWater.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RingInWater.View
{
    public class SpireView : MonoBehaviourLogger, IViewWithId
    {
        [SerializeField] private SpiresViewId viewId = SpiresViewId.unknown;
        /// <summary>
        /// Минимальная высота, на которую опуститься кольцо,
        /// когда оно попадет на шпиль.
        /// </summary>
        [SerializeField] private Transform ringMinPosition = null;
        /// <summary>
        /// Длительность приведения кольца к егго конечному положению.
        /// </summary>
        [SerializeField] private float ringStopDuration = 2f;

        public int idInt
        {
            get => (int)viewId;
        }


        private List<RingView> ringViews = new List<RingView>();
        private RingView lastRingView
        {
            get => this.ringViews[this.ringViews.Count - 1];
        }
        public int ringsCount
        {
            get => this.ringViews.Count;
        }
        private List<TweenerCore<Quaternion, Vector3, QuaternionOptions>> twinsRotate = new List<TweenerCore<Quaternion, Vector3, QuaternionOptions>>();
        private List<TweenerCore<Vector3, Vector3, VectorOptions>> twinsMove = new List<TweenerCore<Vector3, Vector3, VectorOptions>>();
        public void ResetSpire()
        {
            foreach (var twin in this.twinsRotate)
            {
                twin.Kill();
            }
            foreach (var twin in this.twinsMove)
            {
                twin.Kill();
            }
            this.twinsRotate.Clear();
            this.twinsMove.Clear();
            this.ringViews.Clear();
        }
        public event Action ringAdded;
        private float GetEndRotateForAxis(float axisRotateValue)
        {
            if (axisRotateValue > 90 && axisRotateValue < 270)
            {
                return 180;
            }
            else if (axisRotateValue > 270)
            {
                return 360;
            }

            return 0;
        }
        /// <summary>
        /// Добавить кольцо на палку шпиля, чтобы оно осталось тут навсегда.
        /// </summary>
        /// <param name="ringView"></param>
        public void AddRing(RingView ringView)
        {
            if (!this.ringViews.Contains(ringView))
            {
                ringView.transform.SetParent(this.transform, true);
                ringView.selfRigidbody.isKinematic = true;
                Vector3 zeroRotaion = Vector3.zero;
                //Посчитать поворот
                {
                    Vector3 ringRotation = ringView.transform.rotation.eulerAngles;
                    ringView.transform.rotation = Quaternion.Euler(ringRotation.x % 360, ringRotation.y % 360, ringRotation.z % 360);
                    float ringRotationX = Mathf.Abs(ringRotation.x % 360);
                    float ringRotationZ = Mathf.Abs(ringRotation.z % 360);
                    int mult = ringRotation.x > 0 ? 1 : -1;
                    zeroRotaion = new Vector3

                        (
                        GetEndRotateForAxis(ringRotationX) * mult,
                        0,
                        GetEndRotateForAxis(ringRotationZ)
                        );
                }
                this.twinsRotate.Add(ringView.transform.DOLocalRotate(zeroRotaion, this.ringStopDuration));

                //Посчитать положение.
                Vector3 endPosition = new Vector3
                            (
                            this.transform.position.x,
                            this.ringMinPosition.position.y + ringView.ringSize.y * this.ringViews.Count,
                            this.transform.position.z
                            );
                this.twinsMove.Add(ringView.transform.DOMove(endPosition, this.ringStopDuration));
                this.ringViews.Add(ringView);
                this.ringAdded?.Invoke();
            }
        }
    }
}