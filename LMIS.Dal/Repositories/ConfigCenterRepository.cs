using LMIS.Dal.Entity;
using LMIS.Infrastructure.Data.DTOs;
using LMIS.Infrastructure.Data.Entities;
using LMIS.Infrastructure.Enums;
using LMIS.Infrastructure.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;

namespace LMIS.Dal.Repositories
{
 public  class ConfigCenterRepository : IConfigCenterRepository
    {
        private readonly Func<List<LocalString>, ConfigCenter, List<LocalString>> _f1 = (a, b) =>
        {
            var langId = Array.IndexOf(Enum.GetValues(typeof(Language)), (Language)b.SexCode) == -1
                ? (int) Language.English
                : b.SexCode;

            a.Add(new LocalString(langId, b.Value));
            return a;
        };
        
        public List<ConfigCenterVm> GetValue(string key, int textType, int sexCode, int categoryType)
        {
            using (var context = new LMISEntities())
            {
                var result = (from config in context.ConfigCenters
                    where (config.Key == key || string.IsNullOrEmpty(key))
                          && (config.TextType == textType || textType==-1)
                          && (config.SexCode == sexCode || textType == -1)
                          && (config.CategoryType == categoryType || categoryType == -1)
                    select new ConfigCenterVm
                    {
                        Key = config.Key,
                        Value = config.Value,
                        TextType = config.TextType,
                        SexCode = config.SexCode,
                        CategoryType = config.CategoryType
                    });

                return result.ToList();
            }
        }

        public ConfigCenterVm GetConfigCenter(string key)
        {
            using (var context = new LMISEntities())
            {
                //Get all active records from the table
                var query = context.ConfigCenters.Where(o => o.Key == key).ToList();

                //Map results to a View Model list
                return query.Select(m => new ConfigCenterVm()
                {
                    Key = m.Key,
                    Value = m.Value,
                    TextType = m.TextType,
                    SexCode = m.SexCode,
                    CategoryType = m.CategoryType
                }).SingleOrDefault();
            }
        }

        public Dictionary<string, string> List(List<string> keys, int langId = 0)
        {
            var lang = Array.IndexOf(Enum.GetValues(typeof(Language)), (Language)langId) == -1
                ? Language.English
                : (Language) langId;

            keys = keys.ConvertAll(k => k.Trim().ToLower());

            using (var db = new LMISEntities())
            {
                return db.ConfigCenters
                    .Where(r => keys.Contains(r.Key.Trim().ToLower()))
                    .ToList()
                    .GroupBy(r => r.Key.Trim().ToLower())
                    .Select(g => new
                    {
                        k = g.Key.Trim().ToLower(),
                        v = new GlobalString(g.Aggregate(new List<LocalString>(), (a, b) => _f1(a, b)), g.Key)
                    })
                    .ToList()
                    .ToDictionary(r => r.k, r => r.v.ToLocalString(lang, true).T);
            }
        }

        public string Get(string key, int langId = 0)
        {
            return List(new List<string>() { key })[key];
        }

     public int Post(Dictionary<string, string> keys)
     {
         using (var context = new LMISEntities())
         {
             ConfigCenter config;

             foreach (var key in  keys)
             {
                 if (key.Key.EndsWith("En"))
                 {

                     config = context.ConfigCenters.Single(w => w.Key == key.Key.Substring(0, key.Key.Length - 2) && w.SexCode == 1);
                 }
                 else if (key.Key.EndsWith("Fr"))
                 {
                     config = context.ConfigCenters.Single(w => w.Key == key.Key.Substring(0, key.Key.Length - 2) && w.SexCode == 2);
                 }
                 else if (key.Key.EndsWith("Ar"))
                 {
                     config = context.ConfigCenters.Single(w => w.Key == key.Key.Substring(0, key.Key.Length - 2) && w.SexCode == 3);
                 }
                 else
                 {
                     config = context.ConfigCenters.Single(w => w.Key == key.Key);
                 }
                 config.Value = key.Value;


             }
             context.SaveChanges();
         }

         return 0;
     }
    }
}