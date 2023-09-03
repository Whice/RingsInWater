using System;
using UnityEngine;
using UnityEngine.UI;

namespace RingInWater.UI
{
    public class MainMenuWindow : AbstractWindow
    {
        [SerializeField] private Button playButton = null;
        [SerializeField] private Button collectionButton = null;
        [SerializeField] private Button exitButton = null;

        public event Action playStarted;
        private void OnPlayStarted()
        {
            this.playStarted?.Invoke();
            OpenWindow(typeof(GameWindow));
        }
        public event Action collectionOpened;
        public void OnCollectionOpened()
        {
            this.collectionOpened?.Invoke();
            OpenWindow(typeof(CollectionWindow));
        }
        protected override void OnCreate()
        {
            IsNullCheck(this.playButton, this.playButton.name);
            IsNullCheck(this.collectionButton, this.collectionButton.name);
            IsNullCheck(this.exitButton, this.exitButton.name);

            this.playButton.onClick.AddListener(OnPlayStarted);
            this.collectionButton.onClick.AddListener(OnCollectionOpened);
            this.exitButton.onClick.AddListener(() => Application.Quit());
        }

        protected override void OnWindowDestroy()
        {
            this.playButton.onClick.RemoveAllListeners();
            this.collectionButton.onClick.RemoveAllListeners();
            this.exitButton.onClick.RemoveAllListeners();
        }
    }
}