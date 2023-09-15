using Model;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RingInWater.UI
{
    public class CollectionWindow : AbstractWindow
    {
        [SerializeField] private Button toMenuButton = null;
        [SerializeField] private Button ringsButton = null;
        [SerializeField] private Button spiresButton = null;
        [SerializeField] private Button bubbleButton = null;

        [SerializeField] private CollectionChoosePanel collectionChoosePanel = null;

        [Inject] private PlayerInfo playerInfo = null;
        private CollectionModel collectionModel
        {
            get => this.playerInfo.collectionModel;
        }
        public event Action collectionClosed;
        private void OnCollectionClosed()
        {
            this.collectionClosed?.Invoke();    
            OpenPreviousWindow();
        }
        private void OnCollectionChanged(CollectionEntityType entityType)
        {
            this.collectionModel.SetEntityType(entityType);
            this.collectionChoosePanel.Initilize();
        }
        protected override void OnCreate()
        {
            IsNullCheck(this.toMenuButton, this.toMenuButton.name);
            IsNullCheck(this.ringsButton, this.ringsButton.name);
            IsNullCheck(this.spiresButton, this.spiresButton.name);
            IsNullCheck(this.bubbleButton, this.bubbleButton.name);

            this.toMenuButton.onClick.AddListener(OnCollectionClosed);
            //Визуала для рузырей пока нет.
            this.bubbleButton.interactable = false;

            this.ringsButton.onClick.AddListener(() => OnCollectionChanged(CollectionEntityType.ring));
            this.spiresButton.onClick.AddListener(() => OnCollectionChanged(CollectionEntityType.spire));
            this.bubbleButton.onClick.AddListener(() => OnCollectionChanged(CollectionEntityType.bubble));
        }

        protected override void OnWindowDestroy()
        {
        }
    }
}