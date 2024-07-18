using DataAccess.DAOs;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    //Padre abstracto de todos los cruds existentes en la arquitectura
    //patron de la fabrica abstracta
    public abstract class CrudFactory
    {
        
        protected SqlDao _dao;

        //contrato de los CRUDs
        //Obliga a deefinir los metodos de create, retreive, update y delete

        public abstract void Create(BaseDTO baseDTO);
        public abstract void Update(BaseDTO baseDTO);
        public abstract void Delete(BaseDTO baseDTO);

        public abstract T Retieve<T>();
        public abstract T RetrieveById<T>(int id);
        public abstract List<T> RetrieveAll<T>();


    }
}
