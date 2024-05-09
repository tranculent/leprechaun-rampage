using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private bool exists = false;
    private float disappearTimer;
    private Color textColor;

    private void Start()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>();
        Debug.Log(textMesh);
    }

    private void Update()
    {
        if (exists)
        {
            float moveYSpeed = 3f;
            transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
            disappearTimer -= Time.deltaTime;

            if (disappearTimer < 0)
            {
                // Start disappearing
                float disappearSpeed = 3f;
                textColor.a -= disappearSpeed * Time.deltaTime;
                textMesh.color = textColor;

                if (textColor.a < 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void DisplayDamage(int damage)
    {
        Debug.Log(textColor);
        Debug.Log(textMesh);
        disappearTimer = 1f;
        textColor = textMesh.color;
        textMesh.text = damage.ToString();
        exists = true;
    }
}
