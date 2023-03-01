using Microsoft.EntityFrameworkCore;

namespace esabzi.Models.Interfaces
{
    public interface IRepo
    {
        //get all users minimal information
        public List<User> getAllUsernames();
        //get all users requires admin previllage
        public List<User> getAllUsers();
        //signup user
        public void signup(User user);
        //get a single user against username 
        public User? getUserByUsername(User user);
        public User? getUserById(int id);
        //returns 1 if profile picture upadated successfully,
        //returns -1 if user not found against the given id
        public int updateProfile(int id, string url);
        //returns 1 if address upadated successfully,
        //returns -1 if user not found against the given id
        public int updateAddress(int id, string address);
    }
}
