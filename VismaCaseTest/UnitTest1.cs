using System.Threading;
using System;
using Microsoft.EntityFrameworkCore;
using Xunit;
using VismaCase;
using VismaCase.Services;
using VismaCase.Models;
using Moq;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace VismaCaseTest
{
    public class WebApplicationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;
        public WebApplicationTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public void TestValidators()
        {
            var eValidator = factory.Services.GetService<IEmployeeValidator>();
            var pValidator = factory.Services.GetService<IPositionValidator>();
            var wValidator = factory.Services.GetService<IWorkTaskValidator>();
            Assert.NotNull(eValidator);
            Assert.NotNull(pValidator);
            Assert.NotNull(wValidator);
        }

        [Fact]
        public void TestProviders()
        {
            using (var db = new AppDbContext(Utilities.TestDbContextOptions()))
            {
                var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var eProvider = scope.ServiceProvider.GetRequiredService<IEmployeeProvider>();
                var pProvider = scope.ServiceProvider.GetRequiredService<IEmployeeProvider>();
                var wProvider = scope.ServiceProvider.GetRequiredService<IEmployeeProvider>();
                Assert.NotNull(eProvider);
                Assert.NotNull(pProvider);
                Assert.NotNull(wProvider);
            }
        }

        [Fact]
        // Tester at det er mulig å legge til en Ansatt,
        // en stilling for den ansatte og en oppgave
        // innen perioden for stillingen
        public async void SimpleCase()
        {
            using (var db = new AppDbContext(Utilities.TestDbContextOptions()))
            {
                var eProvider = new EmployeeProvider(db);
                var eValidator = new EmployeeValidator();
                var pProvider = new PositionProvider(db);
                var pValidator = new PositionValidator();
                var wProvider = new WorkTaskProvider(db);
                var wValidator = new WorkTaskValidator();

                var employee = new Employee();
                employee.FirstName = "Edvin";
                employee.LastName = "Grytnes";
                if (eValidator.IsValid(employee).Length == 0)
                {
                    await eProvider.Add(employee);
                }

                var position = new Position();
                position.Name = "Utvikler";
                position.StartTime = new DateTime(2022, 2, 1);
                position.EndTime = new DateTime(2023, 7, 1);
                var pEmployee = await eProvider.GetById(1);
                position.Employee = pEmployee;
                position.EmployeeId = pEmployee.Id;

                if (pValidator.IsValid(position).Length == 0)
                {
                    await pProvider.Add(position);
                }

                var workTask = new WorkTask();
                workTask.Name = "Løs case";
                var wEmployee = await eProvider.GetById(1);
                workTask.Employee = wEmployee;
                workTask.EmployeeId = wEmployee.Id;
                workTask.Date = new DateTime(2022, 2, 17);

                if (wValidator.IsValid(workTask).Length == 0)
                {
                    await wProvider.Add(workTask);
                }

                var retrievedEmp = await eProvider.GetById(1);
                var retrievedPos = await pProvider.GetById(1);
                var retrievedWor = await wProvider.GetById(1);

                Assert.Equal(retrievedEmp.FirstName, "Edvin");
                Assert.Equal(retrievedPos.Name, "Utvikler");
                Assert.Equal(retrievedWor.Name, "Løs case");
            }
        }

        [Fact]
        // Sjekker at en oppgave havner på
        // riktig stilling dersom det er 
        // flere stillinger for den ansatte
        public async void CorrectPosition()
        {
            using (var db = new AppDbContext(Utilities.TestDbContextOptions()))
            {
                var eProvider = new EmployeeProvider(db);
                var eValidator = new EmployeeValidator();
                var pProvider = new PositionProvider(db);
                var pValidator = new PositionValidator();
                var wProvider = new WorkTaskProvider(db);
                var wValidator = new WorkTaskValidator();

                var employee = new Employee();
                employee.FirstName = "Edvin";
                employee.LastName = "Grytnes";
                if (eValidator.IsValid(employee).Length == 0)
                {
                    await eProvider.Add(employee);
                }

                var position = new Position();
                position.Name = "Graduate";
                position.StartTime = new DateTime(2022, 2, 1);
                position.EndTime = new DateTime(2023, 7, 1);
                position.Employee = await eProvider.GetById(1);
                position.EmployeeId = position.Employee.Id;

                if (pValidator.IsValid(position).Length == 0)
                {
                    await pProvider.Add(position);
                }

                var secondPosition = new Position();
                secondPosition.Name = "Junior utvikler";
                secondPosition.StartTime = new DateTime(2023, 7, 2);
                secondPosition.EndTime = new DateTime(2025, 7, 1);
                secondPosition.Employee = await eProvider.GetById(1);
                secondPosition.EmployeeId = secondPosition.Employee.Id;

                if (pValidator.IsValid(secondPosition).Length == 0)
                {
                    await pProvider.Add(secondPosition);
                }

                var workTask = new WorkTask();
                workTask.Name = "Løs case";
                var wEmployee = await eProvider.GetById(1);
                workTask.Employee = wEmployee;
                workTask.EmployeeId = wEmployee.Id;
                workTask.Date = new DateTime(2024, 9, 17);
               
                if (wValidator.IsValid(workTask).Length == 0)
                {
                    await wProvider.Add(workTask);
                }

                var getTask = await wProvider.GetById(1);
                Assert.Equal(getTask.Position.Name, "Junior utvikler");
            }
        }


        [Fact]
        // Sjekker at det kommer exception om man legger til en stilling
        // som starter eller slutter samtidig som en annen stilling
        // eller om en stilling starter før og slutter etter en
        // eksisterende stilling
        public async void Overlap()
        {
            using (var db = new AppDbContext(Utilities.TestDbContextOptions()))
            {
                var eProvider = new EmployeeProvider(db);
                var eValidator = new EmployeeValidator();
                var pProvider = new PositionProvider(db);
                var pValidator = new PositionValidator();
                var wProvider = new WorkTaskProvider(db);
                var wValidator = new WorkTaskValidator();

                var employee = new Employee();
                employee.FirstName = "Edvin";
                employee.LastName = "Grytnes";
                if (eValidator.IsValid(employee).Length == 0)
                {
                    await eProvider.Add(employee);
                }

                var position = new Position();
                position.Name = "Graduate";
                position.StartTime = new DateTime(2022, 2, 1);
                position.EndTime = new DateTime(2024, 7, 1);
                position.Employee = await eProvider.GetById(1);
                position.EmployeeId = position.Employee.Id;

                if (pValidator.IsValid(position).Length == 0)
                {
                    await pProvider.Add(position);
                }

                var secondPosition = new Position();
                secondPosition.Name = "Junior utvikler";
                secondPosition.StartTime = new DateTime(2023, 7, 1);
                secondPosition.EndTime = new DateTime(2025, 7, 1);
                secondPosition.Employee = await eProvider.GetById(1);
                secondPosition.EmployeeId = secondPosition.Employee.Id;

                if (pValidator.IsValid(secondPosition).Length == 0)
                {
                    await Assert.ThrowsAsync<Exception>(async () =>
                    {
                        await pProvider.Add(secondPosition);
                    });
                }

                var thirdPosition = new Position();
                thirdPosition.Name = "Senior utvikler";
                thirdPosition.StartTime = new DateTime(2021, 7, 1);
                thirdPosition.EndTime = new DateTime(2023, 7, 1);
                thirdPosition.Employee = await eProvider.GetById(1);
                thirdPosition.EmployeeId = thirdPosition.Employee.Id;

                if (pValidator.IsValid(thirdPosition).Length == 0)
                {
                    await Assert.ThrowsAsync<Exception>(async () =>
                    {
                        await pProvider.Add(thirdPosition);
                    });
                }

                var fourthPosition = new Position();
                fourthPosition.Name = "Prosjektleder";
                fourthPosition.StartTime = new DateTime(2021, 1, 1);
                fourthPosition.EndTime = new DateTime(2026, 1, 1);
                fourthPosition.Employee = await eProvider.GetById(1);
                fourthPosition.EmployeeId = fourthPosition.Employee.Id;

                if (pValidator.IsValid(fourthPosition).Length == 0)
                {
                    await Assert.ThrowsAsync<Exception>(async () =>
                    {
                        await pProvider.Add(fourthPosition);
                    });
                }
            }

        }

        [Fact]
        // Sjekker at det kommer en exception om man legger
        // til en oppgave utenfor et stillingsforhold
        public async void NoPositionForCase()
        {
            using (var db = new AppDbContext(Utilities.TestDbContextOptions()))
            {
                var eProvider = new EmployeeProvider(db);
                var eValidator = new EmployeeValidator();
                var pProvider = new PositionProvider(db);
                var pValidator = new PositionValidator();
                var wProvider = new WorkTaskProvider(db);
                var wValidator = new WorkTaskValidator();

                var employee = new Employee();
                employee.FirstName = "Edvin";
                employee.LastName = "Grytnes";
                if (eValidator.IsValid(employee).Length == 0)
                {
                    await eProvider.Add(employee);
                }

                var position = new Position();
                position.Name = "Graduate";
                position.StartTime = new DateTime(2022, 2, 1);
                position.EndTime = new DateTime(2023, 7, 1);
                position.Employee = await eProvider.GetById(1);
                position.EmployeeId = position.Employee.Id;

                if (pValidator.IsValid(position).Length == 0)
                {
                    await pProvider.Add(position);
                }

                var secondPosition = new Position();
                secondPosition.Name = "Junior utvikler";
                secondPosition.StartTime = new DateTime(2023, 7, 3);
                secondPosition.EndTime = new DateTime(2025, 7, 1);
                secondPosition.Employee = await eProvider.GetById(1);
                secondPosition.EmployeeId = secondPosition.Employee.Id;

                if (pValidator.IsValid(secondPosition).Length == 0)
                {
                    await pProvider.Add(secondPosition);
                }

                var newTask = new WorkTask();
                newTask.Name = "Case";
                newTask.Employee = await eProvider.GetById(1);
                newTask.EmployeeId = newTask.Employee.Id;
                newTask.Date = new DateTime(2023, 7, 2);

                if (wValidator.IsValid(newTask).Length == 0)
                {
                    await Assert.ThrowsAsync<Exception>(async () =>
                    {
                        await wProvider.Add(newTask);
                    });
                }
            }
        }

        [Fact]
        public async void TestDuplicate() 
        {
            using (var db = new AppDbContext(Utilities.TestDbContextOptions()))
            {
                var eProvider = new EmployeeProvider(db);
                var eValidator = new EmployeeValidator();
                var pProvider = new PositionProvider(db);
                var pValidator = new PositionValidator();
                var wProvider = new WorkTaskProvider(db);
                var wValidator = new WorkTaskValidator();

                var employee = new Employee();
                employee.FirstName = "Edvin";
                employee.LastName = "Grytnes";

                if (eValidator.IsValid(employee).Length == 0)
                {
                    await eProvider.Add(employee);
                }

                var newEmployee = new Employee();
                newEmployee.FirstName = "Edvin";
                newEmployee.LastName = "Grytnes";

                if (eValidator.IsValid(employee).Length == 0)
                {
                    await Assert.ThrowsAsync<Exception>(async () =>
                    {
                        await eProvider.Add(employee);
                    });
                }
            }
        }
    }
}

