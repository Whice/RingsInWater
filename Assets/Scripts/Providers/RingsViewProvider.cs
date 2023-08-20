using UnityEngine;

namespace RingInWater.View.Providers
{
    [CreateAssetMenu(fileName = nameof(RingsViewProvider), menuName = "Providers/" + nameof(RingsViewProvider))]
    public class RingsViewProvider : AbstractViewProvider<RingView>
    {
    }
}