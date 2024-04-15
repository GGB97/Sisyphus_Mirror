public class AreaAttack_Ice : AreaAttack
{
    protected override void Attack()
    {
        base.Attack();
        SoundManager.Instance.PlayAudioClip(ProjectileID.AreaAttack_Ice.ToString());
        HitCheck(0.2f);

        StartCoroutine(DelayedHitCheck(1f, 1f));
    }
}
