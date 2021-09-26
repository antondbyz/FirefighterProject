using System;
using UnityEditor;

public class FixJavaHome : Editor
{
    [InitializeOnLoadMethod]
    static void SetJavaHome()
    {
        string newJDKPath = EditorApplication.applicationPath.Replace("Editor/Unity", "Editor/Data/PlaybackEngines/AndroidPlayer/OpenJDK");
        if (Environment.GetEnvironmentVariable("JAVA_HOME") != newJDKPath)
        {
            Environment.SetEnvironmentVariable("JAVA_HOME", newJDKPath);
        }
    }
}