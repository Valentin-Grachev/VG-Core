using UnityEngine;


namespace VG.Internal
{
    public static class Core
    {
        public const string version = "v.1.2.0";

        private const string vgDebugColor = "#9D68D3";
        private const string editorDebugColor = "#00B454";

        public static class Error
        {
            public static void NoSupportedService(string managerName)
            {
                throw new System.Exception(Prefix(managerName) + "No supported service.");
            }

            public static void ProductDoesNotExists(string managerName, string key_product)
            {
                throw new System.Exception(Prefix(managerName) + $"Product \"{key_product}\" does not exists!");
            }
        }

        public static class Message
        {
            public static string Initialized(string managerName) => "Initialized.";
        }

        public static string Prefix(string name)
        {
            if (Environment.editor) return $"<color={vgDebugColor}>[{name}] </color>";
            else return $"{name}: ";
        }

        public static void LogEditor(string message)
        {
            string prefix = $"<color={editorDebugColor}>Editor mode: </color>";
            Debug.Log(prefix + message);
        }

        public static string Green(string message)
            => Environment.editor ? $"<color=green>{message}</color>" : message;


    }
}


