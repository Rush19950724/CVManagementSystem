using CVManagementSystem.Data;

namespace CVManagementSystem.Services
{
    public class UserService
    {

        private readonly CVContext _context;

        public UserService(CVContext context)
        {
            _context = context;
        }


    }
}
