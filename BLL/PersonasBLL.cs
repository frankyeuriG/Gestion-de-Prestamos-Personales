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
        public async Task<bool> Existe(int personaId)
        {
            return await _contexto.Personas.AnyAsync(o => o.PersonaId == personaId);
        }
        private async Task<bool> Insertar(Personas persona)
        {
            _contexto.Personas.Add(persona);
            return await _contexto.SaveChangesAsync() > 0;
        }
        private async Task<bool> Modificar(Personas persona)
        {
            _contexto.Entry(persona).State = EntityState.Modified;

            return await _contexto.SaveChangesAsync() > 0;

        }
        public async Task <bool> Guardar(Personas persona)
        {
            if (!await Existe(persona.PersonaId))
                return await this.Insertar(persona);
            else
                return await this.Modificar(persona);
        }
     

        public async Task<bool> Eliminar(Personas persona)
        {
            _contexto.Entry(persona).State = EntityState.Deleted;
            return await _contexto.SaveChangesAsync() > 0;
        }
        public Personas? Buscar(int personaId)
        {
            return _contexto.Personas
            .Where(o => o.PersonaId == personaId)
            .AsNoTracking()
            .SingleOrDefault();
        }

        public async Task<List<Personas>> BuscarP(string Nombre)
        {
            var nombreE = await _contexto.Personas
             .Where(p => p.Nombre!.ToLower() == Nombre.ToLower())
             .AsNoTracking()
             .ToListAsync();
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
