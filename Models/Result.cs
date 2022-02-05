using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace _1262228_Arosh.Models
{
    public class Result
    {
        [Key]
        public int ResultID { get; set; }
        public int? Bangla { get; set; }
        public int? English { get; set; }
        public int? GeneralKnowlede { get; set; }
        public int? Ict { get; set; }
       // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? TotalMark
        {
            get { return (Bangla + English + GeneralKnowlede + Ict); }
            private set { }
        }

        public int StudentID { get; set; }
        public virtual Student Student { get; set; }



    }
}