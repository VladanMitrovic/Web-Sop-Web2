using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web2projekat.Models;

namespace web2projekat.Infrastructure.Configurations
{
    public class KorisnikConfigurations : IEntityTypeConfiguration<Korisnik>
    {
        public void Configure(EntityTypeBuilder<Korisnik> builder)
        {
            throw new NotImplementedException();
        }
    }
}
