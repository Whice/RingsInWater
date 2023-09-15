using System;

namespace Model
{
    /// <summary>
    /// Модельная часть коллекции для обмена информацией между представлениями.
    /// </summary>
    public class CollectionModel
    {
        /// <summary>
        /// ID текущей сущности выбранной в колекции для отображения.
        /// </summary>
        public int currentEnityId { get; private set; }
        /// <summary>
        /// Вид текущей сущности выбранной в колекции для отображения.
        /// </summary>
        public CollectionEntityType currentCollectionEntityType { get; private set; }

        /// <summary>
        /// Выбор элемента колекции изменился.
        /// В аргументах тип сущности и ее индетификатор.
        /// </summary>
        public event Action<CollectionEntityType, int> collectionEntityChooseChanged;
        private void OnCollectionEntityChooseChanged()
        {
            this.collectionEntityChooseChanged?.Invoke(this.currentCollectionEntityType, this.currentEnityId);
        }
        public void SetEntityID(int entityId)
        {
            this.currentEnityId = entityId;
            OnCollectionEntityChooseChanged();
        }
        public void SetEntityType(CollectionEntityType collectionEntityType)
        {
            this.currentCollectionEntityType = collectionEntityType;
            OnCollectionEntityChooseChanged();
        }

        public void SetDefaultValues(int defaultEntityID, CollectionEntityType defaultCollectionEntityType)
        {
            this.currentEnityId = defaultEntityID;
            this.currentCollectionEntityType = defaultCollectionEntityType;
        }
    }
}