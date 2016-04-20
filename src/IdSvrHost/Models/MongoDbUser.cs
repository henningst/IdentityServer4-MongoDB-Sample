namespace IdSvrHost.Models
{
    public class MongoDbUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public bool IsActive { get; set; }
        public string HashedPassword { get; set; }
    }
}
