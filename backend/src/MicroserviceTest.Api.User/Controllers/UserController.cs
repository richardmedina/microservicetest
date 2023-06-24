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
            var createUserDto = new CreateUserDto(createUserModel.UserName, createUserModel.Password);
            var result = await userService.CreateAsync(createUserDto);

            var userCreatedEvent = new UserCreatedEvent(
                "UserId",
                "richard",
                "password"
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
                .Select(userDto => new UserModel(userDto.Id, userDto.UserName, userDto.Password));

            return StatusCode(StatusCodes.Status200OK, userModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(string id)
        {
            var userDto = await userService.ReadAsync(id);

            if (userDto != null)
            {
                var userModel = new UserModel(userDto.Id, userDto.UserName, userDto.Password);
                return StatusCode(StatusCodes.Status200OK, userModel);
            }

            return StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody]UpdateUserModel updateUserModel)
        {
            var updateUserDto = new UpdateUserDto(id, updateUserModel.UserName, updateUserModel.Password);
            var result = await userService.UpdateAsync(updateUserDto);


            return result
                ? StatusCode(StatusCodes.Status200OK)
                : StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await userService.DeleteAsync(id);

            return result
                ? StatusCode(StatusCodes.Status200OK)
                : StatusCode(StatusCodes.Status404NotFound);
        }
    }
}
