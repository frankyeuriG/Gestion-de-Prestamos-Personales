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
            return _contexto.Prestamo.Any(o => o.PrestamosId == PrestamosId);
        }
        private bool Insertar(Prestamo prestamo)
        {
            _contexto.Prestamo.Add(prestamo);
            return _contexto.SaveChanges() > 0;
        }
        private bool Modificar(Prestamo prestamo)
        {
            _contexto.Entry(prestamo).State = EntityState.Modified;
            return _contexto.SaveChanges() > 0;
        }

        public bool Guardar(Prestamo prestamo)
        {
            if (!Existe(prestamo.PrestamosId))
                return this.Insertar(prestamo);
            else
                return this.Modificar(prestamo);
        }
        public bool Eliminar(Prestamo prestamo)
        {
            _contexto.Entry(prestamo).State = EntityState.Deleted;
            return _contexto.SaveChanges() > 0;
        }

        public Prestamo? Buscar(int personaId)
        {
            return _contexto.Prestamo
            .Where(o => o.PersonaId == personaId)
            .AsNoTracking()
            .SingleOrDefault();
        }
        public List<Prestamo> GetList(Expression<Func<Prestamo, bool>> Criterio)
        {
            return _contexto.Prestamo
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
        }

    }

}