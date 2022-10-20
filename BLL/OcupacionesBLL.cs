using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using GestionPrestamos2022.DAL;
using GestionPrestamos2022.Models;
using System;

namespace GestionPrestamos2022.BLL
{
    public class OcupacionesBLL
    {
        private Contexto _contexto;

        public OcupacionesBLL(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<bool> Existe(int ocupacionId)
        {
            return await _contexto.Ocupaciones.AnyAsync(o => o.OcupacionId == ocupacionId);
        }
        private async Task<bool> Insertar(Ocupaciones ocupacion)
        {
             _contexto.Ocupaciones.Add(ocupacion);

            return await _contexto.SaveChangesAsync() > 0;
        }
        private async Task<bool> Modificar(Ocupaciones ocupacion)
        {
            _contexto.Entry(ocupacion).State = EntityState.Modified;
            return await _contexto.SaveChangesAsync() > 0;

        }
        public async Task<bool> Guardar(Ocupaciones ocupacion)
        {
            if (!await Existe(ocupacion.OcupacionId))
                return await this.Insertar(ocupacion);
            else
                return await this.Modificar(ocupacion);
        }
        public async Task<bool> Eliminar(Ocupaciones ocupacion)
        {
            _contexto.Entry(ocupacion).State = EntityState.Deleted;
            return await _contexto.SaveChangesAsync() > 0;
        }
        public Ocupaciones? Buscar(int ocupacionId)
        {
            return _contexto.Ocupaciones
            .Where(o => o.OcupacionId == ocupacionId)
            .AsNoTracking()
            .SingleOrDefault();
        }
        public async Task<List<Ocupaciones>> BuscarO(string Profecion)
        {
            var descripcion = await _contexto.Ocupaciones
             .Where(o => o.Descripcion!.ToLower() == Profecion.ToLower())
             .AsNoTracking()
             .ToListAsync();
            return descripcion;
        }

        public List<Ocupaciones> GetList(Expression<Func<Ocupaciones, bool>> Criterio)
        {
            return _contexto.Ocupaciones
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
        }

    }

}