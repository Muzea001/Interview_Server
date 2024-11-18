using Interview_Server.Interfaces;
using Interview_Server.Models;

namespace Interview_Server.Repositories
{
    public class NoteRepository : Repository<Note>, INote
    {
        public NoteRepository (DatabaseContext context) : base(context)
        {

        }

        public async Task ChangeStatusAsync(int noteId, NoteStatus newStatus)
        {
            var note = await _context.Notes.FindAsync(noteId); //finding note to update

            if (note == null)
            {
                throw new KeyNotFoundException($"Note with ID {noteId} not found.");
            } //if note is not found, throw exception

            note.Status = newStatus; // update note status
            
            _dbSet.Update(note); //setting the note as updated in the database and saving the changes.
            await _context.SaveChangesAsync();
        }
    }
}
