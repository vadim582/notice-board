﻿namespace NoticeBoard.API.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }
}
