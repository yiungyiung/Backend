﻿using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }
    }
}