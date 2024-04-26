using UnityEngine;

[CreateAssetMenu(fileName = "WaveSO")]
public class WaveSO : ScriptableObject
{
    public int[] normal;
    public int[] elite;
    public int[] boss;

    public int maxEnemyCnt = 50;

    public int numPerSpawn;
    public float spawnDelay;

    public float eliteSpawnChance = 5f;

    public int GetAllLength()
    {
        return normal.Length + elite.Length + boss.Length;
    }

    public void ModifierInit()
    {
        int stage = DungeonManager.Instance.currnetstage;

        maxEnemyCnt = (stage / 3) * 2;
        numPerSpawn = stage / 3;
    }
}
