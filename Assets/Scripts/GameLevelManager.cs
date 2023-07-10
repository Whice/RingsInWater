using UnityEngine;

namespace Level
{
    /// <summary>
    /// Создать.
    /// </summary>
    class GameInteface
    {

    }

    public class GameLevelManager : MonoBehaviour
    {
        [SerializeField] private GameInteface inteface;
        [SerializeField] private RoomController room;
    }
}