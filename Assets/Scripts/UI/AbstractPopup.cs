using RingInWater.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace RingInWater.UI
{
    public abstract class AbstractPopup : MonoBehaviourLogger
    {
        [SerializeField] private Button[] closeButtons = new Button[1];


    }
}