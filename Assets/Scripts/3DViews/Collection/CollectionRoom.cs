using Model;
using RingInWater.Utility;
using RingInWater.View.Providers;
using UnityEngine;
using Zenject;

namespace RingInWater.View
{
    public class CollectionRoom : MonoBehaviourLogger
    {
        [SerializeField] private Transform entity3DPlace = null;

        [Inject] private PlayerInfo playerInfo = null;
        [Inject] private AllViewsProvider allViewsProvider = null;

        private CollectionEntitiesProvider collectionEntitiesProvider
        {
            get => this.allViewsProvider.GetCollectionEntitiesProvider();
        }

        private void Awake()
        {
            this.playerInfo.collectionModel.collectionEntityChooseChanged += OnCollectionEntityChooseChanged;
        }

        private GameObject currentView;
        private void OnCollectionEntityChooseChanged(CollectionEntityType type, int id)
        {
            CollectionEntity[] entities = null;
            if (type == CollectionEntityType.ring)
            {
                entities = this.collectionEntitiesProvider.GetCollectionByType<CollectionRing>();
            }
            if (type == CollectionEntityType.spire)
            {
                entities = this.collectionEntitiesProvider.GetCollectionByType<CollectionSpire>();
            }

            if (!IsNullCheck(entities, nameof(entities) + "in collection room"))
            {
                if (this.currentView != null)
                {
                    Destroy(this.currentView);
                }

                foreach (CollectionEntity entity in entities)
                {
                    this.currentView = InstantiateWithInject(entity.view.gameObject, this.entity3DPlace);
                }
            }
        }
    }
}