namespace ApiApplication
{
    public static class Constants
    {
        public static class Headers {
            public const string ApiKey = "X-Apikey";
        }
        public static class ConfigKeys
        {
            public const string Api = "ApiKey";
            public const string ApiUrl = nameof(ApiUrl);
            public const string RedisUrl = nameof(RedisUrl);
        }
    }
}
