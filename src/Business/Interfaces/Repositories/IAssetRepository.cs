﻿using Business.Models;

namespace UnitTests.Services
{
    public interface IAssetRepository
    {
        Task<int> CreateNewAsset( Asset asset );
    }
}