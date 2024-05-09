using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextPopup : MonoBehaviour
{
    [SerializeField]
    private Transform pfDamagePopup;
    [SerializeField]
    private Vector3 position;

    private void CreatePopUp(Vector3 pos)
    {
        Transform damagePopupTransform = Instantiate(pfDamagePopup, pos, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        // damagePopup.Setup(300);
    }
}
