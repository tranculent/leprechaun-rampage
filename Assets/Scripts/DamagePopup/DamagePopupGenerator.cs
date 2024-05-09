using TMPro;
using UnityEngine;

public class DamagePopupGenerator : MonoBehaviour
{
    public static DamagePopupGenerator current;
    public GameObject prefab;

    private void Awake()
    {
        current = this;
    }

    private void Update()
    {
        // to instantiate: CreatePopUp(enemy.transform.position, damageAmount.ToString(), Color.orange);
    }

    public void CreatePopUp(Vector3 position, string text, Color color)
    {
        var popup = Instantiate(prefab, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;
        temp.faceColor = color;

        Destroy(popup, 1f);
    }
}
