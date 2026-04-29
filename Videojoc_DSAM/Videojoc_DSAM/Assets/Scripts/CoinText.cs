using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinText : MonoBehaviour
{
    TextMeshProUGUI _text;
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        ChangeText();
    }
    private void OnEnable()
    {
        CoinManager.OnAddPoints += ChangeText;
    }
    private void OnDisable()
    {
        CoinManager.OnAddPoints -= ChangeText;
    }

    private void ChangeText()
    {
        _text.text = "Coins: " + CoinManager.Instance.Amount;
    }
}
