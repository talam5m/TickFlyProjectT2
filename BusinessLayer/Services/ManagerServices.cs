using BusinessLayer.DTOs;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class ManagerServices : IManagerServices
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;

        public ManagerServices(ITicketRepository ticketRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task<ResponseDto<IEnumerable<SupportTeamDto>>> GetSupportTeamMembersAsync()
        {
            var supports = await _userRepository.GetAllTeamSupportAsync();
            return new ResponseDto<IEnumerable<SupportTeamDto>>
            {
                IsSuccess = true,
                StatusCode = 200,
                Result = supports
                .Select(u => new SupportTeamDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    MobileNumber = u.MobileNumber,
                    Address = u.Address,
                })
            };

        }

        public async Task<ResponseDto<IEnumerable<ExternalClientDto>>> GetExternalClientAsync()
        {
            var clients = await _userRepository.GetAllExternalClientsAsync();

            var externalClients = clients
                .Select(u => new ExternalClientDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    MobileNumber = u.MobileNumber,
                    Address = u.Address,
                    Tickets = u.Tickets.Select(a => new TicketDTO
                    {
                        Description = a.Description,
                        Id = a.Id,
                        Priority = a.Priority,
                        Status = a.Status,
                        ProductId = a.ProductId,
                        UserId = a.UserId,

                    }).ToList()
                });

            return new ResponseDto<IEnumerable<ExternalClientDto>>
            {
                IsSuccess = true,
                StatusCode = 200,
                Result = externalClients
            };
        }

        public async Task<ResponseDto<bool>> UpdateExternalClientAsync(Guid id, UpdateExternalClientDto dto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return new ResponseDto<bool>
                {
                    IsSuccess = false,
                    StatusCode = 404,
                    ErrorMessage = "User not found."
                };
            }

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.MobileNumber = dto.MobileNumber;
            user.Address = dto.Address;
            user.IsActive = dto.IsActive;

            await _userRepository.UpdateUserAsync(user);

            return new ResponseDto<bool>
            {
                IsSuccess = true,
                StatusCode = 200,
                Result = true
            };
        }

        public async Task<ResponseDto<bool>> UpdateSupportEmployeeAsync(Guid id, UpdateSupportEmployeeDto dto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return new ResponseDto<bool>
                {
                    IsSuccess = false,
                    StatusCode = 404,
                    ErrorMessage = "User not found."
                };
            }

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.MobileNumber = dto.MobileNumber;
            user.Address = dto.Address;
            user.IsActive = dto.IsActive;

            await _userRepository.UpdateUserAsync(user);

            return new ResponseDto<bool>
            {
                IsSuccess = true,
                StatusCode = 200,
                Result = true
            };
        }

        public async Task<ResponseDto<bool>> AssignTicketAsync(AssignTicketDto dto)
        {
            var result = await _ticketRepository.AssignTicketAsync(dto.TicketId, dto.SupportEmployeeId);

            return new ResponseDto<bool>
            {
                IsSuccess = result,
                StatusCode = result ? 200 : 500,
                Result = result,
                ErrorMessage = result ? null : "Failed to assign ticket."
            };

        }

        public async Task<ResponseDto<bool>> ChangeUserStatusAsync(Guid id, ChangeUserStatusDto dto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return new ResponseDto<bool>
                {
                    IsSuccess = false,
                    StatusCode = 404,
                    ErrorMessage = "User not found."
                };
            }

            user.IsActive = dto.IsActive;

            await _userRepository.UpdateUserAsync(user);

            return new ResponseDto<bool>
            {
                IsSuccess = true,
                StatusCode = 200,
                Result = user.IsActive
            };
        }

        public async Task<ResponseDto<IEnumerable<TicketDTO>>> GetTicketsAsync(TicketFilterDto filterDto)
        {
            var query = _ticketRepository.GetTicketsAsQueryable();

            if (filterDto.Status.HasValue)
            {
                query = query.Where(t => t.Status == filterDto.Status);
            }

            if (filterDto.AssignedToId.HasValue)
            {
                query = query.Where(t => t.AssignedTo == filterDto.AssignedToId.Value);
            }

            if (filterDto.ClientId.HasValue)
            {
                query = query.Where(t => t.UserId == filterDto.ClientId.Value);
            }


            var tickets = await query.ToListAsync();

            var ticketDtos = tickets.Select(t => new TicketDTO
            {
                Id = t.Id,
                UserId = t.UserId,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                ProductId = t.ProductId
            });

            return new ResponseDto<IEnumerable<TicketDTO>>
            {
                IsSuccess = true,
                StatusCode = 200,
                Result = ticketDtos
            };
        }
        public async Task<ResponseDto<IEnumerable<TicketStatusCountDto>>> GetTicketStatusCountsAsync()
        {
            var counts = await _ticketRepository.GetTicketsAsQueryable()
                .GroupBy(t => t.Status)
                .Select(g => new TicketStatusCountDto
                {
                    Status = g.Key.ToString(),
                    Count = g.Count()
                }).ToListAsync();

            return new ResponseDto<IEnumerable<TicketStatusCountDto>>
            {
                IsSuccess = true,
                StatusCode = 200,
                Result = counts
            };
        }

        public async Task<ResponseDto<IEnumerable<EmployeeProductivityDto>>> GetMostProductiveEmployeesAsync()
        {
            var ticketData = await _ticketRepository.GetTicketsAsQueryable()
                .Where(t => t.Status == DataAccessLayer.Models.Enum.Status.Closed)
                .GroupBy(t => t.AssignedTo)
                .Select(g => new
                {
                    UserId = g.Key,
                    TicketCount = g.Count()
                })
                .Join(_userRepository.GetAllUsersAsQueryable(),
                      ticket => ticket.UserId,
                      user => user.Id,
                      (ticket, user) => new EmployeeProductivityDto
                      {
                          EmployeeName = $"{user.FirstName} {user.LastName}",
                          TicketCount = ticket.TicketCount
                      })
                .ToListAsync();

            return new ResponseDto<IEnumerable<EmployeeProductivityDto>>
            {
                IsSuccess = true,
                StatusCode = 200,
                Result = ticketData
            };
        }


    }
}