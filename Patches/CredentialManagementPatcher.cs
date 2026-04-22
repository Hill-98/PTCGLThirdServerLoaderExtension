using CredentialManagement;
using HarmonyLib;

namespace PTCGLThirdServerLoaderExtension.Patches
{
    internal static class CredentialManagementPatcher
    {
        [HarmonyPatch(typeof(Credential), nameof(Credential.Target), MethodType.Setter)]
        [HarmonyPrefix]
        private static void Credential_TargetSetterPrefix(ref string value)
        {
            value += "_" + Plugin.AccountTarget;
        }
    }
}
