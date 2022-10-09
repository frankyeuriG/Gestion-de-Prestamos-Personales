using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using GestionPrestamos2022.DAL;
using GestionPrestamos2022.Models;

namespace GestionPrestamos2022.BLL
{
    public class PersonasBLL
    {
        private Contexto _contexto;

        public PersonasBLL(Contexto contexto)
        {
            _contexto = contexto;
        }
        public bool Existe(int personaId)
        {
            return _contexto.Personas.Any(o => o.PersonaId == personaId);
        }
        private bool Insertar(Personas persona)
        {
            _contexto.Personas.Add(persona);
            return _contexto.SaveChanges() > 0;
        }
        private bool Modificar(Personas persona)
        {
            _contexto.Entry(persona).State = EntityState.Modified;
            return _contexto.SaveChanges() > 0;

        }
        public bool Guardar(Personas persona)
        {
            if (!Existe(persona.PersonaId))
                return this.Insertar(persona);
            else
                return this.Modificar(persona);
        }
        public bool Editar(Personas persona)
        {
            if (!Existe(persona.PersonaId))
                return this.Insertar(persona);
            else
                return this.Modificar(persona);
        }

        public bool Eliminar(Personas persona)
        {
            _contexto.Entry(persona).State = EntityState.Deleted;
            return _contexto.SaveChanges() > 0;
        }
        public Personas? Buscar(int personaId)
        {
            return _contexto.Personas
            .Where(o => o.PersonaId == personaId)
            .AsNoTracking()
            .SingleOrDefault();
        }

        public List<Personas> BuscarP(string Nombre)
        {
            var nombreE = _contexto.Personas
             .Where(p => p.Nombres == Nombre)
             .AsNoTracking().ToList();
            return nombreE;
        }
        public List<Personas> GetList(Expression<Func<Personas, bool>> Criterio)
        {
            return _contexto.Personas
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
        }

    }

}
