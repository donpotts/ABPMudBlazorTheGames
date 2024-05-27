namespace TheGames.Permissions;

public static class TheGamesPermissions
{
    public const string GroupName = "TheGames";

    public static class Publishers
    {
        public const string Default = GroupName + ".Publishers";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Games
    {
        public const string Default = GroupName + ".Games";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
