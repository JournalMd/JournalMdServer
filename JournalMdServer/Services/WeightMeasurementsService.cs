using System.Collections.Generic;
using System.Threading.Tasks;
using JournalMdServer.Repositories;
using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using JournalMdServer.DTOs.WeightMeasurements;
using AutoMapper;
using JournalMdServer.Helpers;
using System.Web.Http.ModelBinding;

namespace JournalMdServer.Services
{
    public class WeightMeasurementsService
    {
        private readonly IRepository<WeightMeasurement> _repository;
        private readonly IMapper _mapper;

        public WeightMeasurementsService(IRepository<WeightMeasurement> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WeightMeasurementOutput>> GetItems(long userId)
        {
            var weightMeasurements = await _repository.Query.Where(m => m.UserId == userId).ToListAsync();
            return _mapper.Map<List<WeightMeasurementOutput>>(weightMeasurements);
            //var weightMeasurements = _repository.Query.GetPaged(1, 3);
            //return _mapper.Map<PagedResult<WeightMeasurementOutput>>(weightMeasurements);
        }

        public async Task<WeightMeasurementOutput> GetItem(long id, long userId)
        {
            var weightMeasurement = await _repository.Query.FirstOrDefaultAsync(m => m.UserId == userId && m.Id == id);
            return _mapper.Map<WeightMeasurementOutput>(weightMeasurement);
        }

        public async Task<WeightMeasurementOutput> Insert(WeightMeasurementInput weightMeasurement, long userId)
        {
            var entry = _mapper.Map<WeightMeasurement>(weightMeasurement);
            entry.UserId = userId;
            _repository.Insert(entry);
            await _repository.Context.SaveChangesAsync();
            return _mapper.Map<WeightMeasurementOutput>(entry);
        }

        public async Task Update(long id, WeightMeasurementInput weightMeasurement, long userId)
        {
            var dbWeightMeasurement = await _repository.Query.FirstOrDefaultAsync(m => m.UserId == userId && m.Id == id);

            if (id != dbWeightMeasurement.Id)
                throw new ArgumentException("Invalid id");

            dbWeightMeasurement = _mapper.Map<WeightMeasurementInput, WeightMeasurement>(weightMeasurement, dbWeightMeasurement);
            _repository.Update(dbWeightMeasurement);
            await _repository.Context.SaveChangesAsync();
        }

        public async Task<long?> Delete(long id, long userId)
        {
            var weightMeasurement = await _repository.Query.FirstOrDefaultAsync(m => m.UserId == userId && m.Id == id);

            if (weightMeasurement == null)
                return null;

            _repository.Delete(weightMeasurement);
            await _repository.Context.SaveChangesAsync();

            return id;
        }

        private bool ItemExists(long id)
        {
            return _repository.Query.Any(e => e.Id == id);
        }
    }
}
