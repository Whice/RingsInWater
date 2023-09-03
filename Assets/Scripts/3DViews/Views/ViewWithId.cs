using RingInWater.Utility;

namespace RingInWater.View
{
    /// <summary>
    /// Представление, у которого есть ID.
    /// </summary>
    public abstract class ViewWithId : MonoBehaviourLogger, IViewWithId
    {
        public abstract int idInt { get; }
        /// <summary>
        /// Выполнить при создании объекта.
        /// Всегда вызывать родительский метод при расширении.
        /// </summary>
        protected virtual void OnCreate()
        {
            //Считается, что 0 всегда будет не установленым id.
            if (this.idInt == 0)
            {
                LogError($"{this.name} have unknown id!");
            }
        }

        private void Awake()
        {
            OnCreate();
        }
    }
}