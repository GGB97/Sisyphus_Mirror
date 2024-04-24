public class Guard : Skill_Base
{
    protected override void Init()
    {
        _isCooldown = false;
        _cooldown = 20f;
        _currentCooldown = _cooldown;

        _isActive = false;
        _duration = 10f;
        _currentduration = _duration;
    }

    protected override void SetBuff()
    {
        _player.currentStat.def += 60;
        _player.currentStat.shield = _player.currentStat.maxHealth * 0.5f;

        if (_player != null)
        {
            _player.InvokeShieldChange();
        }
    }

    protected override void ResetBuff()
    {
        _player.currentStat.def -= 60;
    }
}
