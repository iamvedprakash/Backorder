﻿using System.ComponentModel.DataAnnotations;

namespace Backorder.Models
{
    public class users
    {
        [Key]
        public int Id { get; set; }

        public string username { get; set; }

        public string password { get; set; }

    }
}
