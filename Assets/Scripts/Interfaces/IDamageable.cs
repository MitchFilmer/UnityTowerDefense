using System;

public interface IDamageable
{
    void Damage(float dmg);
    void SetDamageHandler(Action<float> OnDamageTaken);
}
