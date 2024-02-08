using LibraryManagement.Debugging;

namespace LibraryManagement
{
    public class LibraryManagementConsts
    {
        public const string LocalizationSourceName = "LibraryManagement";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "6bea6a9f2c5f435eaed8fda98142ca66";
    }
}
