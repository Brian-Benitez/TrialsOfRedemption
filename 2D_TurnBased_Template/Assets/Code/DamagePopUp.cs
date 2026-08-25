using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    public static DamagePopUp Create(Vector3 position, float damAmount)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.Instance.DamagePopUp, position, Quaternion.identity);
        DamagePopUp damagePopUp = damagePopupTransform.GetComponent<DamagePopUp>();
        damagePopUp.Setup(damAmount);

        return damagePopUp;
    }

    private static int sortingOrder;
    private const float DISAPPER_TIMER_MAX = 1F;
    private TextMeshPro DamagePopUpText;
    private float disappearTimer;
    private Color TextColor;
    private Vector3 moveVector;

    private void Awake()
    {
        DamagePopUpText = GetComponent<TextMeshPro>();
    }

    public void Setup(float damageAmount)
    {
        DamagePopUpText.SetText(damageAmount.ToString());   
        TextColor = DamagePopUpText.color;
        disappearTimer = DISAPPER_TIMER_MAX;
        moveVector = new Vector3(0.5f, 0.5f) * 20f;
        sortingOrder++;
        DamagePopUpText.sortingOrder = sortingOrder;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if(disappearTimer > DISAPPER_TIMER_MAX * 0.5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            float disappearingSpeed = 3f;
            TextColor.a -= disappearingSpeed * Time.deltaTime;
            DamagePopUpText.color = TextColor;
            if(TextColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
