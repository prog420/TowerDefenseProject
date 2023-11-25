using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    [Header("Optional")]
    [SerializeField] private TMP_Text livesText;

    void Start()
    {
        livesText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        livesText.text = $"Lives: {PlayerStats.Lives}";
    }
}
