using System.Linq.Expressions;

namespace Interview_Server.Interfaces
{
    public interface IUserInterview : IRepository<UserInterview>
    {
        Task ChangeStatusAsync(int userInterviewId, InterviewStatus newStatus);
        Task<List<UserInterview>> SearchUserInterviewsAsync(string searchTerm, Expression<Func<UserInterview, object>> include = null);
    }
}
