public class AreaAttack_Ice : AreaAttack
{
    protected override void Attack()
    {
        base.Attack();

        HitCheck(0.2f);

        StartCoroutine(DelayedHitCheck(1f, 1f));
    }
}
