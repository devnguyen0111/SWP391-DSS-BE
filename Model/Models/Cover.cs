﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class Cover
{
    public int CoverId { get; set; }

    public string CoverName { get; set; }

    public string Status { get; set; }

    public decimal? UnitPrice { get; set; }

    public int SubCategoryId { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<CoverMetaltype> CoverMetaltypes { get; set; } = new List<CoverMetaltype>();

    public virtual ICollection<CoverSize> CoverSizes { get; set; } = new List<CoverSize>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual SubCategory SubCategory { get; set; }
}