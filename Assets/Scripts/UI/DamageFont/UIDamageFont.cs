using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDamageFont : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] float moveSpeed;
    [SerializeField] float fadeDuration;
    private Transform targetTransform;

    public void SetDamageFont(int p_damageAmount, Transform target)
    {
        damageText.text = p_damageAmount.ToString();
        targetTransform = target;
        StartCoroutine(ShowDamageFont());
    }

    IEnumerator ShowDamageFont()
    {
        Color startColor = damageText.color;
        damageText.color = new Color(startColor.r, startColor.g, startColor.b, 1f);

        Vector3 startPosition = targetTransform.position + new Vector3(0, 0.5f, 0);
        Vector3 endPosition = startPosition + new Vector3(0, 1.25f, 0);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            damageText.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(1f, 0f, t));
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        transform.position = endPosition;
        damageText.color = new Color(startColor.r, startColor.g, startColor.b, 0f);

        ObjectPoolManager.Instance.Destroy("DamageFont", gameObject);
    }

}
