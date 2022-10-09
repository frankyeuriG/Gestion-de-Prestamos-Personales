using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using GestionPrestamos2022.DAL;
using GestionPrestamos2022.Models;

namespace GestionPrestamos2022.BLL
{
    public class PAgosBLL
    {
        private Contexto _contexto;

        public PAgosBLL(Contexto contexto)
        {
            _contexto = contexto;
        }
        public bool Existe(int pagosId)
        {
            return _contexto.Pagos.Any(o => o.PagosId == pagosId);
        }
        private bool Insertar(Pagos pagos)
        {
            _contexto.Pagos.Add(pagos);

        foreach (var item in pagos.PagosDetalle)
        {
            var pago = _contexto.Pagos.Find(item.PagosId);

            pago.Monto += item.ValorPagado;

        }

        var insertados = _contexto.SaveChanges();

        return insertados > 0;
        }
        private bool Modificar(Pagos pagos)
        {

            _contexto.Database.ExecuteSqlRaw($"Delete FROM PagosDetalle Where PagosId = {pagos.PagosId}");

            foreach (var item in pagos.PagosDetalle)
            {
                _contexto.Entry(item).State = EntityState.Added;
            }
            _contexto.Entry(pagos).State = EntityState.Modified;
            return _contexto.SaveChanges() > 0;

        }
        public bool Guardar(Pagos pagos)
        {

            if (!Existe(pagos.PagosId))
                return this.Insertar(pagos);
            else
                return this.Modificar(pagos);
        }
        

        public bool Eliminar(Pagos pagos)
        {
            _contexto.Entry(pagos).State = EntityState.Deleted;
            return _contexto.SaveChanges() > 0;
        }
        public Pagos? Buscar(int pagosId)
        {
            return _contexto.Pagos
            .Where(o => o.PagosId == pagosId)
            .Include(o => o.PagosDetalle)
            .AsNoTracking()
            .SingleOrDefault();
        }
        public List<Pagos> GetList(Expression<Func<Pagos, bool>> Criterio)
        {
            return _contexto.Pagos
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
        }

        


    }

}