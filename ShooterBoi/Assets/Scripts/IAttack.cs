using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackTypes { Melee, Range}
public interface IAttack
{
    void Update();
    void AnimateAttack(Animator anim);
}
