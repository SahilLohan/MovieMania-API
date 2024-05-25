using System;

namespace MovieMania.Models.Request
{
    public class ProducerRequest
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
    }
}
