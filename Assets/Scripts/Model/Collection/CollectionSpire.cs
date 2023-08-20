using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class CollectionSpire : CollectionEntity
    {
        [SerializeField] private SpiresViewId spiresViewId= SpiresViewId.unknown;
        public override int id
        {
            get => (int)this.spiresViewId;
        }
    }
}