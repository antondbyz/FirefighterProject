using UnityEditor;

[CustomEditor(typeof(PlayerSkinInitializer))]
public class PlayerSkinInitializerEditor : Editor 
{
    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();
        PlayerSkinInitializer initializer = (PlayerSkinInitializer)target;
        initializer.UpdateSkin();
    }
}