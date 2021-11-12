using UnityEngine;

[CreateAssetMenu(fileName = "BuyerBase", menuName = "ScriptableObject/BuyerBase")]
public class BuyerBase : ScriptableObject
{
    public enumBuyerType enumBuyerType;
    public string buyerName;
    public GameObject buyerPrefab;
    public float patienceDuration = 10f;
}
