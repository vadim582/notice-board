using Microsoft.Data.SqlClient;
using NoticeBoard.API.Models;
using System.Data;

namespace NoticeBoard.API.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly string _connectionString;

        public AnnouncementRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SQLServer")!;
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync()
        {
            var announcements = new List<Announcement>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_GetAllAnnouncements", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                announcements.Add(ReadAnnouncement(reader));
            }

            return announcements;
        }

        public async Task<Announcement?> GetByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_GetAnnouncementById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Id", id);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return ReadAnnouncement(reader);
            }

            return null;
        }

        public async Task<int> CreateAsync(Announcement a)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_InsertAnnouncement", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Title", a.Title);
            cmd.Parameters.AddWithValue("@Description", a.Description);
            cmd.Parameters.AddWithValue("@Status", a.Status);
            cmd.Parameters.AddWithValue("@Category", a.Category);
            cmd.Parameters.AddWithValue("@SubCategory", a.SubCategory);

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<bool> UpdateAsync(Announcement a)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_UpdateAnnouncement", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Id", a.Id);
            cmd.Parameters.AddWithValue("@Title", a.Title);
            cmd.Parameters.AddWithValue("@Description", a.Description);
            cmd.Parameters.AddWithValue("@Status", a.Status);
            cmd.Parameters.AddWithValue("@Category", a.Category);
            cmd.Parameters.AddWithValue("@SubCategory", a.SubCategory);

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_DeleteAnnouncement", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Id", id);
            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        private Announcement ReadAnnouncement(SqlDataReader reader)
        {
            return new Announcement
            {
                Id = (int)reader["Id"],
                Title = reader["Title"].ToString()!,
                Description = reader["Description"].ToString()!,
                CreatedDate = (DateTime)reader["CreatedDate"],
                Status = (bool)reader["Status"],
                Category = reader["Category"].ToString()!,
                SubCategory = reader["SubCategory"].ToString()!
            };
        }
    }
}