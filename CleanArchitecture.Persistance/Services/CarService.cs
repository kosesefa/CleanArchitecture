using AutoMapper;
using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetAllCar;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistance.Context;
using EntityFrameworkCorePagination.Nuget.Pagination;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CleanArchitecture.Persistance.Services
{
    public sealed class CarService : ICarService
    {
        private readonly AppDbContext _context;
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CarService(AppDbContext context, IMapper mapper, ICarRepository carRepository, IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _carRepository = carRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(CreateCarCommand request, CancellationToken cancellationToken)
        {
            Car car=_mapper.Map<Car>(request);
            
            //Bu iki kod bloğu eğer repository pattern kullanmasaydık yazacağımız yöntemi gösteriyor.
            //await _context.Set<Car>().AddAsync(car,cancellationToken);
            //await _context.SaveChangesAsync(cancellationToken);

            await _carRepository.AddAsync(car,cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<PaginationResult<Car>> GetAllAsync(GetAllCarQuery request, CancellationToken cancellationToken)
        {
            PaginationResult<Car> cars =
                await _carRepository
                .GetWhere(p => p.Name.ToLower().Contains(request.search.ToLower()))
                .ToPagedListAsync(request.PageNumber,request.PageSize,cancellationToken);
            return cars;
        }
    }
}
