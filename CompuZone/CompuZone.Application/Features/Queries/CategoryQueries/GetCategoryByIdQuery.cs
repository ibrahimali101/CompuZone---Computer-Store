using AutoMapper;
using MediatR;
using CompuZone.Application.Exceptions;
using CompuZone.Application.Features.Dtos.Responses.CategoryResponses;
using CompuZone.Domain.Interfaces;
using System.Threading.Tasks;
using CompUZone.Models;

namespace CompuZone.Application.Features.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryReadReponseDto>
    {
        public int Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryReadReponseDto>
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetCategoryByIdQueryHandler(
            IGenericRepository<Category> repository,
            IMapper mapper,
            ICurrentUserService currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<CategoryReadReponseDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            // Note: Ensure your GenericRepository.GetByIDAsync uses the primary key correctly. 
            // If your Generic Repo uses 'ID' but this class uses 'CategoryId', 
            // you might need to override the method or ensure BaseEntity maps them.
            var category = await _repository.GetByIDAsync(request.Id);

            if (category == null)
                throw new NotFoundException($"Category with ID {request.Id} was not found.");
            return _mapper.Map<CategoryReadReponseDto>(category);
        }
    }
}