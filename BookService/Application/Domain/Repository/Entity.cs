using System;

namespace BookService.Application.Domain.Repository
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
