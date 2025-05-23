using NoticeBoard.API.Models;

namespace NoticeBoard.API.Repositories
{
    public interface IAnnouncementRepository
    {
        Task<IEnumerable<Announcement>> GetAllAsync();
        Task<Announcement?> GetByIdAsync(int id);
        Task<int> CreateAsync(Announcement announcement);
        Task<bool> UpdateAsync(Announcement announcement);
        Task<bool> DeleteAsync(int id);
    }
}