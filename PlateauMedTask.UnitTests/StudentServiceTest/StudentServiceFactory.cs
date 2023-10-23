using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using PlateauMedTask.Domain.Entities;
using PlateauMedTask.Infrastructure.Context;
using PlateauMedTask.Infrastructure.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.UnitTests.StudentServiceTest
{
    public class StudentServiceFactory
    {
        public ApplicationDbContext Context;
        public Mock<UserManager<User>> UserManager = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object,
             null, null, null, null, null, null, null, null);


        public StudentServiceFactory()
        {
            Context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: "PlateauMed")
              .Options);

            StudentService = new StudentService(UserManager.Object, Context);

        }

        public StudentService StudentService { get; set; }

    }
}
