using UnityEngine;
using UnityEditor;
using System.Linq;

public static class Publish
{
    [MenuItem("Frictionless/Publish Package")]
    public static void PublishPackage()
    {
        const string exportFilename = "../Frictionless.unitypackage";

        // As the project now contains .asmdef files, we want to ensure only scripts get exported
        var assetGuids = AssetDatabase.FindAssets("t:script", new[] {"Assets/Frictionless"});

        AssetDatabase.ExportPackage(assetGuids.Select(asset => AssetDatabase.GUIDToAssetPath(asset)).ToArray(),
            exportFilename, ExportPackageOptions.Recurse);
        Debug.Log("Exported " + exportFilename);
    }
}