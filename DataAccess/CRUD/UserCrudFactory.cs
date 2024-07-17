using DataAccess.DAOs;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class UserCrudFactory : CrudFactory
    {
  
        public UserCrudFactory()
        {
            _dao = SqlDao.GetInstance();
        }

        public override void Create(BaseDTO baseDTO)
        {
            //convertir el BaseDTO en un usuario
            var user = baseDTO as User;
            //creacion de instructivo para que EL DAO PUEDA EJECUTAR
            var sqlOperation = new SqlOperation { ProcedureName = "CRE_CONDO_USER_PR" };
            sqlOperation.AddVarcharParam("P_CEDULA", user.Identity);
            sqlOperation.AddVarcharParam("P_NOMBRE", user.Name);
            sqlOperation.AddVarcharParam("P_PRIMER_APELLIDO", user.LastName1);
            sqlOperation.AddVarcharParam("P_SEGUNDO_APELLIDO", user.LastName2);
            sqlOperation.AddVarcharParam("P_TELEFONO", user.PhoneNumber);
            sqlOperation.AddVarcharParam("P_CORREO", user.Email);
            sqlOperation.AddIntParam("P_NUM_DPT_VISITADO", user.NumDpt);
            sqlOperation.AddDateTimeParam("P_HORA_ENTRADA", user.Hour);
            _dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDTO)
        {
            var user = baseDTO as User;
            var sqlOperation = new SqlOperation { ProcedureName = "DEL_CONDO_USER_PR" };
            sqlOperation.AddIntParam("P_USER_ID", user.Id);
            _dao.ExecuteProcedure(sqlOperation);
        }


        public override T Retieve<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var userList = new List<T>();

            var sqlOperation = new SqlOperation { ProcedureName = "RET_ALL_CONDO_USER_PR" };

            var results = _dao.ExecuteQueryProcedure(sqlOperation);
            if (results.Count > 0)
            {

                foreach (var row in results)
                {
                    var user = BuildUser(row);
                    userList.Add((T)Convert.ChangeType(user, typeof(T)));
                }
            }

            return userList;
        }



        public override T RetrieveById<T>(int id)
        {
            if (typeof(T) == typeof(User))
            {
                var sqlOperation = new SqlOperation { ProcedureName = "RET_CONDO_USER_BY_ID_PR" };
                sqlOperation.AddIntParam("P_USER_ID", id);

                var listResults = _dao.ExecuteQueryProcedure(sqlOperation);

                if (listResults.Count > 0)
                {
                    var row = listResults[0];
                    var user = BuildUser(row);
                    return (T)Convert.ChangeType(user, typeof(T));
                }
            }
            throw new ArgumentException("Invalid type parameter for RetrieveById");
        }



        public User RetrieveByEmail(User u)
        {
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_CONDO_USER_BY_EMAIL_PR" };

            sqlOperation.AddVarcharParam("P_CORREO", u.Email);

            var listResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (listResults.Count > 0) {

                var row = listResults[0];

                var user = BuildUser(row);
                return user;


            }

            return null;
        }

    

    public override void Update(BaseDTO baseDTO)
    {
        var user = baseDTO as User;
        var sqlOperation = new SqlOperation { ProcedureName = "UPD_CONDO_USER_PR" };
        sqlOperation.AddIntParam("P_USER_ID", user.Id);
        sqlOperation.AddVarcharParam("P_CEDULA", user.Identity);
        sqlOperation.AddVarcharParam("P_NOMBRE", user.Name);
        sqlOperation.AddVarcharParam("P_PRIMER_APELLIDO", user.LastName1);
        sqlOperation.AddVarcharParam("P_SEGUNDO_APELLIDO", user.LastName2);
        sqlOperation.AddVarcharParam("P_TELEFONO", user.PhoneNumber);
        sqlOperation.AddVarcharParam("P_CORREO", user.Email);
        sqlOperation.AddIntParam("P_NUM_DPT_VISITADO", user.NumDpt);
        sqlOperation.AddDateTimeParam("P_HORA_ENTRADA", user.Hour);
        _dao.ExecuteProcedure(sqlOperation);
    }

    private User BuildUser(Dictionary<string, object> row)
    {
        var userToRetun = new User()
        {
            Id = (int)row["ID"],
            Identity = (string)row["Cedula"],
            Name = (string)row["Nombre"],
            LastName1 = (string)row["Primer_Apellido"],
            LastName2 = (string)row["Segundo_Apellido"],
            PhoneNumber = (string)row["Telefono"],
            Email = (string)row["Correo"],           
            NumDpt = (int)row["Num_Dpt_Visitado"],
            Hour = (DateTime)row["Hora_Entrada"]
        };
        return userToRetun;
        }
    }
}
