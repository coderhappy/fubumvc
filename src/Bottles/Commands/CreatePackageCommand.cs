using System;
using Bottles.Assemblies;
using Bottles.Creation;
using Bottles.Parsing;
using Bottles.Zipping;
using FubuCore;
using FubuCore.CommandLine;

namespace Bottles.Commands
{
    [CommandDescription("Create a package file from a package directory", Name = "create-pak")]
    public class CreatePackageCommand : FubuCommand<CreatePackageInput>
    {
        public override bool Execute(CreatePackageInput input)
        {
            input.PackageFolder = AliasCommand.AliasFolder(input.PackageFolder);
            Execute(input, new FileSystem());
            return true;
        }

        public void Execute(CreatePackageInput input, IFileSystem fileSystem)
        {
            if (fileSystem.FileExists(input.ZipFile) && !input.ForceFlag)
            {
                WriteZipFileAlreadyExists(input.ZipFile);
                return;
            }

            // Delete the file if it exists?
            if (fileSystem.PackageManifestExists(input.PackageFolder))
            {
                fileSystem.DeleteFile(input.ZipFile);
                CreatePackage(input, fileSystem);
            }
            else
            {
                WritePackageManifestDoesNotExist(input.PackageFolder);
            }
        }

        public virtual void WriteZipFileAlreadyExists(string zipFileName)
        {
            Console.WriteLine("Package Zip file already exists at {0}.  Use the -force flag to overwrite the existing flag", zipFileName);
        }

        public virtual void WritePackageManifestDoesNotExist(string packageFolder)
        {
            Console.WriteLine(
                "The requested package folder at {0} does not have a package manifest.  Run 'fubu init-pak \"{0}\"' first.",
                packageFolder);
        }

        public virtual void CreatePackage(CreatePackageInput input, IFileSystem fileSystem)
        {
            // TODO -- override this in the input
            var fileName = FileSystem.Combine(input.PackageFolder, PackageManifest.FILE);
            var reader = new PackageManifestXmlReader();
            var manifest = reader.ReadFrom(fileName);

            //var manifest = fileSystem.LoadPackageManifestFrom(input.PackageFolder);

            var creator = new PackageCreator(fileSystem, new ZipFileService(fileSystem), new PackageLogger(), new AssemblyFileFinder(new FileSystem()));
            creator.CreatePackage(input, manifest);
        }
    }
}