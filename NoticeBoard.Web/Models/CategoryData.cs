namespace NoticeBoard.Web.Models
{
    public class CategoryData
    {
        public static readonly Dictionary<string, List<string>> Categories = new()
        {
            ["Побутова техніка"] = new() { "Холодильники", "Пральні машини", "Бойлери", "Печі", "Витяжки", "Мікрохвильові печі" },
            ["Комп'ютерна техніка"] = new() { "ПК", "Ноутбуки", "Монітори", "Принтери", "Сканери" },
            ["Смартфони"] = new() { "Android смартфони", "iOS/Apple смартфони" },
            ["Інше"] = new() { "Одяг", "Взуття", "Аксесуари", "Спортивне обладнання", "Іграшки" }
        };
    }
}