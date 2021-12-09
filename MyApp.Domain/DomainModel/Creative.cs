namespace MyApp.Domain;
using Swashbuckle.AspNetCore.Annotations;

    public abstract class Creative
    {
        [SwaggerSchema(ReadOnly =true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }