using Model;
using RingInWater.Utility;
using RingInWater.View;
using RingInWater.View.Providers;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RingInWater.UI
{
    public class CollectionChoosePanel : MonoBehaviourLogger
    {
        [SerializeField] private CollectionEntityVisualizator entityVisualizatorTemplate = null;
        [SerializeField] private Transform visualizatorsParent = null;

        [Inject] private PlayerInfo playerInfo = null;
        [Inject] private AllViewsProvider allViewsProvider = null;
        private CollectionModel collectionModel
        {
            get => this.playerInfo.collectionModel;
        }
        private CollectionEntitiesProvider collectionEntitiesProvider
        {
            get=>this.allViewsProvider.GetCollectionEntitiesProvider();
        }
        private Stack<CollectionEntityVisualizator> unactiveEntity = new Stack<CollectionEntityVisualizator>();
        private List<CollectionEntityVisualizator> activeEntity = new List<CollectionEntityVisualizator>();
        /// <summary>
        /// Добавить и активировать новый отображаемый объект выбора.
        /// </summary>
        /// <param name="entity"></param>
        private void AddEntity(CollectionEntity entity)
        {
            if(this.unactiveEntity.Count == 0)
            {
                this.unactiveEntity.Push(InstantiateWithInject(this.entityVisualizatorTemplate, this.visualizatorsParent));
            }

            CollectionEntityVisualizator visualizator = this.unactiveEntity.Pop();
            visualizator.Initialize(entity);
            visualizator.SetActiveObject(true);
            this.activeEntity.Add(visualizator);
        }
        /// <summary>
        /// Отключить все отображаемые объекты выбора.
        /// </summary>
        private void DeactivateAllVisualizators()
        {
            for (int i = this.activeEntity.Count - 1; i >= 0; i--)
            {
                CollectionEntityVisualizator visualizator = this.activeEntity[i];
                visualizator.SetActiveObject(false);
                this.unactiveEntity.Push(visualizator);
            }
            this.activeEntity.Clear();
        }
        public void Initilize()
        {
            DeactivateAllVisualizators();
            CollectionEntityType type = this.collectionModel.currentCollectionEntityType;
            CollectionEntity[] collectionEntities = null;
            switch(type)
{
                case CollectionEntityType.ring:
                    {
                        collectionEntities = this.collectionEntitiesProvider.GetCollectionByType<CollectionRing>();
                        break;
                    }
                case CollectionEntityType.spire:
                    {
                        collectionEntities = this.collectionEntitiesProvider.GetCollectionByType<CollectionSpire>();
                        break;
                    }
                /*case CollectionEntityType.bubble:
                    {
                        collectionEntities = this.collectionEntitiesProvider.GetCollectionByType<CollectionRing>();
                        break;
                    }*/
            }

            foreach (CollectionEntity entity in collectionEntities)
            {
                AddEntity(entity);
            }
        }
    }
}