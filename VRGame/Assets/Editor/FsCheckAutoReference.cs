using UnityEditor;

[InitializeOnLoad]
public static class FsCheckAutoReference
{
    static FsCheckAutoReference()
    {
        SetAutoReferenced("Assets/Packages/FsCheck.3.3.2/lib/netstandard2.0/FsCheck.dll");
        SetAutoReferenced("Assets/Packages/FSharp.Core.6.0.7/lib/netstandard2.0/FSharp.Core.dll");
    }

    private static void SetAutoReferenced(string path)
    {
        var importer = AssetImporter.GetAtPath(path) as PluginImporter;
        if (importer != null)
        {
            if (importer.GetCompatibleWithAnyPlatform()) return;
            importer.SetCompatibleWithAnyPlatform(true);
            importer.SaveAndReimport();
        }
    }
}
