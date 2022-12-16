using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(new Hotel
            {
                Id = 1,
                Name = "Sandals Resort",
                Address = "Nigril",
                CountryId = 1,
                Rating = 4.5
            },
             new Hotel
             {
                 Id = 2,
                 Name = "Secrets Resort",
                 Address = "Cab",
                 CountryId = 2,
                 Rating = 4.7
             },
            new Hotel
            {
                Id = 3,
                Name = "Sandals Resort",
                Address = "Punta Cana",
                CountryId = 3,
                Rating = 4.9
            });
        }

    }
}
