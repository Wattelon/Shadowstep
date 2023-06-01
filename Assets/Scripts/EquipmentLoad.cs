using UnityEngine;

public class EquipmentLoad : MonoBehaviour
{
    [SerializeField] private GameObject[] equipments;
    
    void Start()
    {
        for (int i = 0; i < equipments.Length; i++)
        {
            if (PlayerPrefs.GetInt($"{i}", 0) == 1)
            {
                equipments[i].SetActive(true);
            }
        }
    }

    public void ObtainEquipment(int equipmentID)
    {
        equipments[equipmentID].SetActive(true);
    }
}