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
    }
}