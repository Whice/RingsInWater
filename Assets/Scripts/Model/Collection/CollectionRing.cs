using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class CollectionRing : CollectionEntity
    {
        [SerializeField] private RingViewId ringViewId = RingViewId.unknown;

        public override int id
        {
            get => (int)this.ringViewId;
        }
    }
}