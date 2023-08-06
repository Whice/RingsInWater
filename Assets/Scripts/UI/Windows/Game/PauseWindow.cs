using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RingInWater.UI
{
    public class PauseWindow : AbstractWindow
    {
        [SerializeField] private Button continueButton = null;
        [SerializeField] private Button restartButton = null;
        [SerializeField] private Button toMenuButton = null;

        public event Action gameRestared;
        private void OnGameRestarted()
        {
            this.gameRestared?.Invoke();
            OpenWindow(typeof(GameWindow));
        }
        protected override void OnCreate()
        {
            this.continueButton.onClick.AddListener(() => OpenWindow(typeof(GameWindow)));
            this.restartButton.onClick.AddListener(OnGameRestarted);
            this.toMenuButton.onClick.AddListener(() => OpenWindow(typeof(MainMenuWindow)));
        }
        protected override void OnWindowDestroy()
        {
            this.continueButton.onClick.RemoveAllListeners();
            this.restartButton.onClick.RemoveAllListeners();
            this.toMenuButton.onClick.RemoveAllListeners();
        }
    }
}