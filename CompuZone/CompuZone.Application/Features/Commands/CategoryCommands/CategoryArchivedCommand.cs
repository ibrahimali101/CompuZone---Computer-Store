using AutoMapper;
using MediatR;

using CompuZone.Domain.Entities;
using CompuZone.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompUZone.Models;
using CompuZone.Application.Exceptions;

namespace CompuZone.Application.Features.Commands.CategoryCommands
{
    public class CategoryArchivedCommand : IRequest<bool>
    {
        public int ID { get; set; }
    }
    public class CategoryArchivedCommandHandler : IRequestHandler<CategoryArchivedCommand, bool>
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public CategoryArchivedCommandHandler(IGenericRepository<Category> repository, IMapper mapper ,ICurrentUserService currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
        }
        public async Task<bool> Handle(CategoryArchivedCommand request, CancellationToken cancellationToken)
        {
            var category =  await _repository.GetByIDAsync(request.ID);

            if (category == null)
                throw new NotFoundException($"Category with ID {request.ID} was not found.");

            _repository.ArchivedAsync(category);
            var status = await _repository.SaveChangesAsync();

            return status;
        }
    }

}
