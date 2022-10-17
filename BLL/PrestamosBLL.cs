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
        public bool Existe(int PrestamosId)
        {
            return _contexto.Prestamos.Any(o => o.PrestamoId == PrestamosId);
        }
        private bool Insertar(Prestamos prestamo)
        {
            _contexto.Prestamos.Add(prestamo);

            prestamo.Balance = prestamo.Monto;

            var persona = _contexto.Personas.Find(prestamo.PersonaId);

            persona.Balance += prestamo.Monto;

            int Cantidad = _contexto.SaveChanges();

            return Cantidad> 0;
        }
       public bool Modificar(Prestamos prestamoActual)
        {
            //descontar el monto anterior
            var prestamoAnterior = _contexto.Prestamos
                .Where(p => p.PrestamoId == prestamoActual.PrestamoId)
                .AsNoTracking()
                .SingleOrDefault();

            var personaAnterior = _contexto.Personas.Find(prestamoAnterior.PersonaId);
            personaAnterior.Balance -= prestamoAnterior.Monto;

            _contexto.Entry(prestamoActual).State = EntityState.Modified;
            
            //descontar el monto nuevo
            var persona = _contexto.Personas.Find(prestamoActual.PersonaId);
            persona.Balance += prestamoActual.Monto;

            return _contexto.SaveChanges() > 0;
        }

        public bool Guardar(Prestamos prestamo)
        {
            if (!Existe(prestamo.PrestamoId))
                return this.Insertar(prestamo);
            else
                return this.Modificar(prestamo);
        }
      
        public bool Eliminar(Prestamos prestamo)
        {
            var persona = _contexto.Personas.Find(prestamo.PersonaId);
            persona.Balance -= prestamo.Monto;
            
            _contexto.Entry(prestamo).State = EntityState.Deleted;
            return _contexto.SaveChanges() > 0;
        }

        public Prestamos? Buscar(int prestamoId)
        {
            return _contexto.Prestamos
            .Where(o => o.PrestamoId == prestamoId)
            .AsNoTracking()
            .SingleOrDefault();
        }

        public List<Prestamos> Buscarf(DateTime fecha, DateTime fecha2)
        {

            var fechas = _contexto.Prestamos
             .Where(f => f.Fecha.Date == fecha.Date || f.Vence.Date == fecha2.Date)
             .AsNoTracking()
             .ToList();
            return fechas;
        }
        public List<Prestamos> GetList(Expression<Func<Prestamos, bool>> Criterio)
        {
            return _contexto.Prestamos
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
        }
        public List<Prestamos> Filtro(int id)
        {
            var prestamo = _contexto.Prestamos
             .Where(f => f.PersonaId == id)
             .AsNoTracking()
             .ToList();
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
