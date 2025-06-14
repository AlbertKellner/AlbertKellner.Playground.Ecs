﻿using Playground.Application.Features.Country.Query.GetAll.Models;

namespace Playground.Application.Features.Country.Query.GetAll.Interface
{
    public interface IGetAllCountryRepository
    {
        Task<IEnumerable<GetAllCountryOutput>> GetAllCountryAsync(CancellationToken cancellationToken);
    }
}
