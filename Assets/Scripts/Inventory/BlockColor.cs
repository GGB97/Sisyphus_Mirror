using UnityEngine;

[CreateAssetMenu(fileName = "BlockColor", menuName = "Scriptable Object/BlockColor", order = 0)]
public class BlockColor : ScriptableObject
{
    public ItemGrade Grade;
    public Color color;
}
