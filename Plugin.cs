using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using PTCGLThirdServerLoaderExtension.Patches;
using System;
using System.IO;
using Vuplex.WebView;

namespace PTCGLThirdServerLoaderExtension
{
    [BepInPlugin("1ca8d0ec-10e9-4017-9600-8df197b0b55e", "PTCGLThirdServerLoaderExtension", "1.0.0")]
    [BepInDependency("2aecaf59-9969-4ea5-b41c-b1ee114568fb", BepInDependency.DependencyFlags.SoftDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public static string AccountTarget { get; private set; } = string.Empty;
        public static string BaseDirectory { get { return Path.GetDirectoryName(Instance.Info.Location); } }
        public static Plugin Instance { get; private set; }
        public static ManualLogSource LoggerInstance { get { return Instance.Logger; } }

        private void Awake()
        {
            Instance = this;

            var args = Environment.GetCommandLineArgs();

            var prevArg = string.Empty;

            foreach (var arg in args)
            {
                if (prevArg == "--account-target")
                {
                    AccountTarget = arg;
                }
                if (arg == "--fix-builtin-browser-error")
                {
                    StandaloneWebView.SetCommandLineArguments("--disable-gpu --no-sandbox");
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
