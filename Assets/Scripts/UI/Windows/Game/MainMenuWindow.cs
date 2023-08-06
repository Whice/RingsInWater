using System;
using UnityEngine;
using UnityEngine.UI;

namespace RingInWater.UI
{
    public class MainMenuWindow : AbstractWindow
    {
        [SerializeField] private Button playButton = null;
        [SerializeField] private Button storeButton = null;
        [SerializeField] private Button exitButton = null;

        public event Action playStarted;
        private void OnPlayStarted()
        {
            this.playStarted?.Invoke();
            OpenWindow(typeof(GameWindow));
        }
        protected override void OnCreate()
        {
            this.playButton.onClick.AddListener(OnPlayStarted);
            this.exitButton.onClick.AddListener(() => Application.Quit());
        }

        protected override void OnWindowDestroy()
        {
            this.playButton.onClick.RemoveAllListeners();
            this.exitButton.onClick.RemoveAllListeners();
        }
    }
}