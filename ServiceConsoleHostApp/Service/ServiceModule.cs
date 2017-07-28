using Autofac;
using WcfFileService.BL.Helpers;
using WcfFileService.BL.Operations;

namespace WcfFileService
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DirectoryHelper>().As<IDirectoryHelper>();
            builder.RegisterType<DirectoryInfoOperation>().As<IDirectoryInfoOperation>();
            builder.RegisterType<FileCopyOperation>().As<IFileCopyOperation>();
            builder.RegisterType<DirectoryCreateOperation>().As<IDirectoryCreateOperation>();
            builder.RegisterType<DirectoryCopyOperation>().As<IDirectoryCopyOperation>();
        }
    }
}
