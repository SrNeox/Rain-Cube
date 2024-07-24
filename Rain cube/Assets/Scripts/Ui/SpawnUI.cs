using TMPro;
using UnityEngine;

public class SpawnUI : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI TotalCountText;
    [SerializeField] private TextMeshProUGUI ActiveCountText;

    protected void UpdateUI(int activeObject, int totalObject)
    {
        TotalCountText.text = $"Active: {activeObject}";
        ActiveCountText.text = $"Total: {totalObject}";
    }
}
