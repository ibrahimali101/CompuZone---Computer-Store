using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CompUZone.Models;
using MediatR;
using CompuZone.Domain.Entities;
using CompuZone.Domain.Interfaces;

namespace CompuZone.Application.Features.Commands
{
    public class CategoryAddCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CategoryAddCommandHandler : IRequestHandler<CategoryAddCommand, bool>
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryAddCommandHandler(IGenericRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CategoryAddCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            _repository.AddAsync(category);
            var status = await _repository.SaveChangesAsync();

            return status;
        }
    }
}
