﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class CreateInventory
    {
        public long ProductId { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public double UnitPrice { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
