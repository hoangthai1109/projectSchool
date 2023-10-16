namespace API.Helpers
{
    public class PaginationHeader
    {
        public PaginationHeader(int currentPage, int itemPerpage, int totalItems, int totalPages)
        {
            CurrentPage = currentPage;
            ItemPerpage = itemPerpage;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }

        public int CurrentPage { get; set; }       
        public int ItemPerpage {get;set;}
        public int TotalItems {get;set;}
        public int TotalPages {get;set;}
    }
}