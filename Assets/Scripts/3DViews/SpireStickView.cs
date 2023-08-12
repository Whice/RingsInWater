using DG.Tweening;
using RingInWater.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace RingInWater.View
{
    public class SpireStickView : MonoBehaviourLogger
    {
        [SerializeField] private SpireView spireView = null;
        /// <summary>
        /// Добавить кольцо на палку шпиля, чтобы оно осталось тут навсегда.
        /// </summary>
        /// <param name="ringView"></param>
        public void AddRing(RingView ringView)
        {
            spireView.AddRing(ringView);
        }
    }
}