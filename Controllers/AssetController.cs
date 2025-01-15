using AssetService.Interfaces;
using AssetService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace AssetService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;
        private readonly IDistributedCache _cache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5); // Cache duration

        public AssetController(IAssetService assetService, IDistributedCache distributedCache)
        {
            _assetService = assetService;
            _cache = distributedCache;
        }

        // Example: GET /api/asset
        [HttpGet]
        public  async Task<IActionResult> GetAllAssets()
        {
            var assets = await _assetService.GetAllAssetsAsync();
            return Ok(assets);
        }

        // Example: GET /api/asset/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetById(int id)
        {
            string cacheKey = $"AssetCache_{id}";
            string cachedAsset = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedAsset))
            {
                // Return cached data
                var cachedAssetDetail = JsonSerializer.Deserialize<Asset>(cachedAsset);
                return Ok(cachedAssetDetail);
            }

            var asset = await _assetService.GetAssetByIdAsync(id);
            if (asset == null) return NotFound();

            // Store data in cache
            var serializedAsset = JsonSerializer.Serialize(asset);
            await _cache.SetStringAsync(cacheKey, serializedAsset, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration
            });

          return Ok(asset);
        }

        // Example: POST /api/asset
        [HttpPost]
        public async Task<IActionResult> CreateAsset([FromBody] Asset asset)
        {
            var newAsset = await _assetService.AddAssetAsync(asset);
            return CreatedAtAction(nameof(GetAssetById), new { id = newAsset.Id }, newAsset);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsset(int id, [FromBody] Asset asset)
        {
            if (id != asset.Id) return BadRequest();

            var updatedAsset = await _assetService.UpdateAssetAsync(asset);
            return Ok(updatedAsset);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            var deleted = await _assetService.DeleteAssetAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

