//Copyright: Made by Appfox

using System;
using UnityEngine;

namespace WarpTravelAR.Utils.FPSCounting
{
    [Serializable]
    public class FpsData
    {
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public int MinFPS { get; private set; }
    }
}