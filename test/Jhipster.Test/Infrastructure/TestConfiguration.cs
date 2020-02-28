namespace MyCompany.Test.Infrastructure
{
    public static class TestConfiguration
    {
        public static string ConnectionString
        {
            get
            {
                return "mongodb://localhost:27017";
            }
        }

        public static string DatabaseName
        {
            get
            {
                return "TestDB";
            }
        }
    }
}
