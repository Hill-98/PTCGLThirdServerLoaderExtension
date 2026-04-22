using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using PTCGLThirdServerLoaderExtension.Patches;
using System;
using System.IO;

namespace PTCGLThirdServerLoaderExtension
{
    [BepInPlugin("1ca8d0ec-10e9-4017-9600-8df197b0b55e", "PTCGLThirdServerLoaderExtension", "1.1.0")]
    [BepInDependency("2aecaf59-9969-4ea5-b41c-b1ee114568fb", BepInDependency.DependencyFlags.SoftDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public static string AccountTarget { get; private set; } = "";
        public static string BaseDirectory { get { return Path.GetDirectoryName(Instance.Info.Location); } }
        public static Plugin Instance { get; private set; }
        public static ManualLogSource LoggerInstance { get { return Instance.Logger; } }

        private void Awake()
        {
            Instance = this;

                var args = Environment.GetCommandLineArgs();
                var assetBundleProxy = Environment.GetEnvironmentVariable("X_ASSET_BUNDLE_PROXY");
                var httpsProxy = Environment.GetEnvironmentVariable("HTTPS_PROXY");
                var websocketProxy = Environment.GetEnvironmentVariable("X_WEBSOCKET_PROXY");
                var prevArg = "";

                foreach (var arg in args)
                {
                    if (prevArg == "--account-target")
                    {
                        AccountTarget = arg;
                    }

                    prevArg = arg;
                }

                if (!string.IsNullOrWhiteSpace(AccountTarget))
                {
                    try
                    {
                        Harmony.CreateAndPatchAll(typeof(CredentialManagementPatcher));
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex);
                    }
                }
        }
    }
}
