using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Car4U.Models
{
    public class ExtraModel
    {
        public int ID { get; set; }

        [Display(Name = "Nome Modelo")]
        [Required (ErrorMessage = "O campo 'Nome Modelo' é necessário.")]        
        public string Model { get; set; }

        [Display(Name = "Preço")]
        [Range(0.00, Int32.MaxValue, ErrorMessage = "O Preço deve ser maior que 0.")]
        public double Price { get; set; }

        [Range(0,20, ErrorMessage = "O Stock deve ser entre 0 e 20")]
        public int Stock { get; set; }

        [Display (Name = "Tipo de Modelo")]
        public int ExtraModelTypeID { get; set; }


        public virtual ExtraModelType ExtraModelType { get; set; }
        public virtual ICollection<ExtraItem> ExtraItems { get; set; }
    }
}