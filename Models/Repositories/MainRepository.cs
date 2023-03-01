using esabzi.Models.Interfaces;
namespace esabzi.Models.Repositories
{
    public class MainRepository: IRepo
    {
        private readonly EsabziContext _context;

        public MainRepository()
        {
            _context = new EsabziContext();
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

        public User? getUserById(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }
        
        //returns 1 if profile picture upadated successfully,
        //returns -1 if user not found against the given id
        public int updateProfile(int id, string url)
        {
            User user = getUserById(id);
            if (user == null) return -1;
            user.Picture = url;
            _context.SaveChanges();
            return 1;
        }

        //returns 1 if address upadated successfully,
        //returns -1 if user not found against the given id
        public int updateAddress(int id, string address)
        {
            User user = getUserById(id);
            if (user == null) return -1;
            user.Address = address;
            _context.SaveChanges();
            return 1;
        }


    }
}
