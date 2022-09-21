using DapperExample.DataAccess.Repositories;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExample.DataAccess.Jobs
{
    public class GetAllJob : IJob
    {
        private readonly IUserRepository _userRepository;

        public GetAllJob(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var users = await _userRepository.GetAll();

            Console.WriteLine(users);
        }
    }
}
