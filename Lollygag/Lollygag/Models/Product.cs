﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Lollygag.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public string PriceInfo { get; set; }
    }
}
