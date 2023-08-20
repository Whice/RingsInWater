using RingInWater.Utility;

namespace RingInWater.View
{
    /// <summary>
    /// Представление, у которого есть ID.
    /// </summary>
    public abstract class ViewWithId : MonoBehaviourLogger, IViewWithId
    {
        public abstract int idInt { get; }
    }
}