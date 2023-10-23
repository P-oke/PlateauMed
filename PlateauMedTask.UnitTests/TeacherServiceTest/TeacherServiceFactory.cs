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

namespace PlateauMedTask.UnitTests.TeacherServiceTest
{
    public class TeacherServiceFactory
    {
        public ApplicationDbContext Context;
        public Mock<UserManager<User>> UserManager = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object,
             null, null, null, null, null, null, null, null);


        public TeacherServiceFactory()
        {
            Context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: "PlateauMed")
              .Options);

            TeacherService = new TeacherService(UserManager.Object, Context);

        }

        public TeacherService TeacherService { get; set; } 

    }
}
