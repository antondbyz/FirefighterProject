using UnityEditor;

[CustomEditor(typeof(Fire))]
public class FireEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Fire fire = (Fire)target;
        fire.Initialize();
    }
}