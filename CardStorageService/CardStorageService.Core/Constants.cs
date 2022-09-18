namespace CardStorageService.Core
{
    public class Constants 
    {
        public const string AppSettingsJSON = "appsettings.json";
        public const string True = "True";
        public const string False = "False";

        public static class SQLProvider
        {
            public const string ConnectionStringPath = "Settings:DataBaseOptions:ConnectionString";
            public const string MSSQL = "mssql";
        }

        public static class ControllerMethods
        {
            public const string Create = "create";
            public const string Load = "load/{id}";
            public const string List = "list";
            public const string Update = "update";
            public const string Delete = "delete/{id}";
        }
    }
}
