using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class Coin: MonoBehaviour
{
    [SerializeField] float _coinValue = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoinManager.AddCoin(_coinValue);
        gameObject.SetActive(false);
    }
}