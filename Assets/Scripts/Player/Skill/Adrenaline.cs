public class Adrenaline : Skill_Base
{
    protected override void Init()
    {
        _isCooldown = false;
        _cooldown = 20f;
        _currentCooldown = _cooldown;

        _isActive = false;
        _duration = 6f;
        _currentduration = _duration;
    }

    protected override void SetBuff()
    {
        _player.currentStat.critRate += 40;
        _player.currentStat.attackSpeed += 5;
    }

    protected override void ResetBuff()
    {
        base.ResetBuff();
        _player.currentStat.critRate -= 40;
        _player.currentStat.attackSpeed -= 5;
    }
}
