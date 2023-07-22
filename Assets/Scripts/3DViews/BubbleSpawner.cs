using RingInWater.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace RingInWater.View
{
    public class BubbleSpawner : InitilizableView
    {
        [SerializeField] private int minBubbles = 10;
        [SerializeField] private int maxBubbles = 30;
        [SerializeField] private float maxBubblesVisibleHeight = 30f;
        [SerializeField] private Transform[] startPointsPrivate = new Transform[0];
        [SerializeField] private BubbleView bubbleViewTemplate = null;

        public Transform[] startPoints
        {
            get => this.startPointsPrivate;
        }
        private Stack<BubbleView> unactiveBubbles = new Stack<BubbleView>();
        private BubbleView GetBubble()
        {
            if (this.unactiveBubbles.Count == 0)
            {
                BubbleView view = InstantiateWithInject(this.bubbleViewTemplate);
                this.unactiveBubbles.Push(view);
                view.activeChanged += (isActive, bubbleView) =>
                {
                    if (!isActive && !this.unactiveBubbles.Contains(view))
                    {
                        this.unactiveBubbles.Push(bubbleView);
                    }
                };
            }

            BubbleView resultView = this.unactiveBubbles.Pop();
            resultView.SetActive(true);
            return resultView;
        }
        private System.Random random = new System.Random(0);
        public void CreateBubbles(int startPointIndex)
        {
            if (startPointsPrivate.Length > startPointIndex)
            {
                int count = this.random.Next(minBubbles, maxBubbles+1);
                for (int i = 0; i < count; i++)
                {
                    BubbleView view = GetBubble();
                    view.maxBubblesVisibleHeight = this.maxBubblesVisibleHeight;
                    view.transform.SetParent(this.startPointsPrivate[startPointIndex]);
                    float size = this.random.Next(1, 3) / 3f;
                    view.SetSize(size);
                    view.SetRandomPosition();
                }
            }
        }
    }
}