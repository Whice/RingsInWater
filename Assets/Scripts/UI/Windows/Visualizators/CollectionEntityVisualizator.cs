using RingInWater.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using Zenject;

namespace RingInWater.UI
{
    public abstract class CollectionEntityVisualizator : MonoBehaviourLogger
    {
        [SerializeField] protected TextMeshProUGUI nameTextField = null;
        public abstract void Initialize(int id);
    }
}