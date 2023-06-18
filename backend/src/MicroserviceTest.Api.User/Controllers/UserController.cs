using MicroserviceTest.Common.Core.Messaging;
using MicroserviceTest.Common.Services;
using MicroserviceTest.Contract.Dtos.User;
using MicroserviceTest.Contract.Events;
using MicroserviceTest.Contract.Model.User;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTest.Api.User.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMessageProducerCoreService messageProducer;
        public UserController(IUserService userService, IMessageProducerCoreService messageProducer)
        {
            this.userService = userService;
            this.messageProducer = messageProducer;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserModel createUserModel)
        {
            var createUserDto = new CreateUserDto(createUserModel.UserName, createUserModel.FirstName, createUserModel.LastName);
            var result = await userService.CreateAsync(createUserDto);

            var userCreatedEvent = new UserCreatedEvent(
                "Ricardo",
                "Medina",
                "richard"
            );

            await messageProducer.PublishAsync("usercreated", userCreatedEvent);

            return result
                ? StatusCode(StatusCodes.Status201Created)
                : StatusCode(StatusCodes.Status500InternalServerError);

        }

        [HttpGet()]
        public async Task<IActionResult> Read()
        {
            var userDtos = await userService
                .ReadAllAsync();

            var userModels = userDtos
                .Select(userDto => new UserModel(userDto.Id, userDto.UserName, userDto.FirstName, userDto.LastName));

            return StatusCode(StatusCodes.Status200OK, userModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(long id)
        {
            var userDto = await userService.ReadAsync(id);

            if (userDto != null)
            {
                var userModel = new UserModel(userDto.Id, userDto.UserName, userDto.FirstName, userDto.LastName);
                return StatusCode(StatusCodes.Status200OK, userModel);
            }

            return StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateUserModel updateUserModel)
        {
            var updateUserDto = new UpdateUserDto(updateUserModel.UserName, updateUserModel.FirstName, updateUserModel.LastName);
            var result = await userService.UpdateAsync(id, updateUserDto);


            return result
                ? StatusCode(StatusCodes.Status200OK)
                : StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await userService.DeleteAsync(id);

            return result
                ? StatusCode(StatusCodes.Status200OK)
                : StatusCode(StatusCodes.Status404NotFound);
        }
    }
}
