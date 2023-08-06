using RingInWater.UI;
using RingInWater.Utility;
using RingInWater.View;
using UnityEngine;

namespace RingInWater
{
    public class SceneController : MonoBehaviourLogger
    {
        [SerializeField] private WindowsController windowsController = null;
        [SerializeField] private RoomController roomController = null;

        private void OnGameRestarted()
        {
            roomController.ResetGame();
        }
        private void OnActive3DWorld(bool isActive)
        {
            Time.timeScale = isActive ? 1.0f : 0.0f;
        }
        private void OnWindowChanged()
        {
            AbstractWindow current = this.windowsController.currentWindow;
            OnActive3DWorld(current is GameWindow);
        }

        private MainMenuWindow mainMenuWindow;
        private PauseWindow pauseWindow;

        private void Awake()
        {
            this.windowsController.windowChanged += OnWindowChanged;
            this.mainMenuWindow = this.windowsController.GetWindow<MainMenuWindow>();
            this.pauseWindow = this.windowsController.GetWindow<PauseWindow>();

            this.mainMenuWindow.playStarted += OnGameRestarted;
            this.pauseWindow.gameRestared += OnGameRestarted;
        }
        private void OnDestroy()
        {
            this.windowsController.windowChanged -= OnWindowChanged;
            this.mainMenuWindow.playStarted -= OnGameRestarted;
            this.pauseWindow.gameRestared -= OnGameRestarted;
        }
    }
}