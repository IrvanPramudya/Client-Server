using API.Contracts;
using API.DTOs.Rooms;
using FluentValidation;

namespace API.Utilities.Validations.Rooms
{
    public class InsertRoomValidation:AbstractValidator<NewRoomDto>
    {
        private readonly IRoomRepository _repository;

        public InsertRoomValidation(IRoomRepository repository)
        {
            _repository = repository;
            RuleFor(room => room.Name)
                .NotEmpty().WithMessage("Name can not be Null")
                .Must(IsDuplicateValue).WithMessage("Name Already Saved");
            RuleFor(room => room.Floor)
                .NotEmpty().WithMessage("Floor can not be Null");
            RuleFor(room => room.Capacity)
                .NotEmpty().WithMessage("Capacity can not be Null");
        }
        private bool IsDuplicateValue(string arg)
        {
            return _repository.IsNotExist(arg);
        }
    }
}
