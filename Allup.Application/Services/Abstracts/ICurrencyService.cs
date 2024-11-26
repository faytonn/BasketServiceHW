﻿using Allup.Application.ViewModels;
using Allup.Domain.Entities;

namespace Allup.Application.Services.Abstracts;

public interface ICurrencyService : ICrudService<CurrencyViewModel, Currency>
{
    Task<CurrencyViewModel> GetCurrencyAsync(string isoCode);
}