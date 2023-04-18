using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinUI : MonoBehaviour
{
    public int StartCoinQuantity;
    public Text CoinQuantity;

    public static int CurrentCoinQuantity;

    void Start()
    {
        CurrentCoinQuantity = StartCoinQuantity;
    }

    // Update is called once per frame
    void Update()
    {
        CoinQuantity.text = CurrentCoinQuantity.ToString();
    }
}
