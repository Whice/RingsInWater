using UnityEngine;
using UnityEngine.UI;

namespace RingInWater.UI
{
    public class CollectionWindow : AbstractWindow
    {
        [SerializeField] private Button toMenuButton = null;
        [SerializeField] private Button ringsButton = null;
        [SerializeField] private Button spiresButton = null;
        [SerializeField] private Button bubbleButton = null;
        protected override void OnCreate()
        {
            this.toMenuButton.onClick.AddListener(() => OpenPreviousWindow());
            //Визуала для рузырей пока нет.
            this.bubbleButton.interactable = false;
        }

        protected override void OnWindowDestroy()
        {
        }
    }
}