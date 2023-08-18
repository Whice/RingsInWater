namespace RingInWater.View
{
    /// <summary>
    /// Представление, у которого есть ID.
    /// </summary>
    public interface IViewWithId
    {
        /// <summary>
        /// ID предсталения переведенное в число.
        /// Изначально может быть любым перечислением или числом.
        /// </summary>
        int idInt { get; }
    }
}