using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    public float CooldownTime;
    public bool CanDoAction;
    public void StartTimer() => StartCoroutine(ICooldown());

    private void Start()
    {

        CanDoAction = true;
    }

    private IEnumerator ICooldown()
    {
        CanDoAction = false;
        yield return new WaitForSeconds(CooldownTime);
        CanDoAction = true;

    }
}
