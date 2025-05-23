using System.ComponentModel.DataAnnotations;

namespace NoticeBoard.Web.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заголовок є обов’язковим")]
        public string Title { get; set; } = string.Empty; 
        [Required(ErrorMessage = "Опис є обов’язковим")]
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        [Required(ErrorMessage = "Оберіть категорію")]
        public string Category { get; set; } = string.Empty;
        [Required(ErrorMessage = "Оберіть підкатегорію")]
        public string SubCategory { get; set; } = string.Empty;
    }
}