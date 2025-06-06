public class UserData
{
    private User _user;

    public User User => _user;

    public UserData()
    {
        
    }

    public void Initialize(User user)
    {
        _user = user;
    }
}
