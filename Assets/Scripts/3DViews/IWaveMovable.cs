﻿using UnityEngine;

namespace RingInWater.View
{
    public interface IWaveMovable
    {
        public Vector3 position { get; }
        public Rigidbody selfRigidbody { get; }
    }
}