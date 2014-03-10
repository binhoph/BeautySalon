using CamadaDados.DbContextClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace CamadaDados.Cliente
{
    public class Cliente : IDisposable
    {
        BeautySalonDbContext db = new BeautySalonDbContext();

        /// <summary>
        /// Método para inserir um novo Cliente usando EntityState
        /// </summary>
        /// <param name="objCliente"></param>
        public void InsertOrUpdate(ClienteModel objCliente)
        {
            try
            {
                db.Entry(objCliente).State = objCliente.idCliente == 0 ? EntityState.Added : EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Create(ClienteModel obj)
        {
            try
            {
                db.Clientes.Add(obj);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Error ao cadastrar cliente");
            }
        }

        public void Delete(int id)
        {
            try
            {
                var objCliente = db.Clientes.Find(id);
                db.Clientes.Remove(objCliente);
                db.SaveChanges();
            }

            catch (Exception)
            {
                throw new Exception("Error ao excluir o Cliente");
            }
        }

        public void Update(int id)
        {
            try
            {
                var objCliente = db.Clientes.Find(id);
                db.Entry(objCliente).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error ao atualizar Cliente" + ex.Message);
            }
        }

        public ClienteModel PesquisarClientePorId(int id)
        {
            try
            {
                return db.Clientes.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar os dados" + ex.Message);
            }
        }

        public IQueryable<ClienteModel> PesquisarCliente(int id)
        {
            try
            {
                return db.Clientes;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar os dados" + ex.Message);
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
