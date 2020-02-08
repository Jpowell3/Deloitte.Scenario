using AutoMapper;
using Deloitte.Scenario.Data;
using Deloitte.Scenario.Data.Entities;
using Deloitte.Scenario.Models.CityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deloitte.Scenario.Data
{

}
public class CityRepository : ICityRepository
{
    private readonly ICityContext _cityContext;
    private readonly IMapper _mapper;

    public CityRepository(ICityContext cityContext, IMapper mapper)
    {
        _cityContext = cityContext ?? throw new ArgumentNullException(nameof(cityContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<City>> GetCityByNameAsync(string name)
    {
        var cities = _mapper.Map<IEnumerable<CityEntity>, IEnumerable<City>>(await _cityContext.Cities
                                                                       .Where(c => c.Name.ToLower() == name.ToLower())
                                                                       .ToListAsync());
        return cities;
    }

    public async Task<int> AddCityAsync(City city)
    {
        var cityEntity = _mapper.Map<City, CityEntity>(city);

        await _cityContext.Cities.AddAsync(cityEntity);

        await _cityContext.SaveChangesAsync();

        return cityEntity.Id;
    }

    public async Task<bool> DeleteCityAsync(int id)
    {
        var cityEntity = await _cityContext.Cities.Where(c => c.Id == id).FirstOrDefaultAsync();

        if (cityEntity == null)
            return false;

        _cityContext.Cities.Remove(cityEntity);

        await _cityContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateCityAsync(int id, City city)
    {
        var cityEntity = await _cityContext.Cities.Where(c => c.Id == id).FirstOrDefaultAsync();

        cityEntity.EstimatedPopulation = city.EstimatedPopulation;
        cityEntity.DateEstablished = city.DateEstablished;
        cityEntity.TouristRating = city.TouristRating;

        _cityContext.Cities.Update(cityEntity);

        await _cityContext.SaveChangesAsync();

        return true;
    }

    public async Task<City> GetCityByIdAsync(int id)
    {
        var city = _mapper.Map<CityEntity, City>(await _cityContext.Cities.Where(c => c.Id == id).FirstOrDefaultAsync());

        return city;
    }

}

