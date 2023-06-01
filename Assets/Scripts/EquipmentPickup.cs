using UnityEngine;

public class EquipmentPickup : MonoBehaviour
{
    [SerializeField] private int equipmentID;
    [SerializeField] private EquipmentLoad equipmentLoad;

    private void Start()
    {
        if (PlayerPrefs.GetInt($"{equipmentID}", 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    public void OnPickupGrab()
    {
        Progression.SaveEquipment(equipmentID);
        equipmentLoad.ObtainEquipment(equipmentID);
        Destroy(gameObject);
    }
}