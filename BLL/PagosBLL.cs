using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using GestionPrestamos2022.DAL;
using GestionPrestamos2022.Models;

namespace GestionPrestamos2022.BLL
{
    public class PagosBLL
    {
        private Contexto _contexto;

        public PagosBLL(Contexto contexto)
        {
            _contexto = contexto;
        }
        public bool Existe(int pagosId)
        {
            return _contexto.Pagos.Any(o => o.PagoId == pagosId);
        }
        private bool Insertar(Pagos pagos)
        {
            _contexto.Pagos.Add(pagos);

            foreach (var item in pagos.PagosDetalle)
            {
                var prestamo = _contexto.Prestamos.Find(item.PrestamoId);
                prestamo.Balance -= item.ValorPagado;

                pagos.Monto += item.ValorPagado;

            }
            var persona = _contexto.Personas.Find(pagos.PersonaId);
            persona.Balance -= pagos.Monto;

            var insertados = _contexto.SaveChanges();

            return insertados > 0;
        }
        private bool Modificar(Pagos pagoActual)
        {
            var pagoAnterior = _contexto.Pagos
                 .Where(p => p.PagoId == pagoActual.PagoId)
                 .AsNoTracking()
                 .SingleOrDefault();

            var PersonaAnteriro = _contexto.Personas.Find(pagoAnterior.PersonaId);
            PersonaAnteriro.Balance += pagoAnterior.Monto;

            foreach (var item in pagoAnterior.PagosDetalle)
            {
                var prestamos = _contexto.Prestamos.Find(item.PrestamoId);
                prestamos.Balance += pagoAnterior.Monto;

                pagoAnterior.Monto -= item.ValorPagado;

            }
            _contexto.Database.ExecuteSqlRaw($"Delete FROM PagosDetalle Where PagoId = {pagoActual.PagoId}");

            foreach (var item in pagoActual.PagosDetalle)
            {
                _contexto.Entry(item).State = EntityState.Added;
                pagoActual.Monto = item.ValorPagado;

            }
            _contexto.Entry(pagoActual).State = EntityState.Modified;

            var personaActual = _contexto.Personas.Find(pagoActual.PersonaId);
            personaActual.Balance -= pagoActual.Monto;

            return _contexto.SaveChanges() > 0;

        }
        public bool Guardar(Pagos pagos)
        {

            if (!Existe(pagos.PagoId))
                return this.Insertar(pagos);
            else
                return this.Modificar(pagos);
        }


        public bool Eliminar(Pagos pagos)
        {

            var persona = _contexto.Personas.Find(pagos.PersonaId);
            persona.Balance += pagos.Monto;

            foreach (var item in pagos.PagosDetalle)
            {
                var prestamo = _contexto.Prestamos.Find(item.PrestamoId);
                prestamo.Balance += item.ValorPagado;

                pagos.Monto -= item.ValorPagado;
            }

            _contexto.Entry(pagos).State = EntityState.Deleted;

            return _contexto.SaveChanges() > 0;
        }
        public Pagos? Buscar(int pagoId)
        {
            return _contexto.Pagos
            .Where(o => o.PagoId == pagoId)
            .Include(o => o.PagosDetalle)
            .AsNoTracking()
            .SingleOrDefault();
        }

        public List<Pagos> BuscarP(DateTime fecha)
        {
            var fechas = _contexto.Pagos
             .Where(f => f.Fecha.Date == fecha.Date)
             .AsNoTracking().ToList();
            return fechas;
        }

        public List<Pagos> GetList(Expression<Func<Pagos, bool>> Criterio)
        {
            return _contexto.Pagos
                .Where(Criterio)
                .AsNoTracking()
                .ToList();
        }
        public List<PagosDetalle> Filtro(int id)
        {
            var pago = _contexto.PagosDetalle
                .Where(p => p.PrestamoId== id)
                .AsNoTracking()
                .ToList();
            return pago;
        }

    }

}