using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
    public class RingsController : MonoBehaviour
    {
        [SerializeField] private int ringsCount = 3;
        [SerializeField] private Vector2 randomPositionsRange = Vector2.one;
        [SerializeField] private RingView ringViewTemplate = null;
        [SerializeField] private BubbleSpawner bubbleSpawner = null;
        [SerializeField] private float impactForce = 3f;
        [SerializeField]private Transform leftWave = null;
        [SerializeField]private Transform rightWave = null;
        [SerializeField] private Vector3 waveForceAxisMultiplier = Vector3.one;
        [SerializeField] private float maxWaveVelocity = 4f;

        [Header("Explode")]
        [SerializeField] private float explosionRadius = 5f;
        [SerializeField] private float force = 5f;

        private Transform[] bubbleSpawnPoints
        {
            get => this.bubbleSpawner.startPoints;
        }
        private RingView[] ringViews;

        /// <summary>
        /// Проверить пересечение Объектов.
        /// </summary>
        /// <param name="view1"></param>
        /// <param name="view2"></param>
        /// <returns></returns>
        private bool IsColliderIntersection(RingView view1, RingView view2)
        {
            Transform t1 = view1.transform;
            Transform t2 = view2.transform;

            if (t1 != null && t2 != null)
            {
                return Vector3.Distance(t1.localPosition, t2.localPosition) > 1;
            }

            // Если один из объектов не имеет коллайдера, вернуть false
            return false;
        }
        /// <summary>
        /// Есть ли пересечения с уже созданными объектами.
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        private bool IsIntersectionWithObjects(RingView view)
        {
            foreach(RingView currentView in ringViews)
            {
                if (currentView != null && currentView != view)
                {
                    if (IsColliderIntersection(currentView, view))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        private Vector3 randomPosition
        {
            get => new Vector3
                (
                    Random.Range(0, this.randomPositionsRange.x),
                    Random.Range(3, this.randomPositionsRange.y + this.transform.localPosition.y + 1),
                    0
                );
        }
        private void RaiseObjectHigher(Transform transform)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1, transform.localPosition.z);
        }
        private void CreateRings()
        {
            RingView newView = null;
            this.ringViews= new RingView[this.ringsCount];
            this.ringsBodies = new Rigidbody[ringsCount];
            for (int i=0;i<this.ringsCount; i++)
            {
                int counter = 0;
                newView = Instantiate(this.ringViewTemplate, this.transform.transform);
                newView.transform.localPosition = this.randomPosition;
                newView.gameObject.name = $"RingView{i}";
                while (IsIntersectionWithObjects(newView))
                {
                    RaiseObjectHigher(newView.transform);
                    counter++;
                    if (counter > 10)
                        break;
                }
                this.ringViews[i] = newView;
                this.ringsBodies[i] = newView.ringBody;
                newView.ringBody.maxAngularVelocity = 3f;
            }
        }

        #region Explosion

        private Rigidbody[] ringsBodies;
        private void Explode(Vector3 position)
        {
            for (int i = 0; i < this.ringsBodies.Length; i++)
            {
                Rigidbody rigidbody = ringsBodies[i];
                if (rigidbody != null)
                {
                    rigidbody.AddExplosionForce(this.force, position, this.explosionRadius);
                }
            }
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            for(int i=0;i<this.bubbleSpawnPoints.Length;i++)
            {
                Gizmos.DrawWireSphere(this.bubbleSpawnPoints[i].transform.position, this.explosionRadius);
            }

        }
#endif
        #endregion Explosion

        private void AddForceFromPoint(int number)
        {
            Explode(this.bubbleSpawnPoints[number].transform.position);
        }

        public void AddForceFromPoint1()
        {
            AddForceFromPoint(0);
        }
        public void AddForceFromPoint2()
        {
            AddForceFromPoint(1);
        }


        private void Awake()
        {
            CreateRings();
        }
        private void FixedUpdate()
        {
            float maxWaveVelocity = this.maxWaveVelocity;
            foreach (RingView currentView in this.ringViews)
            {
                if (leftWave.position.x < currentView.xPosition)
                    if (Mathf.Abs(currentView.ringBody.velocity.x) < maxWaveVelocity)
                        currentView.ringBody.AddForce(new Vector3(-maxWaveVelocity, maxWaveVelocity * 0.1f, 0), ForceMode.Force);
                if (rightWave.position.x > currentView.xPosition)
                    if (Mathf.Abs(currentView.ringBody.velocity.x) < maxWaveVelocity)
                        currentView.ringBody.AddForce(new Vector3(maxWaveVelocity, maxWaveVelocity * 0.1f, 0), ForceMode.Force);
            }
        }
    }
}