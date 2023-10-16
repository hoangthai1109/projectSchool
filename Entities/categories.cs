using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities {
    [Table("Categories")]
    public class Categories {
        public int Id {get;set;}
        public string Code {get;set;}
        public string CatName {get;set;}
        public int isParent {get;set;} 
        public string? CatParentName {get;set;}
        public int Status {get;set;}
        public IList<CategoriesSubItem> CategoriesSubItems {get;set;}
    }
}