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
        public async Task<bool> Existe(int pagosId)
        {
            return await _contexto.Pagos.AnyAsync(o => o.PagoId == pagosId);
        }
        private async Task <bool> Insertar(Pagos pagos)
        {
            await _contexto.Pagos.AddAsync(pagos);

            foreach (var item in pagos.PagosDetalle)
            {
                var prestamo = await _contexto.Prestamos.FindAsync(item.PrestamoId);
                prestamo!.Balance -= item.ValorPagado;

            }
            var persona = await _contexto.Personas.FindAsync(pagos.PersonaId);
            persona!.Balance -= pagos.Monto;

            var insertados = await _contexto.SaveChangesAsync();

            return insertados > 0;
        }
        private async Task<bool> Modificar(Pagos pagoActual)
        {
            var pagoAnterior = await _contexto.Pagos
                 .Where(p => p.PagoId == pagoActual.PagoId)
                 .AsNoTracking()
                 .SingleOrDefaultAsync();

            var Persona = await _contexto.Personas.FindAsync(pagoAnterior!.PersonaId);
            Persona!.Balance += pagoAnterior.Monto;

            foreach (var item in pagoAnterior.PagosDetalle)
            {
                var prestamos = await _contexto.Prestamos.FindAsync(item.PrestamoId);
                prestamos!.Balance += item.ValorPagado;

            }
            await _contexto.Database.ExecuteSqlRawAsync($"Delete FROM PagosDetalle Where PagoId = {pagoActual.PagoId}");

            foreach (var item in pagoActual.PagosDetalle)
            {
                _contexto.Entry(item).State = EntityState.Added;

                var prestamo = await _contexto.Prestamos.FindAsync(item.PrestamoId);
                prestamo!.Balance -= item.ValorPagado;

            }
            var PersonaActual = await _contexto.Personas.FindAsync(pagoActual.PersonaId);
            PersonaActual!.Balance -= pagoActual.Monto;

            _contexto.Entry(pagoActual).State = EntityState.Modified;

            _contexto.Entry(pagoActual).State = EntityState.Detached;

            return await _contexto.SaveChangesAsync() > 0;

        }
        public async Task<bool> Guardar(Pagos pagos)
        {
            var existe = await Existe(pagos.PagoId);

            if (!existe)
                return await this.Insertar(pagos);
            else
                return await this.Modificar(pagos);
        }


        public async Task<bool> Eliminar(Pagos pagos)
        {

            var persona = await _contexto.Personas.FindAsync(pagos.PersonaId);
            persona!.Balance += pagos.Monto;

            foreach (var item in pagos.PagosDetalle)
            {
                var prestamo = await _contexto.Prestamos.FindAsync(item.PrestamoId);
                prestamo!.Balance += item.ValorPagado;
            }

            _contexto.Entry(pagos).State = EntityState.Deleted;

            return await _contexto.SaveChangesAsync() > 0;
        }
        public async Task<Pagos?> Buscar(int pagoId)
        {
            return await _contexto.Pagos
            .Where(o => o.PagoId == pagoId)
            .Include(o => o.PagosDetalle)
            .AsNoTracking()
            .SingleOrDefaultAsync();
        }
        public async Task<List<Pagos>> GetList(Expression<Func<Pagos, bool>> Criterio)
        {
            return await _contexto.Pagos
                .Where(Criterio)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<PagosDetalle>> Filtro(int id)
        {
            var pago = await _contexto.PagosDetalle
                .Where(p => p.PrestamoId == id)
                .AsNoTracking()
                .ToListAsync();
            return pago;
        }

    }

}