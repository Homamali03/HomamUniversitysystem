using Hope.DomainEntities.DBEntities;
using Hope.Repositories;
using Hope.Repositories.IRepository;
using Hope.Repositories.Repository;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, ModuleRepository>();
builder.Services.AddScoped<INationalityRepository, NationalityRepository>();
builder.Services.AddScoped<IMajorRepository, MajorRepository>();
builder.Services.AddScoped<IStudyTypeRepository, StudyTypeRepository>();
builder.Services.AddScoped<ITawjihiCertificateRepository, TawjihiCertificateRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
builder.Services.AddScoped<IModuleRoleRepository, ModuleRoleRepository>();
builder.Services.AddScoped<IAssignUserToRoleRepository, AssignUserToRoleRepository>();
builder.Services.AddScoped<IModulesRepository, ModulesRepository>();
builder.Services.AddScoped<ISectionRepository, SectionRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
