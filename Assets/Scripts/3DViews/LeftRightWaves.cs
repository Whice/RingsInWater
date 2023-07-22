using UnityEngine;

namespace RingInWater.View
{
    public class LeftRightWaves : InitilizableView
    {
        [SerializeField] private Transform leftWave = null;
        [SerializeField] private Transform rightWave = null;
        [SerializeField] private float maxWaveVelocity = 4f;
        [SerializeField] private float waveAngleHeight = 10f;

        private RingsController ringsController
        {
            get => this.roomController.ringsController;
        }
        private BubbleSpawner bubbleSpawner
        {
            get => this.roomController.bubbleSpawner;
        }

        private void CheckBordersForMovable(IWaveMovable currentView)
        {
            float maxWaveVelocity = this.maxWaveVelocity;
            if (this.leftWave.position.x - currentView.position.y / this.waveAngleHeight
                < currentView.position.x)
            {
                if (Mathf.Abs(currentView.selfRigidbody.velocity.x) < this.maxWaveVelocity)
                    currentView.selfRigidbody.AddForce(
                        new Vector3(-maxWaveVelocity, maxWaveVelocity * 0.1f, 0), ForceMode.Force);
            }

            if (this.rightWave.position.x + currentView.position.y / this.waveAngleHeight
                > currentView.position.x)
            {
                if (Mathf.Abs(currentView.selfRigidbody.velocity.x) < maxWaveVelocity)
                    currentView.selfRigidbody.AddForce(
                        new Vector3(maxWaveVelocity, maxWaveVelocity * 0.1f, 0), ForceMode.Force);
            }
        }
        private void FixedUpdate()
        {
            if (this.isInittilized)
            {
                //Задействовать "волну", которая будет возврщать объекты ближе к центру,
                //когда они уплывают слишком сильно вправо или влево.
                float maxWaveVelocity = this.maxWaveVelocity;

                foreach (IWaveMovable currentView in this.ringsController.waveMovables)
                {
                    CheckBordersForMovable(currentView);
                }

                foreach (IWaveMovable currentView in this.bubbleSpawner.activeBubbles)
                {
                    CheckBordersForMovable(currentView);
                }
            }
        }
    }
}