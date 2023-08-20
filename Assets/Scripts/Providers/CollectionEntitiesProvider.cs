using Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RingInWater.View.Providers
{
    [CreateAssetMenu(fileName = nameof(CollectionEntitiesProvider), menuName = "Providers/" + nameof(CollectionEntitiesProvider))]
    public class CollectionEntitiesProvider : ScriptableObject
    {
        [SerializeField] private CollectionSpire[] collectionSpires = new CollectionSpire[0];
        [SerializeField] private CollectionRing[] collectionRings = new CollectionRing[0];

        private Dictionary<Type, CollectionEntity[]> collections;

        public CollectionEntity[] GetCollectionByType<T>() where T : CollectionEntity
        {
            if (this.collections == null)
            {
                this.collections = new Dictionary<Type, CollectionEntity[]>
                {
                    { typeof(CollectionSpire), this.collectionSpires },
                    { typeof(CollectionRing), this.collectionRings }
                };
            }
            Type type = typeof(T);
            if (this.collections.ContainsKey(type))
            {
                return this.collections[type];
            }
            else
            {
                Debug.LogError("No non-abstract inheritor with the specified type was found!");
                return null;
            }
        }

        public void CheckByUnknown()
        {
            foreach (CollectionSpire view in this.collectionSpires)
            {
                if (view.id == 0)
                {
                    Debug.LogError($"Ring view with name: {view.name} isn't set id!");
                }
                view.CheckValid();
            }
            foreach (CollectionRing view in this.collectionRings)
            {
                if (view.id == 0)
                {
                    Debug.LogError($"Spire view with name: {view.name} isn't set id!");
                }
                view.CheckValid();
            }
        }
    }
}