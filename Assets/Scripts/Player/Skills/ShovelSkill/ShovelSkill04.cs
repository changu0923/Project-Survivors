using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShovelSkill04 : Skill
{
    [Header("스킬 설정")]
    [SerializeField] int weaponDamage;
    [SerializeField] int weaponRPM;
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] float scaleX;
    [SerializeField] float scaleY;

    private Player player;

    private Coroutine skillCoroutine;
    private bool isActive;
    private void Awake()
    {
        transform.localScale = new Vector3(scaleX, scaleY, 1f);
        player = GameManager.Instance.Player;
    }

    public override void Use()
    {
        if (!isActive)
        {
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.zero;
            isActive = true;
            skillCoroutine = StartCoroutine(ShovelSpinCoroutine());

        }
    }

    IEnumerator ShovelSpinCoroutine()
    {
        float rotateSpeed = weaponRPM / 60f;
        float rotateDegree = rotateSpeed * 360f;

        while (isActive == true)
        {
            float rotateValue = rotateDegree * Time.deltaTime;
            transform.Rotate(0, 0, -rotateValue);
            yield return null;
        }
    }
}
