using Model;
using RingInWater.Utility;
using RingInWater.View.Providers;
using UnityEngine;

namespace RingInWater.View
{
    public class AllViewsProvider : MonoBehaviourLogger
    {
        [SerializeField] private RingsViewProvider ringsViewProvider = null;
        [SerializeField] private SpiresViewProvider spiresViewProvider = null;

        public RingView GetRingView(RingViewId ringViewId)
        {
            return this.ringsViewProvider.GetViewByID((int)ringViewId);
        }
        public SpireView GetSpireView(SpiresViewId spiresViewId)
        {
            return this.spiresViewProvider.GetViewByID((int)spiresViewId);
        }

        private void CheckByUnknown()
        {
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
        }
        private void Awake()
        {
            CheckByUnknown();
        }
    }
}