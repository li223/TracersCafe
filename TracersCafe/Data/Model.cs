using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TracersCafe.Data
{
    public class Model : DbContext
    {
        public DbSet<PersonInformation> Information { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseSqlite("Data Source=tracersdb.db");
        }
    }

    public sealed class PersonInformation : PersonInformationBase
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonIgnore]
        public string FullAddress
        {
            get
            {
                return string.Concat(this.AddressLineOne, "\n", this.AddressLineTwo, "\n", this.AddressLineThree, "\n", this.AddressLineFour);
            }
        }
    }

    public class PersonInformationBase
    {
        [JsonProperty("Title"), Required]
        public string Title { get; set; }

        [JsonProperty("Firstname"), Required]
        public string Firstname { get; set; }

        [JsonProperty("Surname"), Required]
        public string Surname { get; set; }

        #region Why

        [JsonProperty("AddressLineOne"), Required]
        public string AddressLineOne { get; set; }

        [JsonProperty("AddressLineTwo"), Required]
        public string AddressLineTwo { get; set; }

        [JsonProperty("AddressLineThree")]
        public string AddressLineThree { get; set; }

        [JsonProperty("AddressLineFour")]
        public string AddressLineFour { get; set; }

        #endregion

        [JsonProperty("Postcode"), Required]
        public string Postcode { get; set; }

        [JsonProperty("Telephone"), Required]
        public ulong Telephone { get; set; }

        [JsonProperty("Age"), Required]
        public byte Age { get; set; }
    }
}