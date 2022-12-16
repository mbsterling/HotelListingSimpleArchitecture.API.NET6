﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Models.Country;
using AutoMapper;
using HotelListing.API.Contracts;
using Microsoft.AspNetCore.Authorization;
using HotelListing.API.Exceptions;
using HotelListing.API.Models;

namespace HotelListing.API.Controllers
{
    [Route("api/v{version:apiVersion}/countries")]    
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class CountriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;

        ///<summary>
        ///JUST GENERAL NOTES ABOUT THE API BEST PRACTICES BEFORE WE 
        ///MAKE THE THE CHANGES...
        ///
        ///We don't want to talk directly to the db context object in our 
        ///controller, so we introduce another layer of abstraction here
        ///which I assume to be the business layer.
        ///
        /// We'll be discussing data 'transfer objects' or a "model"
        ///</summary>
        ///
        public CountriesController(IMapper mapper, ICountriesRepository countriesRepository)
        {
            this._mapper = mapper;
            this._countriesRepository = countriesRepository;
        }

        // GET: api/Countries
        [HttpGet("GetAll")]

        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countries = await _countriesRepository.GetAllAsync();
            /*
             * var records = _mapper.Map<GetCountryDto>(countries);
             * 
             * SINCE WE'RE STILL GETTING A LIST OF countries FOR OUR Dto
             * THE ABOVE records VAR WON'T WORK. WE NEED A LIST of 
             * <GetCountryDto>s to be returned. "LIKE TYPE FOR LIKE TYPE
             * SO BELOW WE USE: _mapper.Map<List<GetCountryDto>>(countries)
             */
            var records = _mapper.Map<List<GetCountryDto>>(countries);
            return Ok(records);
        }

        // GET: api/Countries?StartIndex=0&PageSize=25&PageNumber=1
        [HttpGet]

        public async Task<ActionResult<PagedResult<GetCountryDto>>> GetPagedCountries([FromQuery] QueryParameters queryParameters)
        {
            var pagedCountriesResult = await _countriesRepository.GetAllAsync<GetCountryDto>(queryParameters);
            return Ok(pagedCountriesResult);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _countriesRepository.GetDetails(id);

            if (country == null)
            {
                throw new NotFoundException(nameof(GetCountry), id);
            }

            var countryDto = _mapper.Map<CountryDto>(country);

            return Ok(countryDto);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest("Invalid Record Id");
            }

            ////_context.Entry(country).State = EntityState.Modified;
            ///            
            var country = await _countriesRepository.GetAsync(id);
            if (country == null) 
            {
                throw new NotFoundException(nameof(GetCountries), id);
            }

            _mapper.Map(updateCountryDto, country);

            try
            {
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /// <summary>
        /// So to avoid over posting we're replacing the Country object below
        /// with our new CreateCountryDto object which only takes a name and 
        /// a short name.
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountryDto)
        {
            var country = _mapper.Map<Country>(createCountryDto);

            await _countriesRepository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles="Administrator")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRepository.GetAsync(id);

            if (country == null)
            {
                throw new NotFoundException(nameof(GetCountries), id);
            }

            await _countriesRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRepository.Exists(id);
        }
    }
}
