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

            var persona = _contexto.Personas.Find(prestamo.PersonaId);
            persona.Balance += prestamo.Monto;

            int Cantidad = _contexto.SaveChanges();

            return Cantidad> 0;
        }
       public bool Modificar(Prestamo prestamoActual)
        {
            //descontar el monto anterior
            var prestamoAnterior = _contexto.Prestamo
                .Where(p => p.PrestamosId == prestamoActual.PrestamosId)
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

        public bool Guardar(Prestamo prestamo)
        {
            if (!Existe(prestamo.PrestamosId))
                return this.Insertar(prestamo);
            else
                return this.Modificar(prestamo);
        }
         public bool Editar(Prestamo prestamo)
        {
            if (!Existe(prestamo.PrestamosId))
                return this.Insertar(prestamo);
            else
                return this.Modificar(prestamo);
        }
        public bool Eliminar(Prestamo prestamo)
        {
            var persona = _contexto.Personas.Find(prestamo.PersonaId);
            persona.Balance -= prestamo.Monto;
            
            _contexto.Entry(prestamo).State = EntityState.Deleted;
            return _contexto.SaveChanges() > 0;
        }

        public Prestamo? Buscar(int prestamoId)
        {
            return _contexto.Prestamo
            .Where(o => o.PrestamosId == prestamoId)
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
