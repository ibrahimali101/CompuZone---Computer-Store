using CompuZone.Domain.Interfaces;
using CompUZone.Models; // Ensure this matches your Category entity namespace

namespace CompuZone.Domain.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        void Add(Category category);
        void Update(Category category);
        void Archive(Category category);
        void Unarchive(Category category);
    }
}