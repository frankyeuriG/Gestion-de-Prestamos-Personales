using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using GestionPrestamos2022.DAL;
using GestionPrestamos2022.Models;

namespace GestionPrestamos2022.BLL
{
    public class PrestamosBLL
    {
        private Contexto _contexto;

        public PrestamosBLL(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task <bool> Existe(int PrestamosId)
        {
            return await _contexto.Prestamos.AnyAsync(o => o.PrestamoId == PrestamosId);
        }
        private async Task <bool> Insertar(Prestamos prestamo)
        {
            _contexto.Prestamos.Add(prestamo);

            prestamo.Balance = prestamo.Monto;

            var persona = await _contexto.Personas.FindAsync(prestamo.PersonaId);

            persona!.Balance += prestamo.Monto;

            int Cantidad = await _contexto.SaveChangesAsync();

            return Cantidad> 0;
        }
       public async Task <bool> Modificar(Prestamos prestamoActual)
        {
            //descontar el monto anterior
            var prestamoAnterior = await _contexto.Prestamos
                .Where(p => p.PrestamoId == prestamoActual.PrestamoId)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            var personaAnterior = _contexto.Personas.Find(prestamoAnterior.PersonaId);
            personaAnterior.Balance -= prestamoAnterior.Monto;

            _contexto.Entry(prestamoActual).State = EntityState.Modified;
            
            //descontar el monto nuevo
            var persona = await _contexto.Personas.FindAsync(prestamoActual.PersonaId);
            persona!.Balance += prestamoActual.Monto;

            return await _contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Prestamos prestamo)
        {
            if (!await Existe(prestamo.PrestamoId))
                return await this.Insertar(prestamo);
            else
                return await this.Modificar(prestamo);
        }
      
        public async Task<bool> Eliminar(Prestamos prestamo)
        {
            var persona = await _contexto.Personas.FindAsync(prestamo.PersonaId);
            persona!.Balance -= prestamo.Monto;
            
            _contexto.Entry(prestamo).State = EntityState.Deleted;
            return await _contexto.SaveChangesAsync() > 0;
        }

        public Prestamos? Buscar(int prestamoId)
        {
            return _contexto.Prestamos
            .Where(o => o.PrestamoId == prestamoId)
            .AsNoTracking()
            .SingleOrDefault();
        }

        public async Task<List<Prestamos>> Buscarf(DateTime fecha, DateTime fecha2)
        {

            var fechas = await _contexto.Prestamos
             .Where(f => f.Fecha.Date == fecha.Date || f.Vence.Date == fecha2.Date)
             .AsNoTracking()
             .ToListAsync();
            return fechas;
        }
        public List<Prestamos> GetList(Expression<Func<Prestamos, bool>> Criterio)
        {
            return _contexto.Prestamos
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
        }
        public async Task<List<Prestamos>> Filtro(int id)
        {
            var prestamo = await _contexto.Prestamos
             .Where(f => f.PersonaId == id)
             .AsNoTracking()
             .ToListAsync();
            return prestamo;
        }
        public List<Prestamos> Filtro2(int id)
        {
            var prestamo = _contexto.Prestamos
             .Where(f => f.PrestamoId == id)
             .AsNoTracking()
             .ToList();
            return prestamo;
        }

    }

}
