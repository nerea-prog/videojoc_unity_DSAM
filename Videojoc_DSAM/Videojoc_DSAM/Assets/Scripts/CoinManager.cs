using UnityEngine;
using System;
[DefaultExecutionOrder(-100)]

public class CoinManager : MonoBehaviour
{   
    public static CoinManager Instance { get{ return _instance;}}
    public static Action OnAddPoints;
    private static CoinManager _instance;

    [SerializeField] private float _amount;

    public float Amount{ get {return _instance._amount;}}

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public static void AddCoin(float amount)
    {
        if(_instance != null)
        {
            Instance.AddCoinInternal(amount);
            OnAddPoints?.Invoke();
        }
        else
        {
            Debug.Log("Falta CoinManager en la scene");
        }
    }
    private void AddCoinInternal(float amount)
    {
        _amount += amount;
    }
}
