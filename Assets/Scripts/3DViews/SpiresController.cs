using UnityEngine;

namespace RingInWater.View
{
    public class SpiresController : InitilizableView
    {
        [SerializeField] private SpireView spireTemplate = null;
        [SerializeField] private Transform[] spirePoints = new Transform[0];
        private SpireView[] spiresPrivate;
        public SpireView[] spires
        {
            get => this.spiresPrivate;
        }
        public Vector3[] spiresPositions { get; private set; }
        private void CreateSpires()
        {
            this.spiresPositions = new Vector3[spirePoints.Length];
            this.spiresPrivate = new SpireView[spirePoints.Length];
            for (int i = 0; i < spirePoints.Length; i++)
            {
                this.spiresPrivate[i] = InstantiateWithInject(spireTemplate, spirePoints[i]);
                this.spiresPositions[i] = this.spiresPrivate[i].transform.position;
            }
        }
        public override void Initilize(RoomController roomController)
        {
            base.Initilize(roomController);

            CreateSpires();
        }
    }
}