using Model;
using RingInWater.Utility;
using RingInWater.View.Providers;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEngine.EventSystems.EventTrigger;

namespace RingInWater.View
{
    public class CollectionRoom : MonoBehaviourLogger
    {
        [SerializeField] private Transform spirePlace = null;
        [SerializeField] private Transform[] ringPlaces = new Transform[0];
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


        private GameObject currentSpireView;
        private GameObject[] currentRingViews;

        private Dictionary<int, CollectionEntity> currentEnitiesByType = new Dictionary<int, CollectionEntity>();
        private CollectionEntityType currentCollectionType;
        private void CreateRings()
        {
            foreach (GameObject ring in this.currentRingViews)
                if (ring != null)
                    Destroy(ring);

            if (this.currentEnitiesByType.TryGetValue((int)this.playerInfo.currentRingViewId, out CollectionEntity collectionEntity))
            {
                for (int i = 0; i < this.currentRingViews.Length; i++)
                {
                    this.currentRingViews[i] = InstantiateWithInject(collectionEntity.view.gameObject, this.ringPlaces[i]);
                }
            }
            else
            {
                LogError($"{nameof(this.currentEnitiesByType)} haven't id: {this.collectionModel.currentEnityId}");
            }
        }
        private void CreateSpire()
        {
            if (this.currentSpireView != null)
            {
                Destroy(this.currentSpireView);
            }

            if (this.currentEnitiesByType.TryGetValue((int)this.playerInfo.currentSpiresViewId, out CollectionEntity collectionEntity))
            {
                this.currentSpireView = InstantiateWithInject(collectionEntity.view.gameObject, this.spirePlace);
                Destroy(this.currentSpireView.GetComponentInChildren<SpireStickView>());
                Destroy(this.currentSpireView.GetComponentInChildren<SpireView>());
            }
            else
            {
                LogError($"{nameof(this.currentEnitiesByType)} haven't id: {this.collectionModel.currentEnityId}");
            }
        }
        private void OnCollectionEntityChooseChanged(CollectionEntityType type, int id)
        {
            //Если сменился тип коллекции, то обновить словарь с объектами для отображения
            if (type != this.currentCollectionType)
            {
                this.currentCollectionType = type;
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
                    this.currentEnitiesByType.Clear();
                    foreach (CollectionEntity entity in entities)
                    {
                        this.currentEnitiesByType[entity.id] = entity;
                    }
                }
            }
            //Если сменился id, то робновить непосредственно сам внений вид.
            else
            {
                switch (type)
                {
                    case CollectionEntityType.ring:
                        {
                            CreateRings();
                            break;
                        }
                    case CollectionEntityType.spire:
                        {
                            CreateSpire();
                            break;
                        }
                    case CollectionEntityType.bubble:
                        {
                            //ToDo: еще нет реализации для пузырей.
                            break;
                        }
                }
            }
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

            currentRingViews = new GameObject[this.ringPlaces.Length];

            this.collectionModel.SetDefaultValues(this.defaultEntityID, this.defaultCollectionEntityType);
            this.playerInfo.collectionModel.collectionEntityChooseChanged += OnCollectionEntityChooseChanged;

            //Создать кольца и шпиль при входе в коллекцию.
            OnCollectionEntityChooseChanged(CollectionEntityType.ring, 0);
            OnCollectionEntityChooseChanged(CollectionEntityType.spire, 0);
        }
    }
}