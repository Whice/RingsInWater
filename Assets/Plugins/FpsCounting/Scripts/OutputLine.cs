//Copyright: Made by Appfox

using TMPro;
using UnityEngine;

namespace WarpTravelAR.Utils.FPSCounting
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