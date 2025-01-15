using AssetService.Data;
using AssetService.Interfaces;
using AssetService.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AssetService.Services
{
    public class AssetManagementService: IAssetService
    {
        private readonly AppDbContext _context;

        public AssetManagementService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asset>> GetAllAssetsAsync()
        {
            return await _context.Assets.ToListAsync();
        }

        public async Task<Asset> GetAssetByIdAsync(int id)
        {
            return await _context.Assets.FindAsync(id);
        }

        public async Task<Asset> AddAssetAsync(Asset asset)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
            return asset;
        }

        public async Task<Asset> UpdateAssetAsync(Asset asset)
        {
            _context.Assets.Update(asset);
            await _context.SaveChangesAsync();
            return asset;
        }

        public async Task<bool> DeleteAssetAsync(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null) return false;

            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
