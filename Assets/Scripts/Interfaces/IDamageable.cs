using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {
    void TakeDamage(uint damageTaken = 0, float knockback = 0);
    void Die();
    void TakeKnockback(float knockback);
}
