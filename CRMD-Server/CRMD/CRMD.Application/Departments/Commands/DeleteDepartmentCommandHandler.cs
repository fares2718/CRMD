
namespace CRMD.Application.Departments.Commands
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, ErrorOr<Deleted>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }


        public async Task<ErrorOr<Deleted>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
                return Error.Validation();

            try
            {
                await _departmentRepository.DeleteDepartmentAsync(request.Id);
                return Result.Deleted;
            }
            catch (Exception ex)
            {
                if (ex.Equals(ErrorType.NotFound))
                    return Error.NotFound();
                return Error.Failure();
            }


        }
    }
}