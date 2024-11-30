using Allup.Application.Services.Abstracts;
using Allup.Application.UI.Services.Abstracts;
using Allup.Application.UI.Services.Implementations;
using Allup.Application.ViewModels;
using Allup.Domain.Entities;
using Allup.Persistence.Context;
using AutoMapper;
using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using System.Globalization;
using System.Linq.Expressions;

namespace Allup.Application.Services.Implementations
{
    public class BasketItemService : CrudManager<BasketItemViewModel, BasketItem, CreateBasketItemViewModel>, IBasketItemService
    {
        private readonly EfRepositoryBase<BasketItem, AppDbContext> _repository;
        private readonly ICookieService _cookieService;
        private readonly IMapper _mapper;
        private readonly ExternalApiService _externalApiService;

        public BasketItemService(EfRepositoryBase<BasketItem, AppDbContext> repository, ICookieService cookieService, IMapper mapper, ExternalApiService externalApiService) : base(repository, mapper)
        {
            _repository = repository;
            _cookieService = cookieService;
            _externalApiService = externalApiService;
            _mapper = mapper;
        }

        public override async Task<BasketItemViewModel> CreateAsync(CreateBasketItemViewModel createVm)
        {
            var existingBasketItem = await _repository.GetAsync(
                x => x.ClientId == createVm.ClientId && x.ProductId == createVm.ProductId);

            if (existingBasketItem != null)
            {
                existingBasketItem.Count++;
                await _repository.UpdateAsync(existingBasketItem);
                return _mapper.Map<BasketItemViewModel>(existingBasketItem);
            }

            var newBasketItem = _mapper.Map<BasketItem>(createVm);
            var addedEntity = await _repository.AddAsync(newBasketItem);
            return _mapper.Map<BasketItemViewModel>(addedEntity);
        }

        public async Task<BasketItemViewModel> DeleteAsync(int id)
        {
            var existingBasketItem = await _repository.GetAsync(id);
            if (existingBasketItem == null)
            {
                throw new KeyNotFoundException("not found.");
            }

            await _repository.DeleteAsync(existingBasketItem);
            return _mapper.Map<BasketItemViewModel>(existingBasketItem);
        }

        public override async Task<List<BasketItemViewModel>> GetAllAsync(Expression<Func<BasketItem, bool>>? predicate = null, Func<IQueryable<BasketItem>, IOrderedQueryable<BasketItem>>? orderBy = null, Func<IQueryable<BasketItem>, IIncludableQueryable<BasketItem, object>>? include = null)
        {
            var basketItems = await base.GetAllAsync(predicate, orderBy, include);

            var currency = await _cookieService.GetCurrencyAsync();

            var currencyCoefficient = await _externalApiService.GetCurrencyCoefficient(currency.CurrencyCode ?? "azn");
            var cultureInfo = new CultureInfo(currency.IsoCode ?? "az-az");

            foreach (var item in basketItems)
            {
                if(item.Product != null)
                {
                    item.Product.FormattedPrice = (item.Product.Price / currencyCoefficient).ToString("C", cultureInfo);
                }
            }
                return basketItems;
        }

        public async Task<BasketItemViewModel> UpdateAsync(UpdateBasketItemViewModel updateVm)
        {
            var existingEntity = await _repository.GetAsync(updateVm.Id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"BasketItem with ID {updateVm.Id} was not found.");
            }

            Mapper.Map(updateVm, existingEntity);

            var updatedEntity = await _repository.UpdateAsync(existingEntity);

            return Mapper.Map<BasketItemViewModel>(updatedEntity);
        }
    }
}
