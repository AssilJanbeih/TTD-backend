namespace Domain.Constants;

public static class Contracts
{
    public static class RootUserSeed
    {
        public const string ID = "249bb140-b08e-46c2-89e7-27dd9fced170";
        public const string USERNAME = "Root";
        public const string EMAIL = "root@gmail.com";
        public const string SECURITYSTAMP = "49c222e7-b72b-496a-8eb9-c1c350f35bb4";
        public const string CONCURRENCYSTAMP = "67e3aafc-1711-4ffb-97a6-1df65b64467e";
        public const string NAME = "Root User";
        public const string TITLE = "Project Manager";
        public const string PASSWORD = ".0Root.0";
    }
    public static class JobTypes
    {
        public const string PROJECT_MANAGER = "Project Manager";
        public const string DESIGN_LEAD= "Design Lead";
        public const string TECH_LEAD = "Tech Lead";
        public const string DESIGNER = "Designer";
        public const string WEB_DEVELOPER = "Web Developer";
    }
    
    public static class CustomClaimsNames
    {
        public const string USER_ID_CLAIM = "UserId";
        public const string JOB_TYPE_CLAIM = "JobType";
        public const string GIVEN_NAME_AR = "GivenNameAr";
        public const string PAGE_ACTIONS = "PageActions";
    }

    public static class IdentityTableNames
    {
        public const string USERS = "Users";
    }

}