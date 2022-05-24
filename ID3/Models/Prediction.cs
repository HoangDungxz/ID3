namespace ID3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prediction")]
    public partial class Prediction 
    {
        public int Id { get; set; }

        public int PrecipitationId { get; set; }

        public int HumidityId { get; set; }

        public int TemperatureId { get; set; }

        public int WeatherId { get; set; }

        public int WindId { get; set; }

        public virtual Humididity Humididity { get; set; }

        public virtual Precipitation Precipitation { get; set; }

        public virtual Temperature Temperature { get; set; }

        public virtual Weather Weather { get; set; }

        public virtual Wind Wind { get; set; }
    }
}
