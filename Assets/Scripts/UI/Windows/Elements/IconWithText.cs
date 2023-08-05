using UnityEngine;
using UnityEngine.UI;

namespace RingInWater.UI
{
    public class IconWithText : MonoBehaviour
    {
        [SerializeField] private Image icon = null;
        [SerializeField] private LocalizedText _localizedText = null;

        public LocalizedText localizedText
        {
            get => _localizedText;
        }
        public void SetIcoon(Image icon)
        {
            this.icon = icon;
        }
    }
}