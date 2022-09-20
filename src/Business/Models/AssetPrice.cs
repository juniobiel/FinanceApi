﻿using Business.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Business.Models
{
    [ExcludeFromCodeCoverage]
    public class AssetPrice
    {
        [Key]
        public Guid AssetId { get; set; }
        public string Ticker { get; set; }
        public double CurrentPrice { get; set; }
        public AssetType AssetType { get; set; }
        public Guid UpdatedByUser { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LastUpdateBySystem { get; set; }

    }
}