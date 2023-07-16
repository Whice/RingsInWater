//Copyright: Made by Appfox

using System.Collections.Generic;
using UnityEngine;

namespace WarpTravelAR.Utils.FPSCounting
{
    [CreateAssetMenu(fileName = "FpsConfigs", menuName = "ScriptableObjects/Fps/Fps Configs")]
    public class FpsConfigs : ScriptableObject
    {
        [SerializeField] private List<FpsData> _fpsData;

        public IReadOnlyList<FpsData> FPSData => _fpsData;
    }
}