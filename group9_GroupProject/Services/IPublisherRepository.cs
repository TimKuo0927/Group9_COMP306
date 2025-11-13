using group9_GroupProject.Models;

namespace group9_GroupProject.Services
{
    public interface IPublisherRepository
    {

        // GET
        Task<IEnumerable<Publisher>> GetAllPublishersAsync();
        Task<Publisher?> GetPublisherByIdAsync(int pusblisherId);

        // POST
        Task<Publisher> AddPublisherAsync(Publisher publisher);

        // PUT (full update)
        Task<Publisher?> UpdatePublisherAsync(int pusblisherId, Publisher publisher);

        // PATCH (partial update)
        Task<Publisher?> PatchPublisherAsync(int pusblisherId, Dictionary<string, object> updates);

        // DELETE
        Task<bool> DeletePublisherAsync(int pusblisherId);
    }
}
