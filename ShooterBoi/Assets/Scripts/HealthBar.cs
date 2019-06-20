using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar
{
    GameObject bar;

    public HealthBar(GameObject bar)
    {
        this.bar = bar;
    }

    public void Update(float currentHealth, float healthMax)
    {
        bar.transform.localScale = new Vector3(currentHealth / healthMax, bar.transform.localScale.y, bar.transform.localScale.z);
    }

}
