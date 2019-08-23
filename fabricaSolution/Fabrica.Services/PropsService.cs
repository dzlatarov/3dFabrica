﻿namespace Fabrica.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data;
    using Fabrica.Models;
    using Fabrica.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PropsService : DataService, IPropsService
    {
        public PropsService(FabricaDBContext context) : base(context)
        {
        }

        // CREATE
        public async Task Create(PropServiceModel model)
        {
            var prop = Mapper.Map<Prop>(model);

            await this.context.Props.AddAsync(prop);
            await this.context.SaveChangesAsync();
        }

        // EDIT
        public async Task Edit(PropServiceModel model)
        {
            var prop = await this.context.Props.FirstOrDefaultAsync(p => p.Id == model.Id && !p.IsDeleted);

            if (prop == null)
            {
                return;
            }

            prop.ImageUrl = model.ImageUrl;
            prop.Name = model.Name;
            prop.propType = Mapper.Map<PropType>(model.propType);
            prop.Description = model.Description;
            prop.Price = model.Price;
            prop.Hashtags = model.Hashtags;

            this.context.Props.Update(prop);
            await this.context.SaveChangesAsync();
        }

        // DELETE
        public async Task Delete(string id)
        {
            var product = await this.context.Props.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (product == null)
            {
                return;
            }
            
            product.IsDeleted = true;

            this.context.Props.Update(product);
            await this.context.SaveChangesAsync();
        }

        // ACTIVATE
        public async Task Restore(string id)
        {
            var product = await this.context.Props.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == true);

            if (product == null)
            {
                return;
            }

            product.IsDeleted = false;

            this.context.Props.Update(product);
            await this.context.SaveChangesAsync();
        }

        // GET USER PROPS
        public async Task<IEnumerable<T>> GetUserProps<T>(string id)
        {
            var props = await this.context.Props
                .Where(u => u.PropCreatorId == id && u.IsDeleted == false)
                .ProjectTo<T>()
                .ToArrayAsync();

            return props;
        }

        // GET PROP
        public async Task<IEnumerable<T>> GetProp<T>(string id)
        {
            var prop = this.context.Props.Where(p => p.Id == id && p.IsDeleted == false).ProjectTo<T>();

            return prop;
        }

        // GET USER DELETED PROPS
        public async Task<IEnumerable<T>> GetDeletedProps<T>(string id)
        {
            var props = this.context.Props.Where(u => u.PropCreatorId == id && u.IsDeleted == true).ProjectTo<T>();

            return props;
        }

        // GET USER DELETED PROP
        public async Task<Prop> GetDelProp(string id)
        {
            var prop = await this.context.Props.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == true);
            return prop;
        }

        public async Task<IEnumerable<T>> GetAll<T>(bool isDeleted)
        {
            var props = this.context.Props.Where(p => p.IsDeleted == isDeleted).ProjectTo<T>();
            return props;
        }
        
    }
}
