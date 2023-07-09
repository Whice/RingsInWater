using UnityEngine;

namespace View
{
    public class RingsController : MonoBehaviour
    {
        [SerializeField] private int ringsCount = 3;
        [SerializeField] private Vector2 randomPositionsRange = Vector2.one;
        [SerializeField] private RingView ringViewTemplate = null;
        [SerializeField] private Transform[] points = new Transform[0];
        [SerializeField] private float impactForce = 3f;

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
                return Vector3.Distance(t1.position, t2.position) > 1;
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
                    Random.Range(0, this.randomPositionsRange.y),
                    0
                );
        }
        private void RaiseObjectHigher(Transform transform)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
        private void CreateRings()
        {
            RingView newView = null;
            this.ringViews= new RingView[this.ringsCount];
            for(int i=0;i<this.ringsCount; i++)
            {
                int counter = 0;
                newView = Instantiate(this.ringViewTemplate, this.transform.transform);
                newView.transform.position = this.randomPosition;
                newView.gameObject.name = $"RingView{i}";
                while (IsIntersectionWithObjects(newView))
                    {
                    RaiseObjectHigher(newView.transform);
                    counter++;
                    if (counter > 10)
                        break;
                }
                this.ringViews[i] = newView;
            }
        }

        private void Awake()
        {
            CreateRings();
        }

        private void AddForceFromPoint(int number)
        {
            foreach (RingView currentView in this.ringViews)
            {
                currentView.AddForceToBody(this.points[number].transform.position, this.impactForce);
            }
        }
        
        public void AddForceFromPoint1()
        {
            AddForceFromPoint(0);
        }
        public void AddForceFromPoint2()
        {
            AddForceFromPoint(1);
        }

    }
}