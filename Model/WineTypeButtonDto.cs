
namespace Model
{
    public class WineTypeButtonDto
    {       
        public int Id { get; set; }
        public WineDto Wine { get; set; }
        public int ParentButtonId { get; set; }
        public int ScreenId { get; set; }
        public int CategoryId { get; set; }
        public int ArticleId { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int ColorCode { get; set; }
        public string FontName { get; set; }
        public decimal FontSize { get; set; }
        public int FontColorCode { get; set; }
        public bool IsFontBold { get; set; }
        public bool IsFontItalic { get; set; }
        public bool IsFontUnderline { get; set; }
    }
}
