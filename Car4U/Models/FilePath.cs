namespace Car4U.Models
{
    using System.ComponentModel.DataAnnotations;
    public class FilePath
    {
        public int FilePathId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public FileType FileType { get; set; }


        public int CarID { get; set; }


        public virtual Car car { get; set; }
    }
}