public interface IAttacker
{
    void TakeDamage(float damage);
    void DealDamage(IAttacker defender);
    void Die();
}
