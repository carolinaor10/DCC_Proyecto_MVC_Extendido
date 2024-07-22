using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoreApp
{
    public class UserManager
    {
        public void Create(User user)
        {
            var uc = new UserCrudFactory();
            uc.Create(user);
        }

        public void Update(User user)
        {
            var uc = new UserCrudFactory();
            uc.Update(user);
        }

        public void Delete(User user)
        {
            var uc = new UserCrudFactory();
            uc.Delete(user);
        }

        public User RetrieveUserByEmail(User u)
        {
            var uc = new UserCrudFactory();
            var user = uc.RetrieveByEmail(u);
            return user;
        }

        public User RetrieveUserById(User u)
        {
            var uc = new UserCrudFactory();
            return uc.RetrieveById<User>(u.Id);
        }

        public List<User> RetrieveAll() { 
            var uc = new UserCrudFactory();
            return uc.RetrieveAll<User>();
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool PasswordOK(string password, string email, int id)
        {
            //Validamos que la contraseña sea la correcta parar el usuario.

            User user = RetrieveUserById(new User { Id = id }) as User; //Obtenemos el usuario por el id.


            if (user.Password == password && user.Email == email)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
