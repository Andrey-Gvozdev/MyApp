﻿namespace MyApp.Domain;
using Swashbuckle.AspNetCore.Annotations;

    public class Creative
    {
        [SwaggerSchema(ReadOnly =true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }