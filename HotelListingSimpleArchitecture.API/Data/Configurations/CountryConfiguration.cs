using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
            new Country
            {
                Id = 1,
                Name = "Wakanda",
                ShortName = "WK"
            },
            new Country
            {
                Id = 2,
                Name = "Mexico",
                ShortName = "MX"
            },
            new Country
            {
                Id = 3,
                Name = "Dominica Republic",
                ShortName = "DR"
            });
        }
    }
}
