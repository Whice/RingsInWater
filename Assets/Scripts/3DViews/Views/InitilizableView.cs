using RingInWater.Utility;

namespace RingInWater.View
{
    public class InitilizableView : MonoBehaviourLogger
    {
        public bool isInittilized { get; private set; } = false;
        protected RoomController roomController;
        /// <summary>
        /// Инициализация представления при создании скрипта <see cref="RoomController"/>
        /// </summary>
        /// <param name="roomController"></param>
        public virtual void Initilize(RoomController roomController)
        {
            this.roomController = roomController;
            this.isInittilized = true;
        }
    }
}