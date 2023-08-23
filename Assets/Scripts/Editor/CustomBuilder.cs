#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;

namespace Build
{
    public class CustomBuilder
    {
        private const string BUILD_PATH = "Builds\\";
        private static string nameAndVersion
        {
            get
            {
                string result = PlayerSettings.productName;
                result += "_V" + PlayerSettings.bundleVersion;

                return result;
            }
        }
        /// <summary>
        /// Gjkxebnm лучить список сцен для билда.
        /// </summary>
        /// <returns></returns>
        private static string[] GetScenes()
        {
            var projectScenes = EditorBuildSettings.scenes;
            List<string> scenesToBuild = new List<string>();
            for (int i = 0; i < projectScenes.Length; i++)
            {
                if (projectScenes[i].enabled)
                {
                    scenesToBuild.Add(projectScenes[i].path);
                }
            }
            return scenesToBuild.ToArray();
        }

        [MenuItem("Build/Build Android")]
        private static void BuildAndroid()
        {
            string[] scenes = GetScenes();

            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = scenes;
            buildPlayerOptions.locationPathName = BUILD_PATH + "Android\\" + nameAndVersion + ".apk";
            buildPlayerOptions.target = BuildTarget.Android;
            buildPlayerOptions.options = BuildOptions.CleanBuildCache | BuildOptions.CompressWithLz4HC;

            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
        [MenuItem("Build/Build Windows")]
        static void BuildWindows()
        {
            string[] scenes = GetScenes();

            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = scenes;
            buildPlayerOptions.locationPathName = BUILD_PATH + "Windows/" + nameAndVersion + ".exe";
            buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
            buildPlayerOptions.options = BuildOptions.None;

            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
    }
}

#endif