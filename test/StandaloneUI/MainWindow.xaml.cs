﻿using NuGet.Client;
using NuGet.Configuration;
using NuGet.PackageManagement;
using NuGet.PackageManagement.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Windows;
using System.Linq;
using NuGet.ProjectManagement;
using NuGet.Frameworks;
using System.Diagnostics;

namespace StandaloneUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Import]
        public INuGetUIFactory _uiServiceFactory;

        [ImportMany]
        public IEnumerable<Lazy<INuGetResourceProvider, INuGetResourceProviderMetadata>> _resourceProviders;

        [Import]
        public NuGet.Configuration.ISettings _settings;

        //[Import]
        public INuGetUIContextFactory _contextFactory;

        private CompositionContainer _container;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            _container = Initialize();

            this.Title = "NuGet Standalone UI";

            foreach (var dte in DTEHelper.GetDTE())
            {

            }

            var repositoryProvider = new SourceRepositoryProvider(_resourceProviders, _settings);

            var projectMetadata = new Dictionary<string, object>();
            projectMetadata.Add(NuGetProjectMetadataKeys.Name, "Test Project");
            projectMetadata.Add(NuGetProjectMetadataKeys.TargetFramework, NuGetFramework.Parse("net45"));

            NuGetProject project = new PackagesConfigNuGetProject(@"C:\Users\juste\Documents\Visual Studio 2013\Projects\ConsoleApplication2\ConsoleApplication2\packages.config", projectMetadata);
            var projects = new NuGetProject[] { project };

            _contextFactory = new NuGetUIContextFactory(repositoryProvider);
            var context = _contextFactory.Create(projects);
            var uiController = _uiServiceFactory.Create(projects);

            PackageManagerModel model = new PackageManagerModel(uiController, context);

            PackageManagerControl control = new PackageManagerControl(model);

            NuGetWindow.Child = control;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private CompositionContainer Initialize()
        {
            string assemblyName = Assembly.GetEntryAssembly().FullName;

            var path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            using (var catalog = new AggregateCatalog(
                new AssemblyCatalog(Assembly.Load(assemblyName)),
                new DirectoryCatalog(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.dll")))
            {
                var container = new CompositionContainer(catalog);

                try
                {
                    container.ComposeParts(this);
                    return container;
                }
                catch (Exception ex)
                {
                    Debug.Fail("MEF: " + ex.ToString());

                    throw;
                }
            }
        }
    }

    internal class V3OnlyPackageSourceProvider : IPackageSourceProvider
    {
        public void DisablePackageSource(NuGet.Configuration.PackageSource source)
        {
            throw new NotImplementedException();
        }

        public bool IsPackageSourceEnabled(NuGet.Configuration.PackageSource source)
        {
            return true;
        }

        public IEnumerable<NuGet.Configuration.PackageSource> LoadPackageSources()
        {
            return new List<NuGet.Configuration.PackageSource>() { new NuGet.Configuration.PackageSource("https://api.nuget.org/v3/index.json", "nuget.org v3") };
        }

        public event EventHandler PackageSourcesSaved;

        public void SavePackageSources(IEnumerable<NuGet.Configuration.PackageSource> sources)
        {
            throw new NotImplementedException();
        }
    }
}
