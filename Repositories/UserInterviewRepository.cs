using Interview_Server.Interfaces;
using Interview_Server.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using System.Linq.Expressions;

public class UserInterviewRepository : Repository<UserInterview>, IUserInterview
{
    public UserInterviewRepository(DatabaseContext context) : base(context)
    {
    }

    public async Task ChangeStatusAsync(int userInterviewId, InterviewStatus newStatus)
    {
        var userInterview = await  _dbSet.FindAsync(userInterviewId);
        if (userInterview == null)
        {
            throw new KeyNotFoundException($"UserInterview with ID {userInterviewId} not found.");
        }
        userInterview.Status = newStatus;
        await _context.Entry(userInterview).ReloadAsync();

        await _context.SaveChangesAsync();
    }

    public async Task<List<UserInterview>> SearchUserInterviewsAsync(string searchTerm, Expression<Func<UserInterview, object>> include = null)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return new List<UserInterview>();
        }

        var query = _dbSet.AsQueryable();

        if (include != null)
        {
            query = query.Include(include);
        }

        var searchTermWithWildcards = "%" + searchTerm.ToLower() + "%";

        query = query.Where(ui =>
            EF.Functions.Like(ui.Interview.CompanyName.ToLower(), searchTermWithWildcards) ||
            EF.Functions.Like(ui.Interview.Title.ToLower(), searchTermWithWildcards) ||
            EF.Functions.Like(ui.Interview.Address.ToLower(), searchTermWithWildcards) ||
            EF.Functions.Like(ui.Interview.Description.ToLower(), searchTermWithWildcards)
        );

        return await query.ToListAsync();
    }


}
