using RingInWater.Utility;
using RingInWater.View;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameWindow : MonoBehaviourLogger
    {
        [SerializeField] private Button leftBubbleButton = null;
        [SerializeField] private Button rightBubbleButton = null;
        [SerializeField] private BubbleSpawner spawner = null;

        private void Awake()
        {
            leftBubbleButton.onClick.AddListener(() => spawner.CreateBubbles(0));
            rightBubbleButton.onClick.AddListener(() => spawner.CreateBubbles(1));
        }
    }
}