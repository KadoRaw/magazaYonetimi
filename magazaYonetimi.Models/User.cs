public class User
{
    public User()
    {
    }

    public User(string userName, string email, string password, UserRole userRole)
    {
        this.userName = userName;
        this.email = email;
        this.password = password;
        this.userRole = userRole;
    }

    public string userName;
    public string email;
    public string password;
    public UserRole userRole;
}