﻿namespace Fabrica.Web.Models
{
    using Fabrica.Infrastructure.Mapping;
    using Fabrica.Services.Models;
    using System.ComponentModel.DataAnnotations;

    public class PropEditViewModel : IMapWith<PropServiceModel>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Hashtags { get; set; }

        [Required]
        public string Description { get; set; }

    }
}