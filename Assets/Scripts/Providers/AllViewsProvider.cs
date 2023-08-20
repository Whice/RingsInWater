using Model;
using RingInWater.Utility;
using RingInWater.View.Providers;
using UnityEngine;

namespace RingInWater.View
{
    /// <summary>
    /// Поставщик всех провайдеров или их данных.
    /// </summary>
    public class AllViewsProvider : MonoBehaviourLogger
    {
        [SerializeField] private RingsViewProvider ringsViewProvider = null;
        [SerializeField] private SpiresViewProvider spiresViewProvider = null;
        [SerializeField] private CollectionEntitiesProvider collectionEntitiesProvider = null;

        public RingView GetRingView(RingViewId ringViewId)
        {
            return this.ringsViewProvider.GetViewByID((int)ringViewId);
        }
        public SpireView GetSpireView(SpiresViewId spiresViewId)
        {
            return this.spiresViewProvider.GetViewByID((int)spiresViewId);
        }
        public CollectionEntitiesProvider GetCollectionEntitiesProvider()
        {
            return this.collectionEntitiesProvider;
        }

        /// <summary>
        /// Проверить ссылку на наличие, ругаться, если не задана.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="chekable"></param>
        private void NullCheck(string name, Object chekable)
        {
            if (chekable== null)
            {
                LogError($"{name} in all views provider is null!");
            }
        }
        private void CheckByUnknown()
        {
            NullCheck(nameof(this.ringsViewProvider), this.ringsViewProvider);
            NullCheck(nameof(this.spiresViewProvider), this.spiresViewProvider);
            NullCheck(nameof(this.collectionEntitiesProvider), this.collectionEntitiesProvider);

            foreach (RingView view in this.ringsViewProvider.viewsArray)
            {
                if (view.idInt == 0)
                {
                    LogError($"Ring view with name: {view.name} isn't set id!");
                }
            }
            foreach (SpireView view in this.spiresViewProvider.viewsArray)
            {
                if (view.idInt == 0)
                {
                    LogError($"Spire view with name: {view.name} isn't set id!");
                }
            }
            this.collectionEntitiesProvider.CheckByUnknown();
        }
        private void Awake()
        {
            CheckByUnknown();
        }
    }
}