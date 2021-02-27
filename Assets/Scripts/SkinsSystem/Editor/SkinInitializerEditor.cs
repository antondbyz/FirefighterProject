using UnityEditor;

[CustomEditor(typeof(SkinInitializer))]
public class SkinInitializerEditor : Editor 
{
    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();
        SkinInitializer initializer = (SkinInitializer)target;
        initializer.UpdateSkin();
    }
}