using Domain.Entities;
using Domain.Interfaces.BuildObjects;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.BuildObjects;

internal class BuildObjectsRepository : IBuildObjectsRepository
{
    private readonly ApplicationDbContext _context;
    public BuildObjectsRepository(ApplicationDbContext context) => _context = context;
    /* Función que crea pacientes masivamente */
    public async Task<bool> CreatePatients(int number)
    {
        var cities = await _context.Cities.ToListAsync();
        for (int i = 0; i < number; i++)
        {
            Random objRan = new Random();
            int randomNum = objRan.Next(1, 5000000);
            var city = cities
                        .OrderBy(c => Guid.NewGuid())
                        .FirstOrDefault();

            var plan = await _context.Plans
                        .OrderBy(c => Guid.NewGuid())
                        .FirstOrDefaultAsync();

            if (city == null || plan == null) continue;

            Patient objPatient = new Patient();
            objPatient.PatientIdSo = objPatient.Id;
            objPatient.Id = Guid.NewGuid();
            objPatient.Identification = objRan.Next(649849222, 1102855250).ToString();
            objPatient.Birthday = DateTime.Now;
            objPatient.BusinessLineId = Guid.Parse("DD44C571-4FA5-4133-AECD-062834C93601");
            objPatient.CityId = city.Id;
            objPatient.Comorbidities = objRan.Next(2, 10);
            objPatient.CellPhone = objRan.Next(649849222, 1102855250).ToString();
            objPatient.Email = $"{objPatient.Identification}@grupoemi.test.co";
            //objPatient.Active = true;
            objPatient.FirstName = $"Patient {randomNum}";
            objPatient.LastName = city.Name;
            objPatient.SecondLastName = plan?.Name;
            objPatient.PlanId = plan?.Id;
            objPatient.PatientStateId = Guid.Parse("FE4D5909-AF92-4324-A347-E8CF5CE3D424");
            await _context.AddAsync(objPatient);
            await _context.SaveChangesAsync();

        }
        return true;
    }
    /* Función que crea usuarios masivamente */
    public async Task<bool> CreateUsers(int number)
    {
        var cities = await _context.Cities.ToListAsync();
        var services = await _context.Services.ToListAsync();
        var userExpires = await _context.UserExpires.ToListAsync();
        for (int i = 0; i < number; i++)
        {
            Random objRan = new Random();
            int randomNumber = objRan.Next(1, 5000000);
            var city = cities
                        .OrderBy(c => Guid.NewGuid())
                        .FirstOrDefault();

            var service = services
                       .OrderBy(c => Guid.NewGuid())
                       .FirstOrDefault();

            var userExpire = userExpires
                       .OrderBy(c => Guid.NewGuid())
                       .FirstOrDefault();

            if (city == null || service == null || userExpire == null) continue;

            User objUser = new();
            objUser.Id = Guid.NewGuid();
            objUser.UserIdSo = objUser.Id;
            objUser.BusinessLineId = Guid.Parse("DD44C571-4FA5-4133-AECD-062834C93601");
            objUser.AvailableAt = null;
            objUser.CityId = city.Id;
            //objHealthCareStaff.Email = $"Dr.{randomNumber}.{city.Name}@grupoemi.com";
            objUser.Loggued = false;
            objUser.Name = $"Dr {randomNumber} - {city.Name} - {service.Name}";
            //objHealthCareStaff.ProcessId = processor.Id;
            //objHealthCareStaff.Active = true;
            objUser.UserStateId = Guid.Parse("49B02DCA-21DB-4BD8-B5E7-65AF72260349");
            objUser.UserName = $"user{randomNumber}";
            objUser.Password = "1234";
            objUser.UserExpireId = userExpire.Id;
            //objHealthCareStaff.Rol = Guid.Parse("D743CAD0-3E44-480D-857B-F04C4A4964A1");

            UserService objUserService = new();
            objUserService.Id = Guid.NewGuid();
            objUserService.ServiceId = service.Id;
            objUserService.UserId = objUser.Id;
            objUserService.ServicePriority = true;
            await _context.AddAsync(objUser);
            await _context.AddAsync(objUserService);
            await _context.SaveChangesAsync();
        }
        return true;


    }
}
