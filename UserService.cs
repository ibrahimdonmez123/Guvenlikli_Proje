namespace Guvenlikli_Proje
{
    public class UserService
    {
        private List<User> users = new List<User>
    {
        new User { Id = 1, Username = "İbo", Password = "sifrepassword1234" },
        new User { Id = 2, Username = "İbrahim", Password = "sifrepassword12345" }
    };

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }

}
