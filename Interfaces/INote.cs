using Interview_Server.Models;

namespace Interview_Server.Interfaces
{
    public interface INote : IRepository<Note>
    {
        Task ChangeStatusAsync(int noteId, NoteStatus newStatus);
    }
}
