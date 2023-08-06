using System;
using UnityEngine;

namespace RingInWater.Utils.FPSCounting
{
    [Serializable]
    internal struct FPSColor
    {
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public int MinFPS { get; private set; }
    }
}