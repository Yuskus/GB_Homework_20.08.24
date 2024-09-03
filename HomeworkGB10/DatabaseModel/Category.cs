﻿namespace HomeworkGB10.DatabaseModel
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; } = [];
    }
}