using RingInWater.UI;
using RingInWater.Utility;
using RingInWater.View;
using System;
using UnityEngine;

namespace RingInWater
{
    public class SceneController : MonoBehaviourLogger
    {
        [SerializeField] private WindowsController windowsController = null;
        [SerializeField] private RoomController roomController = null;
        [SerializeField] private float timeForGame= 90;

        private class GameData
        {
            private float startGameTime;
            private float gameTime;
            private readonly float maxGameTime;
            public int gameTimeLeft
            {
                get => (int)(maxGameTime - gameTime);
            }
            /// <summary>
            /// Время для игры кончилось.
            /// </summary>
            public bool isEndGame
            {
                get => this.maxGameTime - this.gameTime < 0;
            }
            public void ResetData(float startGameTime)
            {
                this.startGameTime = startGameTime;
            }
            public event Action gameEnded;
            public void Update(float time)
            {
                this.gameTime = time - this.startGameTime;
                if (this.isEndGame)
                {
                    this.gameEnded?.Invoke();
                }
            }

            public GameData(float maxGameTime)
            {
                this.maxGameTime = maxGameTime;
            }
        }

        private void OnGameRestarted()
        {
            this.roomController.ResetGame();
            this.gameData.ResetData(Time.time);
        }
        /// <summary>
        /// Активировать игровой мир или остановить в нем время.
        /// Если используются игровые окна, то время просто останавливается.
        /// </summary>
        /// <param name="isActive">Отключить объект полностью.</param>
        /// <param name="isWolrdStop">Остановить процессы во времени.</param>
        private void OnActive3DWorld(bool isActive, bool isWolrdStop)
        {
            this.roomController.gameObject.SetActive(isActive);
            if (isActive)
            {
                Time.timeScale = isWolrdStop ? 1.0f : 0.0f;
            }

        }

        private void OnWindowChanged()
        {
            AbstractWindow current = this.windowsController.currentWindow;
            OnActive3DWorld(current is InGameWindow, current is GameWindow);
        }
        private void OnBubbleCreated(int index)
        {
            this.roomController.bubbleSpawner.CreateBubbles(index);
            this.roomController.ringsController.AddForceFromPoint(index);
        }

        private MainMenuWindow mainMenuWindow;
        private PauseWindow pauseWindow;
        private GameWindow gameWindow;
        private WinLooseWindow winLooseWindow;

        private GameData gameData;

        public void OnGameTimeEnded()
        {
            this.winLooseWindow.SetWinLooseText(!this.gameData.isEndGame);

            this.windowsController.OpenWindow<WinLooseWindow>();
        }
        public void OnRingOnSpireChanged(int rings)
        {
            if (this.roomController.ringsController.maxRingsCount == rings)
                OnGameTimeEnded();
        }

        private void Awake()
        {
            this.windowsController.windowChanged += OnWindowChanged;
            this.mainMenuWindow = this.windowsController.GetWindow<MainMenuWindow>();
            this.pauseWindow = this.windowsController.GetWindow<PauseWindow>();
            this.gameWindow = this.windowsController.GetWindow<GameWindow>();
            this.winLooseWindow = this.windowsController.GetWindow<WinLooseWindow>();

            this.mainMenuWindow.playStarted += OnGameRestarted;
            this.pauseWindow.gameRestared += OnGameRestarted;
            this.winLooseWindow.gameRestared += OnGameRestarted;
            this.gameWindow.bubbleCreated += OnBubbleCreated;
            this.roomController.spiresController.ringsOnSpiresCountChanged += OnRingOnSpireChanged;

            this.gameData = new GameData(this.timeForGame);
            this.gameData.ResetData(Time.time);
            this.gameData.gameEnded += OnGameTimeEnded;
        }
        private void Update()
        {
            if(this.windowsController.currentWindow is GameWindow)
            {
                this.gameData.Update(Time.time);
                this.gameWindow.SetTime(gameData.gameTimeLeft);
            }
        }
        private void OnDestroy()
        {
            this.windowsController.windowChanged -= OnWindowChanged;
            this.mainMenuWindow.playStarted -= OnGameRestarted;
            this.pauseWindow.gameRestared -= OnGameRestarted;
            this.gameWindow.bubbleCreated -= OnBubbleCreated;
            this.roomController.spiresController.ringsOnSpiresCountChanged -= OnRingOnSpireChanged;
            if (this.gameData != null)
                this.gameData.gameEnded -= OnGameTimeEnded;
        }
    }
}