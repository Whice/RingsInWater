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
        [SerializeField] private BubbleView bubbleViewTemplate = null;
        [SerializeField] private Transform[] startPointsPrivate = new Transform[0];
        [Header("Bubbles with time")]
        [SerializeField] private float bubbleCreateSecondRate = 1f;
        [SerializeField] private float bubbleWithTimeMaxHeight = 30f;
        [SerializeField] private Transform[] pointForPermanentBubbles = new Transform[0];


        public Transform[] startPoints
        {
            get => this.startPointsPrivate;
        }
        private Stack<BubbleView> unactiveBubbles = new Stack<BubbleView>();
        public List<BubbleView> activeBubbles { get; private set; } = new List<BubbleView>();
        private BubbleView GetBubble()
        {
            if (this.unactiveBubbles.Count == 0)
            {
                BubbleView view = InstantiateWithInject(this.bubbleViewTemplate);
                this.unactiveBubbles.Push(view);
                view.activeChanged += (isActive, bubbleView) =>
                {
                    if (!isActive)
                    {
                        if (!this.unactiveBubbles.Contains(view))
                            this.unactiveBubbles.Push(bubbleView);
                        if (this.activeBubbles.Contains(view))
                            this.activeBubbles.Remove(view);

                    }
                };
            }

            BubbleView resultView = this.unactiveBubbles.Pop();
            this.activeBubbles.Add(resultView);
            resultView.SetActive(true);
            return resultView;
        }
        private System.Random random = new System.Random(0);
        private BubbleView CreateView(Transform parent)
        {
            BubbleView view = GetBubble();
            view.maxBubblesVisibleHeight = this.maxBubblesVisibleHeight;
            view.transform.SetParent(parent);
            float size = this.random.Next(1, 3) / 3f;
            view.SetSize(size);

            return view;
        }
        public void CreateBubbles(int startPointIndex)
        {
            if (startPointsPrivate.Length > startPointIndex)
            {
                int count = this.random.Next(minBubbles, maxBubbles + 1);
                for (int i = 0; i < count; i++)
                {
                    BubbleView view = CreateView(this.startPointsPrivate[startPointIndex]);
                    view.SetRandomPosition();
                }
            }
        }

        private float lastCreateTime = 0;
        private int lastPointForPermanentBubblesIndex = 0;
        private void CreateBubbleWithTime()
        {
            float time = Time.time;
            if (time - this.lastCreateTime > this.bubbleCreateSecondRate)
            {
                this.lastCreateTime = time;
                ++this.lastPointForPermanentBubblesIndex;
                if (this.lastPointForPermanentBubblesIndex == this.pointForPermanentBubbles.Length)
                    this.lastPointForPermanentBubblesIndex = 0;

                Transform parent = this.pointForPermanentBubbles[this.lastPointForPermanentBubblesIndex];

                BubbleView view = CreateView(parent);
                view.transform.localPosition = new Vector3
                    (
                    0,
                    Random.Range(0, this.bubbleWithTimeMaxHeight),
                    0
                    );
                view.SetZAxisDeep();
            }
        }
        private void Update()
        {
            CreateBubbleWithTime();
        }
    }
}