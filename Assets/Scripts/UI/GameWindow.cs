using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWindow : MonoBehaviour
{
    [SerializeField] private Button leftBubbleButton = null;
    [SerializeField] private Button rightBubbleButton = null;
    [SerializeField] private BubbleSpawner spawner = null;

    private void Awake()
    {
        leftBubbleButton.onClick.AddListener(() => spawner.CreateBubbles(0));
        rightBubbleButton.onClick.AddListener(() => spawner.CreateBubbles(1));
    }
}
