using UnityEditor;

[CustomEditor(typeof(PlayerSkinsManager))]
public class PlayerSkinsManagerEditor : Editor 
{
    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();
        PlayerSkinsManager manager = (PlayerSkinsManager)target;
        manager.Initialize();
    }
}