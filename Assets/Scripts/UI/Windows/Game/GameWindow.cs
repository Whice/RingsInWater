using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RingInWater.UI
{
    /// <summary>
    /// HUD, который используется прямо в игре.
    /// </summary>
    public class GameWindow : InGameWindow
    {
        [SerializeField] private float leftTimeAfterClick = 0.2f;
        [SerializeField] private Button leftBubbleButton = null;
        [SerializeField] private Button rightBubbleButton = null;
        [SerializeField] private Button pauseButton = null;
        [SerializeField] private TextMeshProUGUI timeTextFiled = null;

        private class ButtonWithDelay
        {
            private float leftTimeAfterClick;
            private Button button;
            public event Action clicked;

            private float lastTimeClick = 0;
            private void OnClicked()
            {
                if (Time.time - this.lastTimeClick > this.leftTimeAfterClick)
                {
                    this.lastTimeClick = Time.time;
                    this.clicked?.Invoke();
                }
            }
            public void RemoveAllClickedListeners()
            {
                this.clicked = null;
            }
            public ButtonWithDelay(Button button, float leftTimeAfterClick)
            {
                this.leftTimeAfterClick = leftTimeAfterClick;
                this.button = button;
                button.onClick.AddListener(OnClicked);
            }
        }

        private ButtonWithDelay leftBubbleButtonWithDelay;
        private ButtonWithDelay rightBubbleButtonWithDelay;
        /// <summary>
        /// Событие, по которому должны быть созданы пузыри и взрыв для колец.
        /// </summary>
        public event Action<int> bubbleCreated;
        private void CreateBubbles(int pointIndex)
        {
            this.bubbleCreated?.Invoke(pointIndex);
        }
        public void SetTime(int time)
        {
            this.timeTextFiled.text = $"{time}";
        }
        protected override void OnCreate()
        {
            this.leftBubbleButtonWithDelay = new ButtonWithDelay(this.leftBubbleButton, this.leftTimeAfterClick);
            this.leftBubbleButtonWithDelay.clicked += () => CreateBubbles(0);

            this.rightBubbleButtonWithDelay = new ButtonWithDelay(this.rightBubbleButton, this.leftTimeAfterClick);
            this.rightBubbleButtonWithDelay.clicked += () => CreateBubbles(1);

            this.pauseButton.onClick.AddListener(() => OpenWindow(typeof(PauseWindow)));
        }

        protected override void OnWindowDestroy()
        {
            this.leftBubbleButtonWithDelay.RemoveAllClickedListeners();
            this.rightBubbleButtonWithDelay.RemoveAllClickedListeners();
            this.pauseButton.onClick.RemoveAllListeners();
        }
    }
}