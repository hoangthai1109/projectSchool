namespace API.DTOs
{
    public class UserPlsPageDto
    {
        public string userName {get;set;}
        public int PageNumber {get;set;}
        public int PageSize  {get;set;}
        public string Status {get;set;}
        public int isUserUpload {get;set;}
    }
}