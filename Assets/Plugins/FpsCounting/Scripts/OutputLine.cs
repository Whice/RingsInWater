using TMPro;
using UnityEngine;

namespace RingInWater.Utils.FPSCounting
{
    public class OutputLine : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI typeName;
        [SerializeField] private TextMeshProUGUI value;

        public void SetValueAndColor(string value, Color color)
        {
            this.value.text = value;
            this.value.color = color;
            typeName.color = color;
        }
    }
}