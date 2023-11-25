using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [Header("Optional")]
    [SerializeField] private TMP_Text moneyText;

    void Start()
    {
        moneyText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        moneyText.text = $"${PlayerStats.Money}";
    }
}
