using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CompuZone.Application.Exceptions;
using CompuZone.Domain.Entities;
using CompuZone.Domain.Interfaces;
using CompUZone.Models;
using MediatR;

namespace CompuZone.Application.Features.Commands.CategoryCommands
{
    public class CategoryUpdateCommand : IRequest<bool>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CategoryUpdateCommandHandler : IRequestHandler<CategoryUpdateCommand, bool>
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public CategoryUpdateCommandHandler(IGenericRepository<Category> repository, IMapper mapper
            , ICurrentUserService currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
        }
        public async Task<bool> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIDAsync(request.ID);

            if (category == null)
                throw new NotFoundException($"Category with ID {request.ID} was not found.");

            var categoryEntity =  _mapper.Map(request, category);
            _repository.UpdateAsync(categoryEntity);
            var status = await _repository.SaveChangesAsync();
            return status;
        }
    }
}
