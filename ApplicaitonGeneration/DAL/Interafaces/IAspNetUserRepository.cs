using ApplicationGeneration;

namespace ApplicationGeneration.DAL.Interfaces
{
    public interface IAspNetUserRepository : IRepository<AspNetUser>
    {
        // Add aspnetuser specific method contracts here
        bool CheckIfUserExists(string email);
    }
}
