namespace Blazor_eCommerce_Project.Models
{
    public class PageItem
    {
        public string Text { get; set; }
        public int PageIndex { get; set; }
        public bool Enable { get; set; }
        public bool IsActive { get; set; }

        public PageItem(int pageIndex,bool enabled,string text)
        {
            PageIndex = pageIndex;
            Enable = enabled;
            Text = text;
        }





    }
}
