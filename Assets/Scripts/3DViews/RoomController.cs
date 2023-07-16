using RingInWater.Utility;
using UnityEngine;

namespace RingInWater.View
{
    public class RoomController: MonoBehaviourLogger
    {
        [SerializeField] private RingsController ringsControllerPrivate = null;
        [SerializeField] private BubbleSpawner bubbleSpawnerPrivate = null;
        [SerializeField] private SpiresController spiresControllerPrivate = null;

        public RingsController ringsController
        {
            get => this.ringsControllerPrivate;
        }
        public BubbleSpawner bubbleSpawner
        {
            get => this.bubbleSpawnerPrivate;
        }
        public SpiresController spiresController
        {
            get => this.spiresControllerPrivate;
        }
        private void Awake()
        {
            this.ringsController.Initilize(this);
            this.bubbleSpawner.Initilize(this);
            this.spiresController.Initilize(this);
        }
    }
}