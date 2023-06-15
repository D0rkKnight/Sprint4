using UnityEditor;
using UnityEngine;

public class Exporter : EditorWindow
{
    private static string[] foldersToExport = new string[]
    {

        // Add more folder names here as needed
    };

    [MenuItem("Tools/Export Packages")]
    private static void ExportPackages()
    {

        // Import foldersToExport from Config/export_manifest.txt

        string manifestPath = "Assets/Config/export_manifest.txt";
        string[] manifestLines = System.IO.File.ReadAllLines(manifestPath);

        // Trim whitespace from each line
        for (int i = 0; i < manifestLines.Length; i++)
        {
            manifestLines[i] = manifestLines[i].Trim();
        }

        // Remove empty lines
        manifestLines = System.Array.FindAll(manifestLines, line => !string.IsNullOrEmpty(line));

        // Remove comment lines
        manifestLines = System.Array.FindAll(manifestLines, line => !line.StartsWith("#"));

        foldersToExport = manifestLines;

        Exporter window = GetWindow<Exporter>();
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Export Folders", EditorStyles.boldLabel);

        foreach (string folder in foldersToExport)
        {
            EditorGUILayout.LabelField(folder);
        }

        if (GUILayout.Button("Export"))
        {

            // Check if Assets/Exports exists and if not create it
            if (!System.IO.Directory.Exists("Assets/Exports"))
            {
                System.IO.Directory.CreateDirectory("Assets/Exports");
            }

            foreach (string folder in foldersToExport)
            {
                string packagePath = $"Assets/Exports/{folder}.unitypackage";
                AssetDatabase.ExportPackage($"Assets/{folder}", packagePath, ExportPackageOptions.Default | ExportPackageOptions.Recurse);

                Debug.Log($"Exported package: {packagePath}");
            }

            // Export a package for just the library assets as well
            string libraryPackagePath = $"Assets/Exports/Config.unitypackage";
            AssetDatabase.ExportPackage("Assets/Config", libraryPackagePath, ExportPackageOptions.Default | ExportPackageOptions.Recurse | ExportPackageOptions.IncludeLibraryAssets);

            Debug.Log("Export process complete!");
        }
    }
}
