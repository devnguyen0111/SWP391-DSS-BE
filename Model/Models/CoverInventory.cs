﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class CoverInventory
{
    public int InventoryId { get; set; }

    public int CoverId { get; set; }

    public int SizeId { get; set; }

    public int MetaltypeId { get; set; }

    public int Quantity { get; set; }

    public virtual Cover Cover { get; set; }

    public virtual Metaltype Metaltype { get; set; }

    public virtual Size Size { get; set; }
}