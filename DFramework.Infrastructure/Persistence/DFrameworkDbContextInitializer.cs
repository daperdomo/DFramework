using DFramework.Application.Common.Interfaces;
using DFramework.Application.Common.Interfaces.Authentication;
using DFramework.Application.Common.Interfaces.Services;
using DFramework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFramework.Infrastructure.Persistence
{
    public class DFrameworkDbContextInitializer
    {
        private readonly ILogger<DFrameworkDbContextInitializer> _logger;
        private readonly IDFrameworkDbContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IPasswordHasher _passwordHasher;

        public DFrameworkDbContextInitializer(
            ILogger<DFrameworkDbContextInitializer> logger,
            IDFrameworkDbContext context,
            IDateTimeProvider dateTimeProvider,
            IPasswordHasher passwordHasher)
        {
            _logger = logger;
            _context = context;
            _dateTimeProvider = dateTimeProvider;
            _passwordHasher = passwordHasher;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            await TrySeedLocalizationAsync();
            await TrySeedUsersAsync();
        }

        private async Task TrySeedUsersAsync()
        {
            try
            {
                if (!_context.Users.Any())
                {
                    _context.Users.Add(new User
                    {
                        RolId = 1,
                        Username = "master",
                        Password = _passwordHasher.Hash("master"),
                        Active = true,
                        CreatedDate = _dateTimeProvider.Now,
                        FullName = "Administrator",
                        Email = "administrator@mail.com",
                    });
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding users.");
            }
        }

        private async Task TrySeedLocalizationAsync()
        {
            try
            {
                AddSpanishLanguageSeed();
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the localizations.");
            }
        }

        private void AddSpanishLanguageSeed()
        {
            List<LanguageResource> resources = new List<LanguageResource>()
                {
                    new LanguageResource {Name = "login.title", Value = "Login" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "login.text", Value = "Iniciar sesión" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "login.button", Value = "Entrar" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "login.forgotpassword", Value = "Olvidó contraseña" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "login.input.username", Value = "Usuario" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "login.input.username.feedback", Value = "Usuario invalido" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "login.input.password", Value = "Contraseña" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "login.input.password.feedback", Value = "Contraseña invalida" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "headerdropdown.account", Value = "Mi cuenta" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "headerdropdown.logout", Value = "Salir" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "headerdropdown.changepassword", Value = "Cambiar contraseña" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "changepassword.title", Value = "Cambiar contraseña" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "password.invalid", Value = "Contraseña invalida" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "newpassword.invalid", Value = "Contraseña existente" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "confirmedpassword.notmatched", Value = "Contraseñas no coinciden" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "changepassword.input.oldpassword", Value = "Contraseña actual" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "changepassword.input.newpassword", Value = "Contraseña nueva" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "changepassword.input.confirmedpassword", Value = "Confirmar contraseña" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "changepassword.button.save", Value = "Cambiar" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "changepassword.button.close", Value = "Cancelar" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "changepassword.input.oldpassword.feedback", Value = "Contraseña actual invalida" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "changepassword.input.newpassword.feedback", Value = "Contraseña nueva invalida" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "changepassword.input.confirmedpassword.feedback", Value = "Confirmar contraseña invalida" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "header.settings", Value = "Configuraciones" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "header.users", Value = "Usuarios" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "header.dashboard", Value = "Dashboard" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "route.dashboard", Value = "Dashboard" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "route.home", Value = "Inicio" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "route.users", Value = "Usuarios" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "users.title", Value = "Usuarios" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "users.add.button", Value = "Agregar" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "users.table.header.username", Value = "Usuario" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "users.table.header.fullname", Value = "Nombre" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "users.table.header.email", Value = "Correo electrónico" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "users.table.edit.action", Value = "Editar" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "users.table.delete.action", Value = "Borrar" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "existinguser.message", Value = "Usuario existente" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "adduser.title", Value = "Agregar usuario" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "adduser.input.username", Value = "Usuario" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "adduser.input.username.feedback", Value = "Usuario invalido" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "adduser.input.fullname.feedback", Value = "Nombre invalido" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "adduser.input.email.feedback", Value = "Correo electrónico invalido" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "adduser.input.fullname", Value = "Nombre" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "adduser.input.email", Value = "Correo electrónico" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "adduser.button.close", Value = "Cancelar" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "adduser.button.save", Value = "Guardar" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "updateuser.title", Value = "Actualizar usuario" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "updateuser.fullname", Value = "Nombre" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "updateuser.email", Value = "Correo electrónico" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "updateuser.input.username.feedback", Value = "Usuario invalido" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "updateuser.input.fullname.feedback", Value = "Nombre invalido" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "updateuser.input.email.feedback", Value = "Correo electrónico invalido" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "authentication.notvalid", Value = "Usuario o contraseña invalida" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "authentication.user.notfound", Value = "Usuario o contraseña invalida" ,CreatedDate = _dateTimeProvider.Now},
                };
            if (!_context.Languages.Any(m => m.Culture == "es-DO"))
            {
                //Spanish added
                _context.Languages.Add(new Language()
                {
                    Name = "Español DO",
                    Culture = "es-DO",
                    Code = "ES",
                    LanguageResources = resources,
                    CreatedDate = _dateTimeProvider.Now,
                });
            }
            else
            {
                var spanishDo = _context.Languages.Include(m => m.LanguageResources).First(m => m.Culture == "es-DO");
                foreach (var resource in resources)
                {
                    if (!spanishDo.LanguageResources.Any(m => m.Name == resource.Name))
                    {
                        spanishDo.LanguageResources.Add(resource);
                    }
                }
                _context.Languages.Update(spanishDo);
            }
        }
    }
}
