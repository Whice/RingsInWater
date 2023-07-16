using RingInWater.Utility;
using UnityEngine;

namespace RingInWater.View
{
    /// <summary>
    /// Создать.
    /// </summary>
    class GameInteface
    {

    }

    public class GameLevelManager : MonoBehaviourLogger
    {
        [SerializeField] private GameInteface inteface;
        [SerializeField] private RoomController room;
    }
}