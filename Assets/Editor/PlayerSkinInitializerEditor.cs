using UnityEditor;

[CustomEditor(typeof(PlayerSkinInitializer))]
public class PlayerSkinInitializerEditor : Editor 
{
    public override void OnInspectorGUI() 
    {
        PlayerSkinInitializer initializer = (PlayerSkinInitializer)target;
        GameManager.LoadPlayerSkins();
        initializer.UpdateSkin();
        base.OnInspectorGUI();
    }
}