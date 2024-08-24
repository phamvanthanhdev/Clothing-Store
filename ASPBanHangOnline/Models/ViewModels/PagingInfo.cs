namespace ASPBanHangOnline.Models.ViewModels
{
    public class PagingInfo
    {
        //số lượng sản phẩm 1 trang
        public int pageSize { get; set; }
        //Trang hiện tại
        public int pageCurrent { get; set; }
        //Tổng sản phẩm
        public int pageCount { get; set; }
        public int totalPage() => (int)Math.Ceiling((double)pageCount / pageSize);
    }
}
