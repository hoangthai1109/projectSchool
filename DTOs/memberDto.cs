namespace API.DTOs
{
    public class MemberDto
    {
        public int id {get; set;}
        public string userName {get;set;}
        public int Gender{get;set;}
        public DateOnly DateOfBirth {get;set;}
        public string FullName{get;set;}
        public string Address{get;set;}
        public int PhoneNumber{get;set;}
        public string AccountType {get;set;}
        public string ListQaCode {get;set;}
        public DateTime CreateDate {get;set;}
        public DateTime lastActive {get;set;}
        public string PhotoUrl {get;set;}
        public List<QaDto> Qas {get;set;} = new();
    }
}