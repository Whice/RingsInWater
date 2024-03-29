﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RingInWater.UI
{
    public class WinLooseWindow : InGameWindow
    {
        [SerializeField] private TextMeshProUGUI winLooseTextField = null;
        [SerializeField] private TextMeshProUGUI winTimeTextField = null;
        [SerializeField] private Button restartButton = null;
        [SerializeField] private Button toMenuButton = null;

        public event Action gameRestared;
        private void OnGameRestarted()
        {
            this.gameRestared?.Invoke();
            OpenWindow(typeof(GameWindow));
        }
        public void SetWinLooseText(bool isWin, int timeRemaining)
        {
            this.winLooseTextField.text = isWin ? "Вы победили!" : "Вы проиграли!";
            this.winTimeTextField.text = $"Времени осталось: {timeRemaining}";
            this.winTimeTextField.gameObject.SetActive(isWin);
        }
        protected override void OnCreate()
        {
            this.restartButton.onClick.AddListener(OnGameRestarted);
            this.toMenuButton.onClick.AddListener(() => OpenWindow(typeof(MainMenuWindow)));
        }
        protected override void OnWindowDestroy()
        {
            this.restartButton.onClick.RemoveAllListeners();
            this.toMenuButton.onClick.RemoveAllListeners();
        }
    }
}