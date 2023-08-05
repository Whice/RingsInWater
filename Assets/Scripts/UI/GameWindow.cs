using RingInWater.Utility;
using RingInWater.View;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RingInWater.UI
{
    public class GameWindow : MonoBehaviourLogger
    {
        [SerializeField] private float leftTimeAfterClick = 0.2f;
        [SerializeField] private Button leftBubbleButton = null;
        [SerializeField] private Button rightBubbleButton = null;
        [SerializeField] private BubbleSpawner spawner = null;

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


            public ButtonWithDelay(Button button, float leftTimeAfterClick)
            {
                this.leftTimeAfterClick = leftTimeAfterClick;
                this.button = button;
                button.onClick.AddListener(OnClicked);
            }
        }

        private ButtonWithDelay leftBubbleButtonWithDelay;
        private ButtonWithDelay rightBubbleButtonWithDelay;
        private void CreateBubbles(int pointIndex)
        {
            this.spawner.CreateBubbles(pointIndex);
        }
        private void Awake()
        {
            this.leftBubbleButtonWithDelay = new ButtonWithDelay(this.leftBubbleButton, this.leftTimeAfterClick);
            this.leftBubbleButtonWithDelay.clicked += () => CreateBubbles(0);

            this.rightBubbleButtonWithDelay = new ButtonWithDelay(this.rightBubbleButton, this.leftTimeAfterClick);
            this.rightBubbleButtonWithDelay.clicked += () => CreateBubbles(1);
        }
    }
}