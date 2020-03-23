using Olive;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Domain
{
    public class StructureDeserializer
    {
        public static void Load()
        {
            LoadServices();
            LoadFeatures();
            LoadBoards();
        }

        static void LoadServices()
        {
            // LoadServices
            Run(() => Service.All == null, () =>
               {
                   var environment = Context.Current.Environment().EnvironmentName.ToLower();

                   Service.All = ReadXml(GetFromRoot("Services.xml")).Select(x => new Service
                   {
                       Name = x.GetCleanName(),
                       UseIframe = x.GetValue<bool?>("@iframe") ?? false,
                       BaseUrl = x.GetValue<string>("@" + environment) ?? x.GetValue<string>("@url"),
                       Icon = x.GetValue<string>("@icon"),
                       InjectSingleSignon = x.GetValue<bool?>("@sso") ?? false

                   }).ToList();
               });
        }

        static void Run(Func<bool> condition, Action action)
        {
            if (condition() == false) return;

            action();
        }

        static IEnumerable<XElement> ReadXml(FileInfo file)
        {
            return file.ReadAllText().To<XDocument>().Root.Elements();
        }

        static FileInfo GetFromRoot(string filename) => AppDomain.CurrentDomain.WebsiteRoot().GetFile($"Config\\{filename}");


        static FeatureDefinition[] GetFeatureDefinitions()
        {
            var root = new FeatureDefinition(null, new XElement("ROOT"));

            var files = new[] { "Features.xml", "Features.Widgets.xml" }.Select(f => GetFromRoot(f));

            return files
                     .Select(x => ReadXml(x))
                     .SelectMany(x => x)
                     .Select(x => new FeatureDefinition(root, x))
                     .ToArray();
        }

        static void LoadFeatures()
        {
            // LoadFeatures
            Run(() => Feature.All == null, () =>
              {
                  Feature.All = GetFeatureDefinitions()
                  .SelectMany(x => x.GetAllFeatures())
                  .ExceptNull()
                  .ToList();

                  foreach (var item in Feature.All)
                      item.Children = Feature.All.Where(x => x.Parent == item);

              });
        }

        static void LoadBoards()
        {
            // LoadBoards
            Run(() => Board.All == null, () =>
               {
                   Board.All = ReadXml(GetFromRoot("Boards.xml")).Select(x => new Board(x)).ToList();
               });
        }
    }
}