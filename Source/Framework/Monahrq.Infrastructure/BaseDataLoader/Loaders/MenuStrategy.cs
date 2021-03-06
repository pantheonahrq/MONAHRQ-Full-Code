﻿using Microsoft.Practices.Prism.Logging;
using Monahrq.Infrastructure.Domain.Websites;
using Monahrq.Infrastructure.Entities.Core.Import;
using System;
using System.ComponentModel.Composition;
using System.IO;

namespace Monahrq.Infrastructure.BaseDataLoader.Loaders
{
    /// <summary>
    /// The <see cref="Menu"/> base data import strategy.
    /// </summary>
    /// <seealso cref="Monahrq.Infrastructure.BaseDataLoader.BaseDataCustomImporter{Monahrq.Infrastructure.Domain.Websites.Menu, System.Int32}" />
    [Export(DataImportContracts.BaseDataLoader, typeof(IBasedataImporter))]
    public class MenuStrategy : BaseDataCustomImporter<Menu, int>
    {
        /// <summary>
        /// Called when [imports satisfied].
        /// </summary>
        public override void OnImportsSatisfied()
        {
            base.OnImportsSatisfied();
            VersionStrategy = new DefaultBaseDataVersionStrategy(Logger, DataProvider, typeof(Menu));
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        public override void LoadData()
        {
            try
            {
                if (Directory.Exists(baseDataDir))
                {
                    var files = Directory.GetFiles(baseDataDir, "Menu*.sql");
                    foreach (var file in files)
                    {
                        try
                        {
                            VersionStrategy.Filename = file;
                            // Appending Menus
                            if (!VersionStrategy.IsLoaded())
                            {
                                // Verify data file exists.
                                if (!File.Exists(Path.Combine(baseDataDir, file)))
                                {
                                    Logger.Warning(
                                        "Import file \"{0}\" missing from the base data resources directory.",
                                        file);
                                    return;
                                }

                                using (var session = DataProvider.SessionFactory.OpenSession())
                                {
                                    session.CreateSQLQuery(ReadFileContent(Path.Combine(baseDataDir, file)))
                                        .ExecuteUpdate();
                                    session.SaveOrUpdate(VersionStrategy.Version);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Write(e, "Error loading menu data from file {0}", file);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "Error loading menu data");
            }
        }

        /// <summary>
        /// Reads the content of the file.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <returns></returns>
        private string ReadFileContent(string filepath)
        {
            return File.ReadAllText(filepath);
        }
    }
}
