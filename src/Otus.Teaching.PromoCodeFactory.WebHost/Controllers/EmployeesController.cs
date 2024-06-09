using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    // [Route("api/v1/Employees")]  // непосредственное указание контроллера
    public class EmployeesController
        : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Role> _roleRepository;
        public EmployeesController(IRepository<Employee> employeeRepository,IRepository<Role> roleRepository)
        {
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;


        }

        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var employeesModelList = employees.Select(x =>
                new EmployeeShortResponse()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FullName,
                }).ToList();

            return employeesModelList;
        }

        /// <summary>
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            IEnumerable<Role> allRoles = await _roleRepository.GetAllAsync();

           
           // List<Role> userRoles=employee.RoleGuids.Select(x => { Role role = allRoles.First(y => y.Id == x);  return role; }).ToList();

              var employeeModel = new EmployeeResponse()
            {
            
                Id = employee.Id,
                Email = employee.Email,
                Roles = employee.RoleGuids.Select(x => { Role role = allRoles.First(y => y.Id == x); return role; }).ToList() ,
                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return employeeModel;
        }

        // POST     CREATE
        // GET      READ
        // UPDATE   PUT
        // DELETE   DELETE


       /// <summary>
       /// Создание нового сотрудника
       /// </summary>
       /// <param name="employee"></param>
       /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployeeAsync(EmployeeBaseData employeeBase)
        {
            //  if (employee == null) { employee = new EmployeeBaseData { FirstName = "John", LastName = "Doe",  Email = "user@mail.ru" }; }
            Employee employeeModel;
            // = new Employee() { FirstName = employeeBase.FirstName, LastName = employeeBase.LastName, Email = employeeBase.Email };
            var employees = await _employeeRepository.GetAllAsync();
            // ищем совпадение с уже существующими данными сотрудников
            employeeModel=employees.FirstOrDefault(x => { return (x.FirstName == employeeBase.FirstName && x.LastName == employeeBase.LastName && x.Email == employeeBase.Email);  } );

            if ( employeeModel == null ) // не найдено совпадений
            {  // создаем новый элемент коллекции
                
                var roles = await _roleRepository.GetAllAsync(); //список всех ролей

              employeeModel = new Employee() 
              {   FirstName = employeeBase.FirstName, 
                  LastName = employeeBase.LastName, 
                  Email = employeeBase.Email , 
                  AppliedPromocodesCount=0,
                  RoleGuids = new List<Guid>()
                {
                    roles.FirstOrDefault(x => x.Name == "PartnerManager").Id  // Роль по-умолчанию
                }
              };

                employeeModel= await _employeeRepository.CreateAsync(employeeModel);
                
                return Ok($"Имя:{employeeModel.FirstName} Фамилия:{employeeModel.LastName} Электронная почта: {employeeModel.Email} Id: {employeeModel.Id}");
            }

            return Ok($"Такой сотрудник уже существует.");

            
        }

        /// <summary>
        /// Обновление данных сотрудника
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(EmployeeReqUpdateData updEmployee) 
        {

            Employee employee,modifyEmployee;
            // = new Employee() { FirstName = employeeBase.FirstName, LastName = employeeBase.LastName, Email = employeeBase.Email };
            var employees = await _employeeRepository.GetAllAsync();
            // ищем совпадение с уже существующими данными сотрудников
            employee = employees.FirstOrDefault(x => { return (x.Id== updEmployee.Id); });

            if (employee == null) // не найдено совпадений
            {  // создаем новый элемент коллекции
                return Ok($"Такой сотрудник не существует.");
            }

            modifyEmployee = new Employee()
            {   Id= employee.Id,
                FirstName = updEmployee.FirstName,
                LastName = updEmployee.LastName,
                Email = updEmployee.Email,
                
                RoleGuids = updEmployee.RoleGuids
            };

           await _employeeRepository.UpdateAsync(modifyEmployee);

            return Ok("Данные обновлены");

        }
        /// <summary>
        /// Удаление сотрудника по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult DeleteEmployee(Guid id) 
        {
            _employeeRepository.DeleteAsync(id);

            return Ok($"Удалён пользователь с ID {id.ToString()}");

        }

    }
}