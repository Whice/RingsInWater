using RingInWater.View;
using UnityEngine;

namespace Model
{
    public abstract class CollectionEntity
    {
        [SerializeField] private Sprite iconPrivate = null;
        [SerializeField] private Color colorPrivate = Color.white;
        [SerializeField] private string namePrivate = "";
        [SerializeField] private ViewWithId viewWithId = null;

        /// <summary>
        /// Картинка для обозначения внешнего вида.
        /// </summary>
        public Sprite icon
        {
            get => this.iconPrivate;
        }
        /// <summary>
        /// Цвет для картинки или если ее нет.
        /// </summary>
        public Color color
        {
            get => this.colorPrivate;
        }
        /// <summary>
        /// Имя (ключ для локализации) отображаемое для внешнего вида.
        /// </summary>
        public string name
        {
            get => this.namePrivate;
        }
        /// <summary>
        /// ID для внешнего вида.
        /// </summary>
        public abstract int id { get; }

        public void CheckValid()
        {
            if (this.viewWithId == null)
            {
                Debug.LogError($"{nameof(this.viewWithId)} can't be null!");
            }
            else if (this.id != this.viewWithId.idInt)
            {
                Debug.LogError($"id and {nameof(this.viewWithId.idInt)} is not equal!");
            }
        }
    }
}