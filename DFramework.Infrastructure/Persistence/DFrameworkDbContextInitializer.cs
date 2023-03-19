using DFramework.Application.Common.Interfaces;
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

        public DFrameworkDbContextInitializer(
            ILogger<DFrameworkDbContextInitializer> logger,
            IDFrameworkDbContext context,
            IDateTimeProvider dateTimeProvider)
        {
            _logger = logger;
            _context = context;
            _dateTimeProvider = dateTimeProvider;
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
                    new LanguageResource {Name = "login.input.password", Value = "Contraseña" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "headerdropdown.account", Value = "Mi cuenta" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "headerdropdown.logout", Value = "Salir" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "header.settings", Value = "Configuraciones" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "header.users", Value = "Usuarios" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "header.dashboard", Value = "Dashboard" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "route.dashboard", Value = "Dashboard" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "route.home", Value = "Inicio" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "users.title", Value = "Usuarios" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "users.add.button", Value = "Agregar" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "users.table.header.username", Value = "Usuario" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "users.table.header.fullname", Value = "Nombre" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "users.table.header.email", Value = "Correo electrónico" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "users.table.edit.action", Value = "Editar" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "users.table.delete.action", Value = "Borrar" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "adduser.title", Value = "Agregar usuario" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "adduser.input.username", Value = "Usuario" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "adduser.input.fullname", Value = "Nombre" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "adduser.input.email", Value = "Correo electrónico" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "adduser.button.close", Value = "Cancelar" ,CreatedDate = _dateTimeProvider.Now},
                    new LanguageResource {Name = "adduser.button.save", Value = "Guardar" ,CreatedDate = _dateTimeProvider.Now},
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
