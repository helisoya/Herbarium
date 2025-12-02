using System.IO;
using Codice.Client.Commands;
using UnityEditor;
using UnityEditor.Build.Profile;
using UnityEditor.Build.Reporting;
using UnityEngine;

/// <summary>
/// Handles the builds of Herbarium
/// </summary>
public class HerbariumBuild
{
    [MenuItem("Herbarium/Build/Debug")]
    public static void BuildDebug()
    {
        Debug.Log("Building...");

        BuildProfile buildProfile = AssetDatabase.LoadAssetAtPath<BuildProfile>("Assets/Settings/Build Profiles/Debug.asset");
        BuildPlayerWithProfileOptions options = new BuildPlayerWithProfileOptions()
        {
            buildProfile = buildProfile,
            locationPathName = "../HerbariumBuilds/debugBuild",
            options = BuildOptions.Development,
        };

        BuildReport report = BuildPipeline.BuildPlayer(options);
        BuildSummary summary = report.summary;

        // Output the build size or a failure depending on BuildPlayer.
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");   
        }
        WriteLog(report,"Debug Build Summary");
    }

    [MenuItem("Herbarium/Build/Release")]
    public static void BuildRelease()
    {
        Debug.Log("Building...");

        BuildProfile buildProfile = AssetDatabase.LoadAssetAtPath<BuildProfile>("Assets/Settings/Build Profiles/Release.asset");
        BuildPlayerWithProfileOptions options = new BuildPlayerWithProfileOptions()
        {
            buildProfile = buildProfile,
            locationPathName = "../HerbariumBuilds/releaseBuild",
            options = BuildOptions.None,
        };

        BuildReport report = BuildPipeline.BuildPlayer(options);
        BuildSummary summary = report.summary;

        // Output the build size or a failure depending on BuildPlayer.
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
        WriteLog(report,"Release Build Summary");
    }

    private static void WriteLog(BuildReport report,string message)
    {
        StreamWriter writer = new StreamWriter("build.log",false);
        writer.WriteLine(message);
        foreach(BuildStep step in report.steps)
        {
            writer.WriteLine("-------");
            writer.WriteLine(step.ToString());
            foreach(BuildStepMessage mes in step.messages)
            {
                writer.WriteLine(mes.type + " : "+mes.content);
            }
        }
        writer.Close();
    }
}
