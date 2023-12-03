using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using testTask.Data;
using Microsoft.EntityFrameworkCore;

namespace testTask.Models
{

    /// <summary>
    ///  Модель для описания таблицы Employees
    /// </summary>

    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Surname { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public string Phone { get; set; }

        public byte[] Avatar { get; set; }

        [NotMapped]
        public IFormFile AvatarFile { get; set; }

        /// <summary>
        ///  Метод для считывания полученного аватара, либо установки стандартного. Получения поля FullName.
        /// </summary>

        public void Prepare(IWebHostEnvironment appEnvironment, MVCDemoDbContext mvcDemoDbContext)
        {

            var oldEmployee = mvcDemoDbContext.Employee.AsNoTracking().FirstOrDefault(x => x.Id == Id);

            if(AvatarFile != null)
            {
                byte[] avatar = null;

                using (var avatarByteReader = new BinaryReader(AvatarFile.OpenReadStream()))
                {
                    avatar = avatarByteReader.ReadBytes((int)AvatarFile.Length);
                }

                Avatar = avatar;
            }
            else if(oldEmployee?.Avatar == null)
            {
                string standartAvatarPath = Path.Combine(appEnvironment.WebRootPath, "Files", "standart_avatar.jpg");
                Avatar = System.IO.File.ReadAllBytes(standartAvatarPath);
            } else
            {
                Avatar = oldEmployee.Avatar;
            }

            FullName = $"{LastName} {FirstName} {Surname}";
        }

        /// <summary>
        ///  Валидация модели
        /// </summary>

        public string Validate()
        {

            if (LastName == null)
            {
                return "Введите фамилию";
            }

            if (FirstName == null) 
            {
                return "Введите имя";
            }

            if(Surname == null)
            {
                return "Введите отчество";
            }

            if(Phone == null || Phone == "+7(___)___-__-__")
            {
                return "Введите телефон";
            }

            if(DepartmentId == null)
            {
                return "Укажите отдел";
            }

            if(AvatarFile != null)
            {
                string[] permittedExtensions = { ".jpg", ".png", ".jpeg", ".webp" };

                string fileName = AvatarFile?.FileName;

                if (!permittedExtensions.Contains(fileName.Substring(fileName.LastIndexOf('.'))))
                {
                    return "Неправильный формат файла";
                }

                if (AvatarFile != null && AvatarFile.Length > 3145728)
                {
                    return "Слишком большой размер файла";
                }
            }

            return "";

        }
    }

}
