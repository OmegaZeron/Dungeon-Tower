using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {
    void TakeDamage(int damageTaken = 0, float knockback = 0);
    void Die();
}
