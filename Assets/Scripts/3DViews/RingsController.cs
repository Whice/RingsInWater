using System.Collections.Generic;
using UnityEngine;

namespace RingInWater.View
{
    public class RingsController : InitilizableView
    {
        [SerializeField] private int ringsCount = 3;
        [SerializeField] private Vector2 randomPositionsRange = Vector2.one;
        [SerializeField] private RingView ringViewTemplate = null;
        [SerializeField] private float rangeOfLocation = 10f;

        [Header("Explode")]
        [SerializeField] private float explosionRadius = 5f;
        [SerializeField] private float force = 5f;

        private BubbleSpawner bubbleSpawner
        {
            get => this.roomController.bubbleSpawner;
        }
        private SpiresController spiresController
        {
            get => this.roomController.spiresController;
        }
        /// <summary>
        /// Точки появления пузырей, они же - места, откуда идет поток толкающей воды.
        /// </summary>
        private Transform[] bubbleSpawnPoints
        {
            get => this.bubbleSpawner.startPoints;
        }
        /// <summary>
        /// Представления участвующие в игре.
        /// </summary>
        private RingView[] ringViews;
        /// <summary>
        /// Представления для движения волной.
        /// </summary>
        public IWaveMovable[] waveMovables { get; private set; }

        #region Создание колец.

        /// <summary>
        /// Созданные представления.
        /// </summary>
        private Stack<RingView> createdRingViews = new Stack<RingView>();
        /// <summary>
        /// Создать и запомнить представления колец.
        /// </summary>
        /// <returns></returns>
        private RingView GetNewRingView()
        {
            RingView newView = null;
            if (this.createdRingViews.Count == 0)
            {
                newView = InstantiateWithInject(this.ringViewTemplate, this.transform);
                newView.SetActive(false);
                this.createdRingViews.Push(newView);
            }

            newView = this.createdRingViews.Pop();
            newView.ResetView();
            return newView;
        }
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

            //Установить относительный размер, по которому будет учитываться разница растояния между кольцами
            float ralatedSize = t1.localScale.x * this.rangeOfLocation;
            if (t1 != null && t2 != null)
            {
                float distanceX = Mathf.Abs(t1.localPosition.x - t2.localPosition.x);
                float distanceY = Mathf.Abs(t1.localPosition.y - t2.localPosition.y);
                return distanceX < ralatedSize && distanceY < ralatedSize;
            }

            // Если один из объектов не имеет коллайдера, вернуть false
            return false;
        }
        /// <summary>
        /// Есть ли пересечения с уже созданными объектами.
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        private bool IsIntersectionWithRings(RingView view)
        {
            foreach (RingView currentView in ringViews)
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
        /// <summary>
        /// Получить случайное положение в заданных выше координатах.
        /// </summary>
        private Vector3 randomPosition
        {
            get => new Vector3
                (
                    Random.Range(-this.randomPositionsRange.x, this.randomPositionsRange.x),
                    Random.Range(3, this.randomPositionsRange.y + this.transform.localPosition.y + 1),
                    0
                );
        }
        /// <summary>
        /// Приподнять объект на 1 единицу.
        /// </summary>
        /// <param name="transform"></param>
        private void RaiseObjectHigher(Transform transform)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1, transform.localPosition.z);
        }
        private bool IsIntersectionWithSpires(Transform newViewTransform)
        {
            float xPos = newViewTransform.position.x;
            float scaleX = newViewTransform.localScale.x;
            foreach (Vector3 spirePosition in this.spiresController.spiresPositions)
            {
                if (Mathf.Abs(spirePosition.x - xPos) <= scaleX)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Создать все кольцо и заполнить всю инфорамцию для/о них.
        /// </summary>
        private void CreateRings()
        {
            RingView newView = null;
            this.ringViews = new RingView[this.ringsCount];
            this.waveMovables = new IWaveMovable[this.ringsCount];
            this.ringsBodies = new Rigidbody[ringsCount];
            for (int i = 0; i < this.ringsCount; i++)
            {
                int counter = 0;
                newView = GetNewRingView();
                newView.transform.localPosition = this.randomPosition;
                newView.gameObject.name = $"RingView{i}";
                while (IsIntersectionWithRings(newView))
                {
                    RaiseObjectHigher(newView.transform);
                    counter++;
                    if (counter > 100)
                        break;
                }
                while (IsIntersectionWithSpires(newView.transform))
                {
                    newView.transform.position = new Vector3
                        (
                        newView.transform.position.x + 0.5f,
                        newView.transform.position.y,
                        newView.transform.position.z
                        );
                }
                this.ringViews[i] = newView;
                this.ringsBodies[i] = newView.selfRigidbody;
                this.waveMovables[i] = newView;
                newView.SetActive(true);
            }
        }

        #endregion Создание колец.

        #region Explosion

        private Rigidbody[] ringsBodies;
        /// <summary>
        /// Применить взыв к кольцам, чтобы они подлетели вверх.
        /// </summary>
        /// <param name="position"></param>
        private void Explode(Vector3 position)
        {
            for (int i = 0; i < this.ringsBodies.Length; i++)
            {
                Rigidbody rigidbody = ringsBodies[i];
                if (rigidbody != null)
                {
                    rigidbody.AddExplosionForce(this.force, position, this.explosionRadius);
                    float delta = rigidbody.transform.position.x - position.x;
                    float direction = delta / Mathf.Abs(delta);
                    delta = (10 - Mathf.Abs(delta)) * direction * 50;
                    if (delta > 0)
                    {
                        rigidbody.AddForce(delta, 0, 0, ForceMode.Force);
                        this.ringViews[i].OnForceAdded();
                    }
                }
            }
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            //Нарисовать области воздействия взрыва.
            for (int i = 0; i < this.bubbleSpawnPoints.Length; i++)
            {
                Gizmos.DrawWireSphere(this.bubbleSpawnPoints[i].transform.position, this.explosionRadius);
            }

        }
#endif
        #endregion Explosion

        public void AddForceFromPoint(int number)
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

        public void ResetRings()
        {
            foreach (RingView ringView in this.ringViews)
            {
                ringView.ResetView();
                ringView.SetActive(false);
                this.createdRingViews.Push(ringView);
            }
            CreateRings();
        }
        public override void Initilize(RoomController roomController)
        {
            base.Initilize(roomController);
            CreateRings();
        }
    }
}