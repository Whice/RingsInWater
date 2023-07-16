//Copyright: Made by Appfox

using System;
using UnityEngine;

namespace WarpTravelAR.Utils.FPSCounting
{
    [Serializable]
    internal struct FPSColor
    {
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public int MinFPS { get; private set; }
    }
}