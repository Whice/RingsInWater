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
        [SerializeField] private int defaultEntityID = 1;
        [SerializeField] private CollectionEntityType defaultCollectionEntityType = CollectionEntityType.ring;

        [Inject] private PlayerInfo playerInfo = null;
        [Inject] private AllViewsProvider allViewsProvider = null;

        private CollectionEntitiesProvider collectionEntitiesProvider
        {
            get => this.allViewsProvider.GetCollectionEntitiesProvider();
        }
        private CollectionModel collectionModel
        {
            get => this.playerInfo.collectionModel;
        }

        private void Awake()
        {
            //Сделать все кольца и шпили доступными игроку, для тестов!!!
            foreach (CollectionEntity entity in this.collectionEntitiesProvider.GetCollectionByType<CollectionRing>())
            {
                this.playerInfo.AddRingIdToAvailable(entity.id);
            }
            foreach (CollectionEntity entity in this.collectionEntitiesProvider.GetCollectionByType<CollectionSpire>())
            {
                this.playerInfo.AddSpireIdToAvailable(entity.id);
            }

            this.collectionModel.SetDefaultValues(this.defaultEntityID, this.defaultCollectionEntityType);
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
                    if (this.collectionModel.currentEnityId == entity.id)
                        this.currentView = InstantiateWithInject(entity.view.gameObject, this.entity3DPlace);
            }
        }
    }
}