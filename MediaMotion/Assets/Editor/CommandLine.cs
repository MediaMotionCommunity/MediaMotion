using UnityEditor;

class CommandLine
{
     static void PerformBuild ()
     {
		EditorApplication.ExecuteMenuItem("Assets/Sync MonoDevelop Project");
     }
}