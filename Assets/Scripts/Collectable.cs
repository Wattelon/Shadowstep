using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int price;

    public int Price => price;
}
