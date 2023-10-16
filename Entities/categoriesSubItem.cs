using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities {
    [Table("CategoriesSubItem")]
    public class CategoriesSubItem {
        public int Id {get;set;}
        public int CategoriesId {get;set;}
        public string Code {get;set;}
        public string Name {get;set;}
        public int Status {get;set;}
        public Categories Categories {get;set;}
    }
}