using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private Material originalMat;

    [Header("Color Effect")]
    [SerializeField] private Color[] trapDamage;
    [SerializeField] private Color[] enemyDamage;

    [Header("flash FX")]
    [SerializeField] private float flashDuration;
    [SerializeField] private Material hitMat;


    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    private void CancelColorChange()
    {
        CancelInvoke();

        sr.color = Color.white;
    }

    public void TrapDamgeFxFor(float _seconds)
    {
        InvokeRepeating("TrapDamageColorFx", 0, .3f);
        Invoke("CancelColorChange", _seconds);
    }

    private void TrapDamageColorFx()
    {
        if (sr.color != trapDamage[0])
            sr.color = trapDamage[0];
        else
            sr.color = trapDamage[1];
    }

    public void EnemyDamgeFxFor(float _seconds)
    {
        InvokeRepeating("EnemyDamageColorFx", 0, .3f);
        Invoke("CancelColorChange", _seconds);
    }

    private void EnemyDamageColorFx()
    {
        if (sr.color != enemyDamage[0])
            sr.color = enemyDamage[0];
        else
            sr.color = enemyDamage[1];
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        Color currentColor = sr.color;
        sr.color = Color.white;
        yield return new WaitForSeconds(flashDuration);

        sr.color = currentColor;
        sr.material = originalMat;
    }
}
