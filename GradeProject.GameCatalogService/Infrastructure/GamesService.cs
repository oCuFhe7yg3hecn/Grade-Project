﻿using GradeProject.GameCatalogService.Models;
using GradeProject.GameCatalogService.Models.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Infrastructure
{
    public class GamesService
    {
        private readonly GamesRepository _repo;

        public GamesService(GamesRepository repo)
        {
            _repo = repo;
        }


        public async Task<List<GameInfo>> GetAllAsync(PagingOptions pageOptions)
        {
            return await _repo.Where(_ => true,
                                        pageOptions.PageSize,
                                        pageOptions.Page);
        }

        public async Task<GameInfo> GetByIdAsync(string id) =>
            await _repo.Single(g => g.Id == Guid.Parse(id));

        public async Task<List<GameInfo>> GetByTagsAsync(List<string> tags, int count, int page) =>
            await _repo.Where(g => g.Tags.Any(tag => tags.Contains(tag)),
                                  count,
                                  page);

        public async Task<PaginatedResponse<GameInfo>> GetByCategoriesAsync(List<string> categories, PagingOptions opts)
        {
            var itemsCount = await _repo.CountAsync(g => g.Categories.Any(category => categories.Contains(category)));
            if (itemsCount == 0) { return new PaginatedResponse<GameInfo>(opts.Page, opts.PageSize, itemsCount, null); }

            var games = await _repo.Where(g => g.Categories.Any(category => categories.Contains(category)),
                                  opts.PageSize,
                                  opts.Page);

            return new PaginatedResponse<GameInfo>(opts.Page, opts.PageSize, itemsCount/opts.PageSize, games);
        }

    }
}
