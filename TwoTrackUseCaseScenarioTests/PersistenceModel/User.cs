namespace TwoTrackUseCaseScenarioTests.PersistenceModel
{
    internal class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public User(int userId, string userName)
        {
            UserId = userId;
            UserName = userName;

        }

        public static User Empty() => new User(0, "unknown");
    }
}
