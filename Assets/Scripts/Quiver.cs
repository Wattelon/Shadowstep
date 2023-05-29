using UnityEngine;

public class Quiver : MonoBehaviour
{
    private void OnEnable()
    {
        var children = transform.childCount;
        for (var i = 0; i < children; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        var children = transform.childCount;
        for (var i = 0; i < children; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
