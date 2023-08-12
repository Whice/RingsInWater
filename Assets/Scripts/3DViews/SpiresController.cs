using System;
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
        /// <summary>
        /// Количество колец на шпилях изменилось.
        /// В аргументе количество колец.
        /// </summary>
        public event Action<int> ringsOnSpiresCountChanged;
        private void OnRingtoSpireAdded()
        {
            int count = 0;
            foreach (SpireView spire in this.spires)
            {
                count += spire.ringsCount;
            }
            ringsOnSpiresCountChanged?.Invoke(count);
        }
        private void CreateSpires()
        {
            this.spiresPositions = new Vector3[spirePoints.Length];
            this.spiresPrivate = new SpireView[spirePoints.Length];
            for (int i = 0; i < spirePoints.Length; i++)
            {
                this.spiresPrivate[i] = InstantiateWithInject(spireTemplate, spirePoints[i]);
                this.spiresPositions[i] = this.spiresPrivate[i].transform.position;
                this.spiresPrivate[i].ringAdded += OnRingtoSpireAdded;
            }
        }

        public void ResetSpires()
        {
            foreach(SpireView spireView in spiresPrivate)
            {
                spireView.ResetSpire();
            }
        }
        public override void Initilize(RoomController roomController)
        {
            base.Initilize(roomController);

            CreateSpires();
        }
    }
}