using RingInWater.Utility;
using UnityEngine;

namespace RingInWater.View
{
    public class SpiresController : InitilizableView
    {
        [SerializeField] private SpireView spireTemplate = null;
        [SerializeField]private Transform[] spirePoints = new Transform[0];
        private SpireView[] spiresPrivate;
        public SpireView[] spires
        {
            get => this.spiresPrivate;
        }
        private void CreateSpires()
        {
            this.spiresPrivate = new SpireView[spirePoints.Length];
            for(int i=0;i<spirePoints.Length;i++)
            {
                this.spiresPrivate[i] = InstantiateWithInject(spireTemplate, spirePoints[i]);
            }
        }
        public override void Initilize(RoomController roomController)
        {
            base.Initilize(roomController);

            CreateSpires();
        }
    }
}