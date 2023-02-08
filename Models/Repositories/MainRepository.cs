using esabzi.DB;

namespace esabzi.Models.Repositories
{
    public class MainRepository
    {
        private readonly EsabziContext _context;
        private readonly IConfiguration _config;

        public MainRepository(EsabziContext context, IConfiguration config)
        {
            _context = new EsabziContext();
            _config = config;
        }

        //get all users minimal information
        public List<User> getAllUsernames()
        {
            return _context.Users.Select(u => new User
            {
                Username = u.Username,
                Email = u.Email
            }).ToList();
        }

        //get all users requires admin previllage
        public List<User> getAllUsers()
        {
            return _context.Users.ToList();
        }

        //signup user
        public void signup(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        //get a single user against username 
        public User? getUserByUsername(User user)
        {
           return _context.Users.Where(u => u.Username == user.Username).FirstOrDefault();
        }
    }
}
