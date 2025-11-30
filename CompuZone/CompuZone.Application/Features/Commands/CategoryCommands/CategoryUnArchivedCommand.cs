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
    public class CategoryUnArchivedCommand : IRequest<bool>
    {
        public int ID { get; set; }
    }
    public class CategoryUnArchivedCommandHandler : IRequestHandler<CategoryUnArchivedCommand, bool>
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public CategoryUnArchivedCommandHandler(IGenericRepository<Category> repository, IMapper mapper
            , ICurrentUserService currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
        }
        public async Task<bool> Handle(CategoryUnArchivedCommand request, CancellationToken cancellationToken)
        {
            var category =  await _repository.GetByIDAsync(request.ID);

            if (category == null)
                throw new NotFoundException($"Category with ID {request.ID} was not found.");

            _repository.UnArchivedAsync(category);
            var status = await _repository.SaveChangesAsync();

            return status;
        }
    }
}
